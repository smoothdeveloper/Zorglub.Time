﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Simple
{
    using Zorglub.Time.Core;
    using Zorglub.Time.Core.Intervals;

    // REVIEW(api): Subtract(), add Add(Period). NextMonth() & co. Week ops. Quarter ops.

    // Only handle calendrical objects related to the Calendar system; for other
    // systems, see CalendricalMath.

    /// <summary>
    /// Defines the non-standard mathematical operations suitable for use with a given calendar and
    /// provides a base for derived classes.
    /// </summary>
    public abstract partial class CalendarMath
    {
        /// <summary>
        /// Called from constructors in derived classes to initialize the <see cref="CalendarMath"/>
        /// class.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="calendar"/> is null.</exception>
        protected CalendarMath(Calendar calendar, AddAdjustment adjustment)
        {
            Requires.NotNull(calendar);
            if (adjustment.IsInvalid()) Throw.ArgumentOutOfRange(nameof(adjustment));

            AddAdjustment = adjustment;
            Cuid = calendar.Id;
            Schema = calendar.Schema;
            // Internally we use YearOverflowChecker. Externally, one can use
            // SupportedYears instead.
            SupportedYears = calendar.SupportedYears;
            YearOverflowChecker = calendar.YearOverflowChecker;
        }

        /// <summary>
        /// Gets the strategy employed to resolve ambiguities that can occur after adding a number
        /// of months or years to a date.
        /// </summary>
        public AddAdjustment AddAdjustment { get; }

        /// <summary>
        /// Gets the range of supported years.
        /// </summary>
        protected Range<int> SupportedYears { get; }

        /// <summary>
        /// Gets the checker for overflows of the range of years.
        /// </summary>
        private protected IOverflowChecker<int> YearOverflowChecker { get; }

        /// <summary>
        /// Gets the calendrical schema.
        /// </summary>
        protected SystemSchema Schema { get; }

        /// <summary>
        /// Gets the ID of the underlying calendar.
        /// </summary>
        internal Cuid Cuid { get; }

        /// <summary>
        /// Creates the default instance of the <see cref="CalendarMath"/> class for the specified
        /// calendar.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="calendar"/> is null.</exception>
        [Pure]
        internal static CalendarMath Create(Calendar calendar)
        {
            Requires.NotNull(calendar);

            // This method could be public, but it would feel odd, indeed this
            // class has a single public method/prop: AddAdjustment.

            // The schema is not regular iff monthsInYear = 0.
            int monthsInYear = calendar.IsRegular(out int v) ? v : 0;

            return monthsInYear switch
            {
                12 => new Regular12Math(calendar),
                13 => new Regular13Math(calendar),
                > 0 => new RegularMath(calendar),
                _ => new DefaultMath(calendar)
            };
        }

        /// <summary>
        /// Validates the specified <see cref="Simple.Cuid"/>.
        /// </summary>
        /// <exception cref="ArgumentException">The validation failed.</exception>
        private void ValidateCuid(Cuid cuid, string paramName)
        {
            if (cuid != Cuid) Throw.BadCuid(paramName, Cuid, cuid);
        }
    }

    public partial class CalendarMath // CalendarDate
    {
        #region Public methods

        /// <summary>
        /// Adds a number of years to the year field of the specified date.
        /// </summary>
        /// <exception cref="ArgumentException"><paramref name="date"/> does not belong to the
        /// underlying calendar.</exception>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported dates.</exception>
        [Pure]
        public CalendarDate AddYears(CalendarDate date, int years)
        {
            ValidateCuid(date.Cuid, nameof(date));
            return AddYearsCore(date, years);
        }

        /// <summary>
        /// Adds a number of months to the month field of the specified date.
        /// </summary>
        /// <exception cref="ArgumentException"><paramref name="date"/> does not belong to the
        /// underlying calendar.</exception>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported dates.</exception>
        [Pure]
        public CalendarDate AddMonths(CalendarDate date, int months)
        {
            ValidateCuid(date.Cuid, nameof(date));
            return AddMonthsCore(date, months);
        }

        /// <summary>
        /// Counts the number of years between the two specified dates.
        /// </summary>
        /// <exception cref="ArgumentException">One of the paramaters does not belong to the
        /// underlying calendar.</exception>
        [Pure]
        public int CountYearsBetween(CalendarDate start, CalendarDate end)
        {
            ValidateCuid(start.Cuid, nameof(start));
            ValidateCuid(end.Cuid, nameof(end));
            return CountYearsBetweenCore(start, end);
        }

        /// <summary>
        /// Counts the number of months between the two specified dates.
        /// </summary>
        /// <exception cref="ArgumentException">One of the paramaters does not belong to the
        /// underlying calendar.</exception>
        [Pure]
        public int CountMonthsBetween(CalendarDate start, CalendarDate end)
        {
            ValidateCuid(start.Cuid, nameof(start));
            ValidateCuid(end.Cuid, nameof(end));
            return CountMonthsBetweenCore(start, end);
        }

        #endregion
        #region Abstract methods

        /// <summary>
        /// Adds a number of years to the year field of the specified date.
        /// <para>This method does NOT validate the date parameter.</para>
        /// </summary>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported dates.</exception>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "date", Justification = "VB.NET Date.")]
        protected internal abstract CalendarDate AddYearsCore(CalendarDate date, int years);

        /// <summary>
        /// Adds a number of months to the months field of the specified date.
        /// <para>This method does NOT validate the date parameter.</para>
        /// </summary>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported dates.</exception>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "date", Justification = "VB.NET Date.")]
        protected internal abstract CalendarDate AddMonthsCore(CalendarDate date, int months);

        /// <summary>
        /// Counts the number of months between the two specified dates.
        /// <para>This method does NOT validate its parameters.</para>
        /// </summary>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "end", Justification = "F# & VB.NET End statement.")]
        protected internal abstract int CountYearsBetweenCore(CalendarDate start, CalendarDate end);

        /// <summary>
        /// Counts the number of months between the two specified dates.
        /// <para>This method does NOT validate its parameters.</para>
        /// </summary>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "end", Justification = "F# & VB.NET End statement.")]
        protected internal abstract int CountMonthsBetweenCore(CalendarDate start, CalendarDate end);

        #endregion
    }

    public partial class CalendarMath // OrdinalDate
    {
        #region Public methods

        /// <summary>
        /// Adds a number of years to the year field of the specified ordinal date.
        /// </summary>
        /// <exception cref="ArgumentException"><paramref name="date"/> does not belong to the
        /// underlying calendar.</exception>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported dates.</exception>
        [Pure]
        public OrdinalDate AddYears(OrdinalDate date, int years)
        {
            ValidateCuid(date.Cuid, nameof(date));
            return AddYearsCore(date, years);
        }

        /// <summary>
        /// Counts the number of years between the two specified ordinal dates.
        /// </summary>
        /// <exception cref="ArgumentException">One of the paramaters does not belong to the
        /// underlying calendar.</exception>
        [Pure]
        public int CountYearsBetween(OrdinalDate start, OrdinalDate end)
        {
            ValidateCuid(start.Cuid, nameof(start));
            ValidateCuid(end.Cuid, nameof(end));
            return CountYearsBetweenCore(start, end);
        }

        #endregion
        #region Abstract methods

        /// <summary>
        /// Adds a number of years to the year field of the specified ordinal date.
        /// <para>This method does NOT validate the date parameter.</para>
        /// </summary>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported dates.</exception>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "date", Justification = "VB.NET Date.")]
        protected internal abstract OrdinalDate AddYearsCore(OrdinalDate date, int years);

        /// <summary>
        /// Counts the number of months between the two specified ordinal dates.
        /// <para>This method does NOT validate its parameters.</para>
        /// </summary>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "end", Justification = "F# & VB.NET End statement.")]
        protected internal abstract int CountYearsBetweenCore(OrdinalDate start, OrdinalDate end);

        #endregion
    }

    public partial class CalendarMath // CalendarMonth
    {
        #region Public methods

        /// <summary>
        /// Adds a number of years to the year field of the specified month.
        /// </summary>
        /// <exception cref="ArgumentException"><paramref name="month"/> does not belong to the
        /// underlying calendar.</exception>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported months.</exception>
        [Pure]
        public CalendarMonth AddYears(CalendarMonth month, int years)
        {
            ValidateCuid(month.Cuid, nameof(month));
            return AddYearsCore(month, years);
        }

        /// <summary>
        /// Adds a number of months to the month field of the specified month.
        /// </summary>
        /// <exception cref="ArgumentException"><paramref name="month"/> does not belong to the
        /// underlying calendar.</exception>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported months.</exception>
        [Pure]
        public CalendarMonth AddMonths(CalendarMonth month, int months)
        {
            ValidateCuid(month.Cuid, nameof(month));
            return AddMonthsCore(month, months);
        }

        /// <summary>
        /// Counts the number of years between the two specified months.
        /// </summary>
        /// <exception cref="ArgumentException">One of the paramaters does not belong to the
        /// underlying calendar.</exception>
        [Pure]
        public int CountYearsBetween(CalendarMonth start, CalendarMonth end)
        {
            ValidateCuid(start.Cuid, nameof(start));
            ValidateCuid(end.Cuid, nameof(end));
            return CountYearsBetweenCore(start, end);
        }

        /// <summary>
        /// Counts the number of months between the two specified months.
        /// </summary>
        /// <exception cref="ArgumentException">One of the paramaters does not belong to the
        /// underlying calendar.</exception>
        [Pure]
        public int CountMonthsBetween(CalendarMonth start, CalendarMonth end)
        {
            ValidateCuid(start.Cuid, nameof(start));
            ValidateCuid(end.Cuid, nameof(end));
            return CountMonthsBetweenCore(start, end);
        }

        #endregion
        #region Abstract methods

        /// <summary>
        /// Adds a number of years to the year field of the specified month.
        /// <para>This method does NOT validate the date parameter.</para>
        /// </summary>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported months.</exception>
        protected internal abstract CalendarMonth AddYearsCore(CalendarMonth month, int years);

        /// <summary>
        /// Adds a number of months to the month field of the specified month.
        /// <para>This method does NOT validate the month parameter.</para>
        /// </summary>
        /// <exception cref="OverflowException">The calculation would overflow the range of
        /// supported months.</exception>
        protected internal abstract CalendarMonth AddMonthsCore(CalendarMonth month, int months);

        /// <summary>
        /// Counts the number of years between the two specified months.
        /// <para>This method does NOT validate its parameters.</para>
        /// </summary>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "end", Justification = "F# & VB.NET End statement.")]
        protected internal abstract int CountYearsBetweenCore(CalendarMonth start, CalendarMonth end);

        /// <summary>
        /// Counts the number of months between the two specified months.
        /// <para>This method does NOT validate its parameters.</para>
        /// </summary>
        [Pure]
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords.", MessageId = "end", Justification = "F# & VB.NET End statement.")]
        protected internal abstract int CountMonthsBetweenCore(CalendarMonth start, CalendarMonth end);

        #endregion
    }
}
