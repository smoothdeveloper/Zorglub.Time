﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Simple
{
    using Zorglub.Time.Core;
    using Zorglub.Time.Core.Arithmetic;
    using Zorglub.Time.Core.Intervals;
    using Zorglub.Time.Hemerology;
    using Zorglub.Time.Hemerology.Scopes;

    #region Developer Notes

    // Normally, we should implement
    // - ICalendar<CalendarDate>
    // - ICalendar<CalendarDay>
    // - ICalendar<OrdinalDate>
    // but CalendarYear and CalendarMonth make this unnecessary.
    //
    //   Today()            Calendar.GetCurrentXXX()
    //
    //   GetDaysInYear()    DateRange iter
    //                      CalendarDate.GetDaysInYear()
    //   GetDaysInMonth()   DateRange iter
    //
    //   GetStartOfYear()   ISimpleDate<>.AtStartOfYear()
    //   GetEndOfYear()     ISimpleDate<>.AtEndOfYear()
    //   GetStartOfMonth()  ISimpleDate<>.AtStartOfMonth()
    //   GetEndOfMonth()    ISimpleDate<>.AtEndOfMonth()
    //
    //                      ISimpleDate<>.AtDayOfYear()
    //                      ISimpleDate<>.AtDayOfMonth()
    //
    // Conversions
    // -----------
    // - Conversion between CalendarDate, OrdinalDate and CalendarDay are
    //   methods on the objects themselves; see IDateXXX<>.ToXXX().
    // - Conversion from a date object to a DayNumber is a method on the
    //   object himself; see ToDayNumber().
    // - According to our definition above, Today() is a conversion op,
    //   except for CalendarDay...
    // We have a static method FromDayNumber() for each calendrical type
    // BUT it only deals with the Gregorian calendar.

    #endregion

    /// <summary>
    /// Defines a calendar to which the system assigns a unique ID and provides a base for derived
    /// classes.
    /// <para>This class can ONLY be inherited from within friend assemblies.</para>
    /// </summary>
    public partial class Calendar : ICalendar
    {
        /// <summary>
        /// Represents the absolute minimum value admissible for the minimum total number of days
        /// there is at least in a month.
        /// <para>This field is a constant equal to 7.</para>
        /// </summary>
        public const int MinMinDaysInMonth = FastArithmetic.MinMinDaysInMonth;

        /// <summary>
        /// Represents the integer value of the day of the week of the epoch of the calendar.
        /// <para>This field is read-only.</para>
        /// </summary>
        private readonly int _epochDayOfWeek;

        /// <summary>
        /// Represents the mathematical rules followed by the calendar.
        /// </summary>
        private CalendarMath _math;

        /// <summary>
        /// Called from constructors in derived classes to initialize the <see cref="Calendar"/>
        /// class.
        /// <para>Constructor for (built-in) system calendars.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="schema"/> is null.</exception>
        private protected Calendar(
            CalendarId ident,
            SystemSchema schema,
            DayNumber epoch,
            bool proleptic)
            : this(
                  (Cuid)ident,
                  ident.ToCalendarKey(),
                  schema,
                  epoch,
                  proleptic,
                  userDefined: false)
        {
            Debug.Assert(!ident.IsInvalid());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Calendar"/> class.
        /// <para>Constructor for user-defined calendars.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="schema"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="schema"/> defines at least one month
        /// whose length is strictly less than <see cref="MinMinDaysInMonth"/>.</exception>
        /// <exception cref="ArgumentException">The range of supported years by
        /// <paramref name="schema"/> does not contain the interval [-9998, 9999].</exception>
        internal Calendar(
            Cuid id,
            string key,
            SystemSchema schema,
            DayNumber epoch,
            bool proleptic)
            : this(
                  id,
                  key,
                  schema,
                  epoch,
                  proleptic,
                  userDefined: true)
        {
            Debug.Assert(id >= Cuid.MinUser);
            Debug.Assert(id == Cuid.Invalid || id <= Cuid.Max);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Calendar"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="schema"/> is null.</exception>
        private Calendar(
            Cuid id,
            string key,
            SystemSchema schema,
            DayNumber epoch,
            bool proleptic,
            bool userDefined)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));

            Id = id;
            Epoch = epoch;
            _epochDayOfWeek = (int)epoch.DayOfWeek;

            IsProleptic = proleptic;
            IsUserDefined = userDefined;

            if (proleptic)
            {
                Scope = ProlepticShortScope.Create(schema, epoch);
                YearOverflowChecker = ProlepticShortScope.YearOverflowChecker;
            }
            else
            {
                Scope = StandardShortScope.Create(schema, epoch);
                YearOverflowChecker = StandardShortScope.YearOverflowChecker;
            }

            PreValidator = schema.PreValidator;
            Arithmetic = FastArithmetic.Create(schema);

            SupportedYears = Scope.SupportedYears;
            Domain = Scope.Domain;
            (MinDaysSinceEpoch, MaxDaysSinceEpoch) = Domain.Endpoints.Select(x => x - Epoch);

            // Keep this at the end of the constructor: before using "this",
            // all props should be initialized.
            _math = CalendarMath.Create(this);
        }

        /// <summary>
        /// Gets the unique key of the current instance.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Returns true if the current instance is user-defined; otherwise returns false.
        /// </summary>
        public bool IsUserDefined { get; }

        /// <summary>
        /// Gets the permanent unique identifier of the current instance.
        /// </summary>
        /// <exception cref="NotSupportedException">The current instance does not have a permanent
        /// unique identifier.</exception>
        public CalendarId PermanentId =>
            IsUserDefined ? Throw.NotSupported<CalendarId>() : (CalendarId)Id;

        /// <inheritdoc />
        public DayNumber Epoch { get; }

        /// <summary>
        /// Returns true if the current instance is proleptic; otherwise returns false.
        /// <para>A calendar is said to be (strongly) proleptic if it applies to dates before its
        /// epoch.</para>
        /// </summary>
        /// <remarks>
        /// <para>A calendar is <i>weakly proleptic</i> or simply <i>retropolated</i> if it is
        /// extended backward to the dates before its official introduction but only on or after its
        /// epoch.</para>
        /// <para>When we say that a calendar is <i>proleptic</i> without any further detail, we
        /// always mean strongly proleptic.</para>
        /// </remarks>
        // > [En parlant d'un phénomène, d'un fait historique] Qui est fixé ou
        // > daté d'après une méthode ou une ère chronologique qui n'était pas
        // > encore établie au moment où le fait ou le phénomène concerné se
        // > produisait. C'est ainsi qu'on fixe les événements de l'antiquité
        // > biblique, religieux ou profanes, en années juliennes proleptiques
        // > (Foit.11968).
        // https://www.cnrtl.fr/definition/proleptique
        //
        // NB: on n'ajoute pas cette propriété à ICalendar car notre définition
        // dit implicitement que "toutes" les dates avant l'epoch doivent être
        // acceptées. Il est donc nécessaire mais pas suffisant de vérifier que
        // MinDayNumber < Epoch (ou MinYear < 1). En même temps, quelle pourrait
        // être une limite basse acceptable pour tout type de ICalendar ?
        // Dans le cas présent, c'est plus simple, on s'accorde pour dire que
        // -9998 est la première année valide.
        public bool IsProleptic { get; }

        /// <inheritdoc />
        public CalendricalAlgorithm Algorithm => Schema.Algorithm;

        /// <inheritdoc />
        public CalendricalFamily Family => Schema.Family;

        /// <inheritdoc />
        public CalendricalAdjustments PeriodicAdjustments => Schema.PeriodicAdjustments;

        #region Domain & Min/Max Values
        // For CalendarYear & co, do not use a range. It's meaningless since
        // one cannot create objects outside these ranges. It's really the
        // equivalent of IMinMaxValue<>.

        /// <inheritdoc />
        public Range<DayNumber> Domain { get; }

        /// <inheritdoc />
        public Range<int> SupportedYears { get; }

        /// <summary>
        /// Represents the minimum possible value for the number of consecutive days from the epoch.
        /// <para>This field is read-only.</para>
        /// </summary>
        internal int MinDaysSinceEpoch { get; }

        /// <summary>
        /// Represents the maximum possible value for the number of consecutive days from the epoch.
        /// <para>This field is read-only.</para>
        /// </summary>
        internal int MaxDaysSinceEpoch { get; }

        private OrderedPair<CalendarYear> _minMaxYear;
        /// <summary>
        /// Gets the pair of earliest and latest supported <see cref="CalendarYear"/>.
        /// </summary>
        /// <returns>The pair (-9998, 9999) if the current instance is proleptic; otherwise the
        /// pair (1, 9999).</returns>
        public OrderedPair<CalendarYear> MinMaxYear
        {
            get { EnsureInitialized(); return _minMaxYear; }
        }

        private OrderedPair<CalendarMonth> _minMaxMonth;
        /// <summary>
        /// Gets the pair of earliest and latest supported <see cref="CalendarMonth"/>.
        /// </summary>
        public OrderedPair<CalendarMonth> MinMaxMonth
        {
            get { EnsureInitialized(); return _minMaxMonth; }
        }

        private OrderedPair<CalendarDate> _minMaxDate;
        /// <summary>
        /// Gets the pair of earliest and latest supported <see cref="CalendarDate"/>.
        /// </summary>
        public OrderedPair<CalendarDate> MinMaxDate
        {
            get { EnsureInitialized(); return _minMaxDate; }
        }

        private OrderedPair<CalendarDay> _minMaxDay;
        /// <summary>
        /// Gets the pair of earliest and latest supported <see cref="CalendarDay"/>.
        /// </summary>
        public OrderedPair<CalendarDay> MinMaxDay
        {
            get { EnsureInitialized(); return _minMaxDay; }
        }

        private OrderedPair<OrdinalDate> _minMaxOrdinal;
        /// <summary>
        /// Gets the pair of earliest and latest supported <see cref="OrdinalDate"/>.
        /// </summary>
        public OrderedPair<OrdinalDate> MinMaxOrdinal
        {
            get { EnsureInitialized(); return _minMaxOrdinal; }
        }

        private bool _initialized;
        private void EnsureInitialized()
        {
            // We delay the initialization for the following reasons:
            // - We want the ctor to be as lightweight as possible.
            //   It's important for it not to fail otherwise the CalendarCatalog
            //   initialization will crash hard.
            // - With unfinished schemas, GetDateParts() or GetOrdinalParts()
            //   may throw.
            // - Too many types involved which could also result in type init
            //   problems.
            // - We don't really need these props.

            if (_initialized) return;

            // GetCalendarYear() without the validation part.
            var minMaxYear = from y in SupportedYears.Endpoints select new CalendarYear(y, Id);

            _minMaxYear = minMaxYear;

            // NB: It might not be obvious at first glance, but the param "y"
            // is a CalendarYear and so is bounded to the current calendar.
            _minMaxMonth =
                minMaxYear.Select(
                    y => y.FirstMonth,
                    y => y.LastMonth);
            _minMaxDate =
                // REVIEW(code): optimize.
                minMaxYear.Select(
                    y => y.FirstDay.ToCalendarDate(),
                    y => y.LastDay.ToCalendarDate());
            _minMaxOrdinal =
                minMaxYear.Select(
                    y => y.FirstDay,
                    y => y.LastDay);

            // GetCalendarDay() without the validation part.
            _minMaxDay =
                from dayNumber in Domain.Endpoints
                select new CalendarDay(dayNumber - Epoch, Id);

            _initialized = true;
        }

        #endregion

        /// <inheritdoc />
        public ICalendarScope Scope { get; }

        /// <summary>
        /// Gets or sets the mathematical rules followed by the current instance.
        /// <para>WARNING: since the calendars are singletons, setting this property will change the
        /// related mathematical operations for ALL objects using this calendar instance.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public CalendarMath Math
        {
            get => _math;
            set
            {
                Requires.NotNull(value);
                if (value.Cuid != Id) Throw.Argument(nameof(value));

                _math = value;
            }
        }

        /// <summary>
        /// Gets the ID of the current instance.
        /// </summary>
        internal Cuid Id { get; }

        /// <summary>
        /// Gets the calendrical schema.
        /// </summary>
        internal SystemSchema Schema { get; }

        /// <summary>
        /// Gets the pre-validator.
        /// </summary>
        internal ICalendricalPreValidator PreValidator { get; }

        /// <summary>
        /// Gets the arithmetical operators.
        /// </summary>
        internal FastArithmetic Arithmetic { get; }

        /// <summary>
        /// Gets the checker for overflows of the range of years.
        /// </summary>
        internal IOverflowChecker<int> YearOverflowChecker { get; }

        /// <summary>
        /// Returns a culture-independent string representation of this calendar.
        /// </summary>
        [Pure]
        public sealed override string ToString() => Key;

        /// <inheritdoc />
        [Pure]
        public bool IsRegular(out int monthsInYear) => Schema.IsRegular(out monthsInYear);

        /// <summary>
        /// Validates the specified <see cref="Cuid"/>.
        /// </summary>
        /// <exception cref="ArgumentException">The validation failed.</exception>
        private protected void ValidateCuid(Cuid cuid, string paramName)
        {
            if (cuid != Id) Throw.BadCuid(paramName, Id, cuid);
        }
    }

    public partial class Calendar // Year or month infos
    {
        // Hidden, we have "better" alternatives.

        /// <inheritdoc />
        /// <exception cref="AoorException">The year is outside the range of supported years.
        /// </exception>
        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "See CalendarYear.IsLeap.")]
        bool ICalendricalKernel.IsLeapYear(int year)
        {
            Scope.ValidateYear(year);
            return Schema.IsLeapYear(year);
        }

        /// <inheritdoc />
        /// <exception cref="AoorException">The month is either invalid or outside the range of
        /// supported months.</exception>
        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "See CalendarMonth.IsIntercalary.")]
        bool ICalendricalKernel.IsIntercalaryMonth(int year, int month)
        {
            Scope.ValidateYearMonth(year, month);
            return Schema.IsIntercalaryMonth(year, month);
        }

        /// <inheritdoc />
        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "See DateType.IsIntercalary.")]
        bool ICalendricalKernel.IsIntercalaryDay(int year, int month, int day)
        {
            Scope.ValidateYearMonthDay(year, month, day);
            return Schema.IsIntercalaryDay(year, month, day);
        }

        /// <inheritdoc />
        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "See DateType.IsSupplementaryDay.")]
        bool ICalendricalKernel.IsSupplementaryDay(int year, int month, int day)
        {
            Scope.ValidateYearMonthDay(year, month, day);
            return Schema.IsSupplementaryDay(year, month, day);
        }

        /// <inheritdoc />
        /// <exception cref="AoorException">The year is outside the range of supported years.
        /// </exception>
        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "See CalendarYear.CountMonthsInYear().")]
        int ICalendricalKernel.CountMonthsInYear(int year)
        {
            Scope.ValidateYear(year);
            return Schema.CountMonthsInYear(year);
        }

        /// <inheritdoc />
        /// <exception cref="AoorException">The year is outside the range of supported years.
        /// </exception>
        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "See CalendarYear.CountDaysInYear().")]
        int ICalendricalKernel.CountDaysInYear(int year)
        {
            Scope.ValidateYear(year);
            return Schema.CountDaysInYear(year);
        }

        /// <inheritdoc />
        /// <exception cref="AoorException">The month is either invalid or outside the range of
        /// supported months.</exception>
        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "See CalendarMonth.CountDaysInMonth().")]
        int ICalendricalKernel.CountDaysInMonth(int year, int month)
        {
            Scope.ValidateYearMonth(year, month);
            return Schema.CountDaysInMonth(year, month);
        }
    }

    public partial class Calendar // Factories
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CalendarYear"/> struct from its components.
        /// </summary>
        /// <exception cref="AoorException"><paramref name="year"/> is outside the range of years
        /// supported by the current instance.</exception>
        [Pure]
        public CalendarYear GetCalendarYear(int year)
        {
            Scope.ValidateYear(year);
            return new CalendarYear(year, Id);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CalendarMonth"/> struct from its components.
        /// </summary>
        /// <exception cref="AoorException">The specified components do not form a valid month or
        /// <paramref name="year"/> is outside the range of years supported by the current instance.
        /// </exception>
        [Pure]
        public CalendarMonth GetCalendarMonth(int year, int month)
        {
            Scope.ValidateYearMonth(year, month);
            return new CalendarMonth(year, month, Id);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CalendarDate"/> struct from its components.
        /// </summary>
        /// <exception cref="AoorException">The specified components do not form a valid date or
        /// <paramref name="year"/> is outside the range of years supported by the current instance.
        /// </exception>
        [Pure]
        public CalendarDate GetCalendarDate(int year, int month, int day)
        {
            Scope.ValidateYearMonthDay(year, month, day);
            return new CalendarDate(year, month, day, Id);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="OrdinalDate"/> struct from its components.
        /// </summary>
        /// <exception cref="AoorException">The specified components do not form a valid ordinal
        /// date or <paramref name="year"/> is outside the range of years supported by the current
        /// instance.</exception>
        [Pure]
        public OrdinalDate GetOrdinalDate(int year, int dayOfYear)
        {
            Scope.ValidateOrdinal(year, dayOfYear);
            return new OrdinalDate(year, dayOfYear, Id);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CalendarDay"/> struct from its components.
        /// </summary>
        /// <exception cref="AoorException"><paramref name="dayNumber"/> is outside the range of
        /// values supported by this calendar.</exception>
        [Pure]
        public CalendarDay GetCalendarDay(DayNumber dayNumber)
        {
            Domain.Validate(dayNumber);
            return new CalendarDay(dayNumber - Epoch, Id);
        }

        #region  GetCurrentXXX()
        // Identical to the conversion methods found below but without the
        // validation part for system calendars as we know for sure that today
        // is always admissible.

        /// <summary>
        /// Obtains the current <see cref="CalendarYear"/> on this machine,
        /// expressed in local time, not UTC.
        /// </summary>
        /// <exception cref="AoorException">Today is outside the range of values supported by this
        /// calendar.</exception>
        [Pure]
        public CalendarYear GetCurrentYear()
        {
            var today = DayNumber.Today();
            if (IsUserDefined) { Domain.Validate(today); }
            int y = Schema.GetYear(today - Epoch);
            return new CalendarYear(y, Id);
        }

        /// <summary>
        /// Obtains the current <see cref="CalendarMonth"/> on this machine,
        /// expressed in local time, not UTC.
        /// </summary>
        /// <exception cref="AoorException">Today is outside the range of values supported by this
        /// calendar.</exception>
        [Pure]
        public CalendarMonth GetCurrentMonth()
        {
            var today = DayNumber.Today();
            if (IsUserDefined) { Domain.Validate(today); }
            var ymd = Schema.GetDateParts(today - Epoch);
            return new CalendarMonth(ymd.Yemo, Id);
        }

        /// <summary>
        /// Obtains the current <see cref="CalendarDate"/> on this machine,
        /// expressed in local time, not UTC.
        /// </summary>
        /// <exception cref="AoorException">Today is outside the range of values supported by this
        /// calendar.</exception>
        [Pure]
        public CalendarDate GetCurrentDate()
        {
            var today = DayNumber.Today();
            if (IsUserDefined) { Domain.Validate(today); }
            var ymd = Schema.GetDateParts(today - Epoch);
            return new CalendarDate(ymd, Id);
        }

        /// <summary>
        /// Obtains the current <see cref="CalendarDay"/> on this machine,
        /// expressed in local time, not UTC.
        /// </summary>
        /// <exception cref="AoorException">Today is outside the range of values supported by this
        /// calendar.</exception>
        [Pure]
        public CalendarDay GetCurrentDay()
        {
            var today = DayNumber.Today();
            if (IsUserDefined) { Domain.Validate(today); }
            return new(today - Epoch, Id);
        }

        /// <summary>
        /// Obtains the current <see cref="OrdinalDate"/> on this machine,
        /// expressed in local time, not UTC.
        /// </summary>
        /// <exception cref="AoorException">Today is outside the range of values supported by this
        /// calendar.</exception>
        [Pure]
        public OrdinalDate GetCurrentOrdinal()
        {
            var today = DayNumber.Today();
            if (IsUserDefined) { Domain.Validate(today); }
            var ydoy = Schema.GetOrdinalParts(today - Epoch);
            return new OrdinalDate(ydoy, Id);
        }

        #endregion
    }

    public partial class Calendar // Conversions
    {
        /// <summary>
        /// Obtains the day number on the specified calendar year.
        /// </summary>
        /// <exception cref="AoorException"><paramref name="dayNumber"/> is outside the range of
        /// values supported by this calendar.</exception>
        [Pure]
        public CalendarYear GetCalendarYearOn(DayNumber dayNumber)
        {
            Domain.Validate(dayNumber);
            int y = Schema.GetYear(dayNumber - Epoch);
            return new CalendarYear(y, Id);
        }

        /// <summary>
        /// Obtains the day number on the specified calendar month.
        /// </summary>
        /// <exception cref="AoorException"><paramref name="dayNumber"/> is outside the range of
        /// values supported by this calendar.</exception>
        [Pure]
        public CalendarMonth GetCalendarMonthOn(DayNumber dayNumber)
        {
            Domain.Validate(dayNumber);
            var ymd = Schema.GetDateParts(dayNumber - Epoch);
            return new CalendarMonth(ymd.Yemo, Id);
        }

        /// <summary>
        /// Obtains the day number on the specified calendar date.
        /// </summary>
        /// <exception cref="AoorException"><paramref name="dayNumber"/> is outside the range of
        /// values supported by this calendar.</exception>
        [Pure]
        public CalendarDate GetCalendarDateOn(DayNumber dayNumber)
        {
            Domain.Validate(dayNumber);
            var ymd = Schema.GetDateParts(dayNumber - Epoch);
            return new CalendarDate(ymd, Id);
        }

        /// <summary>
        /// Obtains the day number on the specified ordinal date.
        /// </summary>
        /// <exception cref="AoorException"><paramref name="dayNumber"/> is outside the range of
        /// values supported by this calendar.</exception>
        [Pure]
        public OrdinalDate GetOrdinalDateOn(DayNumber dayNumber)
        {
            Domain.Validate(dayNumber);
            var ydoy = Schema.GetOrdinalParts(dayNumber - Epoch);
            return new OrdinalDate(ydoy, Id);
        }

        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "Use GetCalendarDate() then ToDayNumber() or better ToCalendarDay().")]
        DayNumber ICalendar.GetDayNumberOn(int year, int month, int day)
        {
            // Hidden since we have a custom day object (CalendarDay).
            // The idea is that one should always create an object using its
            // "natural" parts.
            Scope.ValidateYearMonthDay(year, month, day);
            return Epoch + Schema.CountDaysSinceEpoch(year, month, day);
        }

        [Pure]
        [SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "Use GetOrdinalDate() then ToDayNumber() or better ToCalendarDay().")]
        DayNumber ICalendar.GetDayNumberOn(int year, int dayOfYear)
        {
            // Hidden since we have a custom day object (CalendarDay).
            // The idea is that one should always create an object using its
            // "natural" parts.
            Scope.ValidateOrdinal(year, dayOfYear);
            return Epoch + Schema.CountDaysSinceEpoch(year, dayOfYear);
        }
    }

    public partial class Calendar // Internal helpers
    {
        // Internal helpers for use by the calendrical objects.
        // - GetDayNumber(...) tiny optimization to avoid having to look up the
        //   calendar multiple times.
        // - GetDayOfWeek(...) because of _epochDayOfWeek.

        /// <summary>
        /// Checks that the specified <paramref name="daysSinceEpoch"/> does not overflow the range
        /// of supported values for the number of consecutive days since the epoch.
        /// </summary>
        /// <exception cref="OverflowException">The operation would overflow the range of supported
        /// dates.</exception>
        internal void CheckOverflow(int daysSinceEpoch)
        {
            if (daysSinceEpoch < MinDaysSinceEpoch || daysSinceEpoch > MaxDaysSinceEpoch)
            {
                Throw.DateOverflow();
            }
        }

        /// <summary>
        /// Validates the specified day of the month.
        /// <para>This method does NOT validate <paramref name="y"/>.</para>
        /// <para>This method does NOT validate <paramref name="m"/>.</para>
        /// </summary>
        /// <exception cref="OverflowException">The operation would overflow the capacity of
        /// <see cref="Int32"/>.</exception>
        /// <exception cref="AoorException">The validation failed.</exception>
        internal void ValidateDayOfMonth(int y, int m, int dayOfMonth, string? paramName = null)
        {
            if (dayOfMonth < 1
                || (dayOfMonth > Schema.MinDaysInMonth
                    && dayOfMonth > Schema.CountDaysInMonth(y, m)))
            {
                Throw.ArgumentOutOfRange(paramName ?? nameof(dayOfMonth));
            }
        }

        /// <summary>
        /// Converts a date to a day number.
        /// </summary>
        [Pure]
        internal DayNumber GetDayNumber(CalendarDate date)
        {
            Debug.Assert(date.Cuid == Id);

            date.Parts.Unpack(out int y, out int m, out int d);
            return Epoch + Schema.CountDaysSinceEpoch(y, m, d);
        }

        /// <summary>
        /// Converts an ordinal date to a day number.
        /// </summary>
        [Pure]
        internal DayNumber GetDayNumber(OrdinalDate date)
        {
            Debug.Assert(date.Cuid == Id);

            date.Parts.Unpack(out int y, out int doy);
            return Epoch + Schema.CountDaysSinceEpoch(y, doy);
        }

        /// <summary>
        /// Obtains the day of the week of the specified date.
        /// <para>This method MUST be overriden by proleptic calendars.</para>
        /// </summary>
        [Pure]
        internal virtual DayOfWeek GetDayOfWeek(CalendarDate date)
        {
            // NB: if we disallow proleptic user-defined calendars, we can use
            // the unsigned modulo operator; idem with the other GetDayOfWeek().
            Debug.Assert(date.Cuid == Id);
            //Debug.Assert(!IsProleptic);

            date.Parts.Unpack(out int y, out int m, out int d);
            int days = _epochDayOfWeek + Schema.CountDaysSinceEpoch(y, m, d);
            //return (DayOfWeek)((uint)days % CalendricalConstants.DaysInWeek);
            return (DayOfWeek)MathZ.Modulo(days, CalendricalConstants.DaysInWeek);
        }

        /// <summary>
        /// Obtains the day of the week of the specified ordinal date.
        /// <para>This method MUST be overriden by proleptic calendars.</para>
        /// </summary>
        [Pure]
        internal virtual DayOfWeek GetDayOfWeek(OrdinalDate date)
        {
            Debug.Assert(date.Cuid == Id);

            date.Parts.Unpack(out int y, out int doy);
            int days = _epochDayOfWeek + Schema.CountDaysSinceEpoch(y, doy);
            return (DayOfWeek)MathZ.Modulo(days, CalendricalConstants.DaysInWeek);
        }
    }
}
