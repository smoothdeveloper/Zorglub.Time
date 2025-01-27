﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Simple.Specialized
{
    // Calendrier liturgique orthodoxe.
    //
    // Références :
    // - https://fr.wikipedia.org/wiki/Calendrier_chr%C3%A9tien
    // - https://fr.wikipedia.org/wiki/Calendrier_liturgique_orthodoxe
    // - https://en.wikipedia.org/wiki/Eastern_Orthodox_liturgical_calendar
    public sealed class OrthodoxKalendar
    {
        private readonly int _year;

        public OrthodoxKalendar(int year)
        {
            if (year <= 0) Throw.ArgumentOutOfRange(nameof(year));

            _year = year;
        }

        private CalendarDate? _easter;
        public CalendarDate Easter => _easter ??= InitEaster(_year);

        private CalendarDate? _paschalMoon;
        public CalendarDate PaschalMoon => _paschalMoon ??= InitPaschalMoon(_year);

        // GetPaschal ???
        // D.&.R (8.1) p.115, orthodox-easter()
        [Pure]
        private static CalendarDate InitEaster(int year)
        {
            var paschalMoon = InitPaschalMoon(year);
            return paschalMoon.Next(DayOfWeek.Sunday);
        }

        [Pure]
        private static CalendarDate InitPaschalMoon(int year)
        {
            int epact = GetEpact(year);
            return JulianCalendar.Instance.GetCalendarDate(year, 4, 19) - epact;
        }

        [Pure]
        private static int GetEpact(int year)
        {
            Debug.Assert(year >= 0);

            return (14 + 11 * (year % 19)) % 30;
        }
    }
}
