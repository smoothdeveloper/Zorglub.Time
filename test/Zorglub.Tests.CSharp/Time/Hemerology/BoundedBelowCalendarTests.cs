﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Hemerology;

#if false
using System.Linq;
#endif

using global::Samples;

using Zorglub.Testing.Data.Unbounded;
using Zorglub.Time.Core.Schemas;

// Cf. aussi GregorianBoundedBelowCalendarTests.
public static class BoundedBelowCalendarTests
{
    // Exemple du calendrier grégorien qui débute officiellement le 15/10/1582.
    // En 1582, 3 mois, octobre à décembre.
    // En 1582, 78 jours = 17 (oct) + 30 (nov) + 31 (déc).

    [Fact]
    public static void CountMonthsInFirstYear()
    {
        // Act
        var chr = CalendarZoo.GenuineGregorian;
        int monthsInFirstYear = 3;
        // Assert
        Assert.Equal(monthsInFirstYear, chr.CountMonthsInYear(chr.SupportedYears.Min));
        Assert.Equal(monthsInFirstYear, chr.CountMonthsInFirstYear());
    }

    [Fact]
    public static void CountDaysInFirstYear()
    {
        // Act
        var chr = CalendarZoo.GenuineGregorian;
        int daysInFirstYear = 78;
        // Assert
        Assert.Equal(daysInFirstYear, chr.CountDaysInYear(chr.SupportedYears.Min));
        Assert.Equal(daysInFirstYear, chr.CountDaysInFirstYear());
    }

    [Fact]
    public static void CountDaysInFirstMonth()
    {
        // Act
        var chr = CalendarZoo.GenuineGregorian;
        int daysInFirstMonth = 17;
        var parts = chr.MinDateParts;
        // Assert
        Assert.Equal(daysInFirstMonth, chr.CountDaysInMonth(parts.Year, parts.Month));
        Assert.Equal(daysInFirstMonth, chr.CountDaysInFirstMonth());
    }
}

public sealed class GregorianBoundedBelowCalendarTests
    : NakedCalendarFacts<BoundedBelowCalendar, UnboundedGregorianDataSet>
{
    private const int FirstYear = -123_456;
    private const int FirstMonth = 4;
    private const int FirstDay = 5;

    public GregorianBoundedBelowCalendarTests()
        : base(MakeCalendar()) { }

    // On triche un peu, la date de début a été choisie de telle sorte que
    // les tests marchent... (cf. GregorianData).
    private static BoundedBelowCalendar MakeCalendar() =>
        new(
            "Gregorian",
            new GregorianSchema(),
            DayZero.NewStyle,
            FirstYear, FirstMonth, FirstDay);

    [Fact]
    public void MinYear_Prop() =>
        Assert.Equal(FirstYear, CalendarUT.SupportedYears.Min);

    [Fact]
    public void MaxYear_Prop() =>
        Assert.Equal(CalendarUT.Schema.SupportedYears.Max, CalendarUT.SupportedYears.Max);

    [Fact]
    public void MinDateParts_Prop()
    {
        var parts = new DateParts(FirstYear, FirstMonth, FirstDay);
        // Act
        Assert.Equal(parts, CalendarUT.MinDateParts);
    }

#if false
    [Fact]
    public void GetDaysInYear_FirstYear()
    {
        DayNumber startOfYear = CalendarUT.MinDayNumber;
        DayNumber endOfYear = DayCalendarUT.GetEndOfYear(FirstYear);
        int daysInFirstYear = CalendarUT.CountDaysInFirstYear();
        IEnumerable<DayNumber> list =
            from i in Enumerable.Range(0, daysInFirstYear)
            select startOfYear + i;
        // Act
        IEnumerable<DayNumber> actual = DayCalendarUT.GetDaysInYear(FirstYear);
        // Assert
        Assert.Equal(list, actual);
        Assert.Equal(daysInFirstYear, actual.Count());
        Assert.Equal(startOfYear, actual.First());
        Assert.Equal(endOfYear, actual.Last());
    }

    [Fact]
    public void GetDaysInMonth_FirstMonth()
    {
        DayNumber startofMonth = CalendarUT.MinDayNumber;
        DayNumber endOfMonth = DayCalendarUT.GetEndOfMonth(FirstYear, FirstMonth);
        int daysInFirstMonth = CalendarUT.CountDaysInFirstMonth();
        IEnumerable<DayNumber> list =
            from i in Enumerable.Range(0, daysInFirstMonth)
            select startofMonth + i;
        // Act
        IEnumerable<DayNumber> actual = DayCalendarUT.GetDaysInMonth(FirstYear, FirstMonth);
        // Assert
        Assert.Equal(list, actual);
        Assert.Equal(daysInFirstMonth, actual.Count());
        Assert.Equal(startofMonth, actual.First());
        Assert.Equal(endOfMonth, actual.Last());
    }
#endif

    [Fact]
    public void GetStartOfYear_InvalidFirstYear() =>
        Assert.ThrowsAoorexn("year", () => CalendarUT.GetStartOfYear(FirstYear));

    //[Fact]
    //public void GetStartOfYearFields_InvalidFirstYear() =>
    //    Assert.ThrowsAoorexn("year", () => CalendarUT.GetStartOfYear(FirstYear));

    [Fact]
    public void GetStartOfMonth_InvalidFirstMonth() =>
        Assert.ThrowsAoorexn("month", () => CalendarUT.GetStartOfMonth(FirstYear, FirstMonth));

    //[Fact]
    //public void GetStartOfMonthFields_InvalidFirstMonth() =>
    //    Assert.ThrowsAoorexn("month", () => CalendarUT.GetStartOfMonth(FirstYear, FirstMonth));

    [Fact]
    public void CountMonthsInFirstYear()
    {
        // Act
        int monthsInFirstYear = 12 - (FirstMonth - 1);
        // Assert
        Assert.Equal(monthsInFirstYear, CalendarUT.CountMonthsInYear(FirstYear));
        Assert.Equal(monthsInFirstYear, CalendarUT.CountMonthsInFirstYear());
    }

    [Fact]
    public void CountDaysInFirstYear()
    {
        // Act
        var schema = CalendarUT.Schema;
        int daysInFirstYear = schema.CountDaysInYear(FirstYear)
            - schema.CountDaysInYearBeforeMonth(FirstYear, FirstMonth)
            - (FirstDay - 1);
        // Assert
        Assert.Equal(daysInFirstYear, CalendarUT.CountDaysInYear(FirstYear));
        Assert.Equal(daysInFirstYear, CalendarUT.CountDaysInFirstYear());
    }

    [Fact]
    public void CountDaysInFirstMonth()
    {
        // Act
        var schema = CalendarUT.Schema;
        int daysInFirstMonth = schema.CountDaysInMonth(FirstYear, FirstMonth) - (FirstDay - 1);
        // Assert
        Assert.Equal(daysInFirstMonth, CalendarUT.CountDaysInMonth(FirstYear, FirstMonth));
        Assert.Equal(daysInFirstMonth, CalendarUT.CountDaysInFirstMonth());
    }
}
