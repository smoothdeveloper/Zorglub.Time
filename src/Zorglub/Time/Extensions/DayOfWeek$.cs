﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="DayOfWeek"/>.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// Obtains the value of the specified day of the week as an ISO weekday number.
        /// </summary>
        /// <exception cref="AoorException">The specified day of the week is not valid.</exception>
        [Pure]
        public static int ToIsoWeekday(this DayOfWeek @this)
        {
            Requires.Defined(@this);

            return @this == DayOfWeek.Sunday ? 7 : (int)@this;
        }
    }
}
