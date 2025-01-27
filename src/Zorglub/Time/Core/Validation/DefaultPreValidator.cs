﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Core.Validation
{
    /// <summary>
    /// Provides a plain implementation for <see cref="ICalendricalPreValidator"/>.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    internal sealed class DefaultPreValidator : ICalendricalPreValidator
    {
        /// <summary>
        /// Represents the minimum total number of days there is at least in a year.
        /// <para>This field is read-only.</para>
        /// </summary>
        private readonly int _minDaysInYear;

        /// <summary>
        /// Represents the schema.
        /// <para>This field is read-only.</para>
        /// </summary>
        private readonly ICalendricalSchema _schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPreValidator"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="schema"/> is null.</exception>
        public DefaultPreValidator(ICalendricalSchema schema)
        {
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
            _minDaysInYear = schema.MinDaysInYear;
        }

        /// <inheritdoc />
        public void ValidateMonth(int y, int month, string? paramName = null)
        {
            if (month < 1 || month > _schema.CountMonthsInYear(y))
            {
                Throw.MonthOutOfRange(month, paramName);
            }
        }

        /// <inheritdoc />
        public void ValidateMonthDay(int y, int month, int day, string? paramName = null)
        {
            if (month < 1 || month > _schema.CountMonthsInYear(y))
            {
                Throw.MonthOutOfRange(month, paramName);
            }
            if (day < 1 || day > _schema.CountDaysInMonth(y, month))
            {
                Throw.DayOutOfRange(day, paramName);
            }
        }

        /// <inheritdoc />
        public void ValidateDayOfYear(int y, int dayOfYear, string? paramName = null)
        {
            if (dayOfYear < 1
                || (dayOfYear > _minDaysInYear && dayOfYear > _schema.CountDaysInYear(y)))
            {
                Throw.DayOfYearOutOfRange(dayOfYear, paramName);
            }
        }
    }
}
