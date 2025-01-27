﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Core.Schemas
{
    /// <summary>
    /// Represents the French Republican schema; alternative form using a virtual
    /// month to hold the epagomenal days, see also
    /// <seealso cref="FrenchRepublican12Schema"/>.
    /// <para>This class cannot be inherited.</para>
    /// <para>This class can ONLY be initialized from within friend assemblies.
    /// </para>
    /// </summary>
    public sealed partial class FrenchRepublican13Schema :
        FrenchRepublicanSchema,
        IEpagomenalFeaturette,
        IVirtualMonthFeaturette,
        IDaysInMonthDistribution,
        IBoxable<FrenchRepublican13Schema>
    {
        /// <summary>
        /// Represents the number of months in a year.
        /// <para>This field is a constant equal to 13.</para>
        /// </summary>
        private const int MonthsPerYear = 13;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrenchRepublican13Schema"/>
        /// class.
        /// </summary>
        internal FrenchRepublican13Schema() : base(5) { }

        /// <inheritdoc />
        public sealed override int MonthsInYear => MonthsPerYear;

        /// <inheritdoc />
        public static int VirtualMonth => 13;

        /// <summary>
        /// Creates a new (boxed) instance of the
        /// <see cref="FrenchRepublican13Schema"/> class.
        /// </summary>
        [Pure]
        public static Box<FrenchRepublican13Schema> GetInstance() =>
            Box.Create(new FrenchRepublican13Schema());

        /// <inheritdoc />
        [Pure]
        static ReadOnlySpan<byte> IDaysInMonthDistribution.GetDaysInMonthDistribution(bool leap) =>
            leap
            ? new byte[13] { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 6 }
            : new byte[13] { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 5 };
    }

    public partial class FrenchRepublican13Schema // Year, month or day infos.
    {
        /// <inheritdoc />
        [Pure]
        public static bool IsEpagomenalDay(int y, int m, int d, out int epagomenalNumber) =>
            Thirteen.IsEpagomenalDay(m, d, out epagomenalNumber);

        /// <inheritdoc />
        [Pure]
        public sealed override bool IsIntercalaryDay(int y, int m, int d) =>
            Thirteen.IsIntercalaryDay(m, d);

        /// <inheritdoc />
        [Pure]
        public sealed override bool IsSupplementaryDay(int y, int m, int d) =>
            Thirteen.IsSupplementaryDay(m);
    }

    public partial class FrenchRepublican13Schema // Counting months and days within a year or a month.
    {
        /// <inheritdoc />
        [Pure]
        public sealed override int CountMonthsInYear(int y) => MonthsPerYear;

        /// <inheritdoc />
        [Pure]
        public sealed override int CountDaysInMonth(int y, int m) =>
            Thirteen.CountDaysInMonth(IsLeapYear(y), m);
    }

    public partial class FrenchRepublican13Schema // Conversions.
    {
        /// <inheritdoc />
        [Pure]
        public sealed override void GetDateParts(int daysSinceEpoch, out int y, out int m, out int d)
        {
            y = GetYear(daysSinceEpoch, out int doy);
            m = Thirteen.GetMonth(doy - 1, out d);
        }

        /// <inheritdoc />
        [Pure]
        public sealed override int GetMonth(int y, int doy, out int d) =>
            Thirteen.GetMonth(doy - 1, out d);
    }

    public partial class FrenchRepublican13Schema // Dates in a given year or month.
    {
        /// <inheritdoc />
        public sealed override void GetEndOfYearParts(int y, out int m, out int d) =>
            Thirteen.GetEndOfYearParts(IsLeapYear(y), out m, out d);
    }
}
