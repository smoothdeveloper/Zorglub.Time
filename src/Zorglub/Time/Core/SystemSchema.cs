﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Core
{
    using Zorglub.Time.Core.Arithmetic;
    using Zorglub.Time.Core.Intervals;

    // Annotations
    // -----------
    // A : abstract
    // + : not part of ICalendricalSchema but of ICalendricalSchemaPlus
    // * : neither part of ICalendricalSchema nor ICalendricalSchemaPlus

    /// <summary>
    /// Represents a system schema and provides a base for derived classes.
    /// <para>This class can ONLY be initialized from within friend assemblies.</para>
    /// </summary>
    /// <remarks>
    /// <para>All results SHOULD be representable by the system; see <see cref="Yemoda"/> and
    /// <see cref="Yedoy"/>.</para>
    /// </remarks>
    public abstract partial class SystemSchema : CalendricalSchema, ICalendricalPartsFactory
    {
        // Les limites MinMinYear/MaxMaxYear ont été fixées afin de pouvoir
        // utiliser Yemoda & co, mais aussi d'éviter des dépassements
        // arithmétiques. Sans cela on pourrait parfois aller beaucoup plus loin
        // (à condition de rester dans les limites de Int32), d'où l'interface
        // ICalendricalSchema permettant de définir des schémas dépourvus des
        // contraintes liées à Yemoda & co.
        // Il est à noter qu'en pratique on peut très bien ignorer ces derniers.
        // Ceci est plus important qu'il n'y paraît, car Yemoda impose aussi des
        // limites sur les mois et les jours, ce qui pourrait être gênant si on
        // décide d'écrire des schémas pour les calendriers chinois ou maya.
        //
        // par défaut, on n'utilise pas Yemoda.Min/MaxYear: les valeurs sont trop
        // grandes et nous obligerait à utiliser des Int64 pour effectuer un
        // certain nombre de calculs.
        //
        // Enfin, ces limites sont tout à fait théoriques. Un schéma n'est pas
        // un calendrier. Pour ces derniers, on utilisera des valeurs bien
        // inférieures (voir scopes).

        /// <summary>
        /// Represents the default value for the earliest supported year.
        /// <para>This field is a constant equal to -999_998.</para>
        /// </summary>
        private protected const int DefaultMinYear = -999_998;

        /// <summary>
        /// Represents the default value for the latest supported year.
        /// <para>This field is a constant equal to 999_999.</para>
        /// </summary>
        private protected const int DefaultMaxYear = 999_999;

        /// <summary>
        /// Represents the interval [-999_998..999_999].
        /// <para>It is also the default value for <see cref="ICalendricalKernel.SupportedYears"/>.</para>
        /// <para>This field is read-only.</para>
        /// </summary>
        public static readonly Range<int> DefaultSupportedYears = new(DefaultMinYear, DefaultMaxYear);

        /// <summary>
        /// Represents the interval [<see cref="Yemoda.MinYear"/>..<see cref="Yemoda.MaxYear"/>],
        /// that is [-2_097_152, 2_097_151].
        /// <para>It matches the value of <see cref="Yemoda.SupportedYears"/>.</para>
        /// <para>This field is read-only.</para>
        /// </summary>
        public static readonly Range<int> MaxSupportedYears = Yemoda.SupportedYears;

        /// <summary>
        /// Called from constructors in derived classes to initialize the
        /// <see cref="SystemSchema"/> class.
        /// </summary>
        /// <remarks>
        /// <para>All methods MUST work with negative years.</para>
        /// <para>All methods MUST work with years in <see cref="DefaultSupportedYears"/>.</para>
        /// </remarks>
        /// <exception cref="AoorException"><paramref name="minDaysInYear"/> or
        /// <paramref name="minDaysInMonth"/> is a negative integer.</exception>
        private protected SystemSchema(int minDaysInYear, int minDaysInMonth)
            : this(DefaultSupportedYears, minDaysInYear, minDaysInMonth) { }

        /// <summary>
        /// Called from constructors in derived classes to initialize the
        /// <see cref="SystemSchema"/> class.
        /// </summary>
        /// <exception cref="AoorException"><paramref name="supportedYears"/> is NOT a subinterval
        /// of <see cref="Yemoda.SupportedYears"/>.</exception>
        /// <exception cref="AoorException"><paramref name="minDaysInYear"/> or
        /// <paramref name="minDaysInMonth"/> is a negative integer.</exception>
        private protected SystemSchema(Range<int> supportedYears, int minDaysInYear, int minDaysInMonth)
            : base(supportedYears, minDaysInYear, minDaysInMonth)
        {
            if (supportedYears.IsSubsetOf(Yemoda.SupportedYears) == false)
            {
                Throw.ArgumentOutOfRange(nameof(supportedYears));
            }
        }

        private Range<int> _supportedYearsCore = Range.Maximal32;
        /// <summary>
        /// Gets the core domain, the interval of years for which the <i>core</i> methods are known
        /// not to overflow.
        /// </summary>
        /// <remarks>
        /// <para>The core methods are those inherited from <see cref="ICalendricalKernel"/>.</para>
        /// <para>The default value is equal to the whole range of 32-bit signed integers.</para>
        /// <para>For methods expecting a month or day parameters, we assume that they are within
        /// the ranges defined by <see cref="Yemoda"/>.</para>
        /// <para>See also <seealso cref="ICalendricalPreValidator"/>.</para>
        /// </remarks>
        public Range<int> SupportedYearsCore
        {
            get => _supportedYearsCore;
            protected init
            {
                if (value.IsSupersetOf(SupportedYears) == false)
                {
                    Throw.Argument(nameof(value));
                }
                _supportedYearsCore = value;
            }
        }
    }

    // Misc.
    //   PreValidator
    //   Arithmetic
    // * Profile
    //   IsRegular()
    // * GetPreValidatorCore()
    // * GetArithmeticCore()
    // * TryGetCustomPreValidator()
    // * TryGetCustomArithmetic()
    public partial class SystemSchema // Misc
    {
        /// <inheritdoc />
        [Pure]
        public sealed override bool IsRegular(out int monthsInYear)
        {
            if (this is IRegularSchema sch)
            {
                monthsInYear = sch.MonthsInYear;
                return true;
            }
            else
            {
                monthsInYear = 0;
                return false;
            }
        }

        /// <summary>
        /// Obtains the <see cref="ICalendricalArithmetic"/> for this schema.
        /// </summary>
        [Pure]
        protected override ICalendricalArithmetic GetArithmeticCore() =>
            TryGetCustomArithmetic(out ICalendricalArithmetic? arithmetic)
            ? arithmetic
            : new DefaultArithmetic(this);

        /// <summary>
        /// Returns true if the construction of a specialized arithmetic engine for this schema was
        /// successful; otherwise returns false; the result is given in an output parameter.
        /// </summary>
        [Pure]
        protected internal bool TryGetCustomArithmetic(
            [NotNullWhen(true)] out ICalendricalArithmetic? arithmetic)
        {
            if (MinDaysInMonth >= FastArithmetic.MinMinDaysInMonth)
            {
                arithmetic = FastArithmetic.Create(this);
                return true;
            }
            else
            {
                arithmetic = null;
                return false;
            }
        }
    }

    // Properties.
    //   Algorithm
    // A Family
    // A PeriodicAdjustments
    //   SupportedYears
    // * SupportedYearsCore
    // * Domain
    // * MinMaxDateParts
    // * MinMaxOrdinalParts
    //   MinDaysInYear
    // * MinDaysInMonth
    public partial class SystemSchema // Properties
    {
        private OrderedPair<Yemoda>? _minMaxDateParts;
        /// <summary>
        /// Gets the pair of date parts of the first day of the first supported year and the last
        /// day of the last supported year.
        /// </summary>
        public OrderedPair<Yemoda> MinMaxDateParts =>
            _minMaxDateParts ??=
                SupportedYears.Endpoints.Select(GetStartOfYearParts, GetEndOfYearParts);

        private OrderedPair<Yedoy>? _minMaxOrdinalParts;
        /// <summary>
        /// Gets the pair of ordinal date parts of the first day of the first supported year and the
        /// last day of the last supported year.
        /// </summary>
        public OrderedPair<Yedoy> MinMaxOrdinalParts =>
            _minMaxOrdinalParts ??=
                SupportedYears.Endpoints.Select(GetStartOfYearOrdinalParts, GetEndOfYearOrdinalParts);
    }

    // Year, month or day infos.
    // A IsLeapYear(y)
    // A IsIntercalaryMonth(y, m)
    // A IsIntercalaryDay(y, m, d)
    // A IsSupplementaryDay(y, m, d)
    public partial class SystemSchema // Year, month or day infos
    { }

    // Counting months and days within a year or a month.
    // A CountMonthsInYear(y)
    // A CountDaysInYear(y)
    // A CountDaysInYearBeforeMonth(y, m)
    //   CountDaysInYearAfterMonth(y, m)
    //   CountDaysInYearBefore(y, m, d)
    //   CountDaysInYearAfter(y, m, d)
    //   CountDaysInYearBefore(daysSinceEpoch)
    //   CountDaysInYearAfter(daysSinceEpoch)
    // A CountDaysInMonth(y, m)
    //   CountDaysInMonthBefore(y, m, d)
    //   CountDaysInMonthAfter(y, m, d)
    //   CountDaysInMonthBefore(daysSinceEpoch)
    //   CountDaysInMonthAfter(daysSinceEpoch)
    public partial class SystemSchema // Counting months and days within a year or a month
    { }

    // Conversions.
    //   CountDaysSinceEpoch(y, m, d)   -> daysSinceEpoch       (Yemoda)       => DaysSinceEpoch
    //   CountDaysSinceEpoch(y, doy)    -> daysSinceEpoch       (Yedoy)        => DaysSinceEpoch
    //   GetDateParts(daysSinceEpoch)   -> out y, m, d          -
    // + GetDateParts(daysSinceEpoch)   -> Yemoda               DaysSinceEpoch => Yemoda
    //   GetYear(daysSinceEpoch)        -> y, out doy           -
    // A GetYear(daysSinceEpoch)        -> y                    -
    // + GetOrdinalParts(daySinceEpoch) -> Yedoy                DaysSinceEpoch => Yedoy
    // A GetMonth(y, doy)               -> m, out d             -
    // + GetDateParts(y, doy)           -> Yemoda               (Yedoy)        => Yemoda
    //   GetDayOfYear(y, m, d)          -> doy                  -
    // + GetOrdinalParts(y, m, d)       -> Yedoy                (Yemoda)       => Yedoy
    //
    // IMPORTANT: We "duplicate" GetDateParts() and GetOrdinalParts() to allow
    // derived classes to be created outside this assembly.
    public partial class SystemSchema // Conversions
    {
        /// <inheritdoc />
        [Pure]
        public override int GetYear(int daysSinceEpoch, out int doy)
        {
            int y = GetYear(daysSinceEpoch);
            doy = 1 + daysSinceEpoch - GetStartOfYear(y);
            return y;
        }

        /// <summary>
        /// Obtains the year from the specified day count (the number of consecutive days from the
        /// epoch to a date).
        /// </summary>
        // Partial form of the other GetYear(). One can also say that it's a
        // partial form of GetDateParts(), but this method usually delegates
        // part of its work to GetOrdinalParts().
        [Pure] public abstract int GetYear(int daysSinceEpoch);

        /// <inheritdoc />
        /// <remarks>
        /// <para>See also
        /// <seealso cref="ICalendricalSchema.GetDateParts(int, out int, out int, out int)"/>.</para>
        /// </remarks>
        [Pure]
        // CIL code size = 22 bytes <= 32 bytes.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Yemoda GetDateParts(int daysSinceEpoch)
        {
            GetDateParts(daysSinceEpoch, out int y, out int m, out int d);
            return new Yemoda(y, m, d);
        }

        /// <inheritdoc />
        /// <remarks>
        /// <para>See also
        /// <seealso cref="ICalendricalSchema.GetYear(int, out int)"/>.</para>
        /// </remarks>
        [Pure]
        // CIL code size = 19 bytes <= 32 bytes.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Yedoy GetOrdinalParts(int daysSinceEpoch)
        {
            int y = GetYear(daysSinceEpoch, out int doy);
            return new Yedoy(y, doy);
        }

        /// <inheritdoc />
        /// <remarks>
        /// <para>See also
        /// <seealso cref="ICalendricalSchema.GetMonth(int, int, out int)"/>.</para>
        /// </remarks>
        [Pure]
        // CIL code size = 21 bytes <= 32 bytes.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Yemoda GetDateParts(int y, int doy)
        {
            int m = GetMonth(y, doy, out int d);
            return new Yemoda(y, m, d);
        }

        /// <inheritdoc />
        // CIL code size = 18 bytes <= 32 bytes.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Yedoy GetOrdinalParts(int y, int m, int d)
        {
            int doy = GetDayOfYear(y, m, d);
            return new Yedoy(y, doy);
        }
    }

    // Dates in a given year or month.
    // A GetStartOfYear(y)                      -> daysSinceEpoch
    // + GetStartOfYearParts(y)                 -> Yemoda
    // + GetStartOfYearOrdinalParts(y)          -> Yedoy
    //
    //   GetEndOfYear(y)                        -> daysSinceEpoch
    // A GetEndOfYearParts(y)                   -> out m, d
    // +   ...                                  -> Yemoda
    // + GetEndOfYearOrdinalParts(y)            -> Yedoy
    //
    //   GetStartOfMonth(y, m)                  -> daysSinceEpoch
    // + GetStartOfMonthParts(y, m)             -> Yemoda
    // + GetStartOfMonthOrdinalParts(y, m)      -> Yedoy
    //
    //   GetEndOfMonth(y, m)                    -> daysSinceEpoch
    // + GetEndOfMonthParts(y, m)               -> Yemoda
    // + GetEndOfMonthOrdinalParts(y, m)        -> Yedoy
    //
    // IMPORTANT: We "duplicate" GetEndOfYearParts() to allow derived classes to
    // be created outside this assembly.
    public partial class SystemSchema // Dates in a given year or month
    {
        /// <inheritdoc />
        [Pure]
        public Yemoda GetStartOfYearParts(int y) => Yemoda.AtStartOfYear(y);

        /// <inheritdoc />
        [Pure]
        public Yedoy GetStartOfYearOrdinalParts(int y) => Yedoy.AtStartOfYear(y);

        /// <inheritdoc />
        [Pure]
        public Yemoda GetEndOfYearParts(int y)
        {
            GetEndOfYearParts(y, out int m, out int d);
            return new Yemoda(y, m, d);
        }

        /// <summary>
        /// Obtains the month and day of the month for the last day of the specified year; the
        /// results are given in output parameters.
        /// </summary>
        // The default implementation
        // > m = CountMonthsInYear(y);
        // > d = CountDaysInMonth(y, m);
        // is rather inefficient, indeed "m" is often a constant. For instance,
        // for regular schemas, we can write:
        // > m = MonthsInYear;
        // > d = CountDaysInMonth(y, MonthsInYear);
        public abstract void GetEndOfYearParts(int y, out int m, out int d);

        /// <inheritdoc />
        [Pure]
        public Yedoy GetEndOfYearOrdinalParts(int y)
        {
            int doy = CountDaysInYear(y);
            return new Yedoy(y, doy);
        }

        /// <inheritdoc />
        [Pure]
        public Yemoda GetStartOfMonthParts(int y, int m) => Yemoda.AtStartOfMonth(y, m);

        /// <inheritdoc />
        [Pure]
        public Yedoy GetStartOfMonthOrdinalParts(int y, int m)
        {
            int doy = CountDaysInYearBeforeMonth(y, m) + 1;
            return new Yedoy(y, doy);
        }

        /// <summary>
        /// Obtains the date parts for the last day of the specified month.
        /// </summary>
        [Pure]
        public Yemoda GetEndOfMonthParts(int y, int m)
        {
            int d = CountDaysInMonth(y, m);
            return new Yemoda(y, m, d);
        }

        /// <inheritdoc />
        [Pure]
        public Yedoy GetEndOfMonthOrdinalParts(int y, int m)
        {
            int doy = CountDaysInYearBeforeMonth(y, m) + CountDaysInMonth(y, m);
            return new Yedoy(y, doy);
        }
    }
}
