﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Facts.Hemerology;

using Zorglub.Testing.Data;
using Zorglub.Time.Core.Intervals;
using Zorglub.Time.Hemerology;

// Hypothesis: years and months are complete.

// In addition, one should test WithYear() with valid and invalid results.

public abstract partial class IAdjustableDateFacts<TDate, TDataSet> :
    CalendarDataConsumer<TDataSet>
    where TDate : IAdjustableDate<TDate>
    where TDataSet : ICalendarDataSet, ISingleton<TDataSet>
{
    protected IAdjustableDateFacts(Range<int> supportedYears)
    {
        SupportedYearsTester = new SupportedYearsTester(supportedYears);
    }

    protected SupportedYearsTester SupportedYearsTester { get; }

    protected abstract TDate GetDate(int y, int m, int d);
}

public partial class IAdjustableDateFacts<TDate, TDataSet> // Adjust()
{
    [Fact]
    public void Adjust_InvalidAdjuster()
    {
        var date = GetDate(1, 1, 1);
        // Act & Assert
        Assert.ThrowsAnexn("adjuster", () => date.Adjust(null!));
    }

    [Theory, MemberData(nameof(DateInfoData))]
    public void Adjust_InvalidYears(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(y, m, d);
        foreach (var invalidYear in SupportedYearsTester.InvalidYears)
        {
            var adjuster = (DateParts parts) =>
            {
                var (y, m, d) = parts;
                return new DateParts(invalidYear, m, d);
            };
            // Act & Assert
            Assert.ThrowsAoorexn("year", () => date.Adjust(adjuster));
        }
    }

    [Theory, MemberData(nameof(InvalidMonthFieldData))]
    public void Adjust_InvalidMonth(int y, int newMonth)
    {
        var date = GetDate(y, 1, 1);
        var adjuster = (DateParts parts) =>
        {
            var (y, m, d) = parts;
            return new DateParts(y, newMonth, d);
        };
        // Act & Assert
        Assert.ThrowsAoorexn("month", () => date.Adjust(adjuster));
    }

    [Theory, MemberData(nameof(InvalidDayFieldData))]
    public void Adjust_InvalidDay(int y, int m, int newDay)
    {
        var date = GetDate(y, m, 1);
        var adjuster = (DateParts parts) =>
        {
            var (y, m, d) = parts;
            return new DateParts(y, m, newDay);
        };
        // Act & Assert
        Assert.ThrowsAoorexn("day", () => date.Adjust(adjuster));
    }

    [Theory, MemberData(nameof(DateInfoData))]
    public void Adjust_Invariance(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(y, m, d);
        // Act & Assert
        Assert.Equal(date, date.Adjust(x => x));
    }

    [Theory, MemberData(nameof(DateInfoData))]
    public void Adjust(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(1, 1, 1);
        var adjuster = (DateParts _) => new DateParts(y, m, d);
        var exp = GetDate(y, m, d);
        // Act & Assert
        Assert.Equal(exp, date.Adjust(adjuster));
    }
}

public partial class IAdjustableDateFacts<TDate, TDataSet> // WithYear()
{
    [Theory, MemberData(nameof(DateInfoData))]
    public void WithYear_InvalidYears(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(y, m, d);
        // Act & Assert
        SupportedYearsTester.TestInvalidYear(date.WithYear, "newYear");
    }

    [Fact]
    public void WithYear_ValidYears()
    {
        foreach (int y in SupportedYearsTester.ValidYears)
        {
            var date = GetDate(1, 1, 1);
            var exp = GetDate(y, 1, 1);
            // Act & Assert
            Assert.Equal(exp, date.WithYear(y));
        }
    }

    [Theory, MemberData(nameof(DateInfoData))]
    public void WithYear_Invariance(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(y, m, d);
        // Act & Assert
        Assert.Equal(date, date.WithYear(y));
    }

    // NB: disabled because this cannot work in case the matching day in year 1
    // is not valid. Nevertheless I keep it around just to remind me that I
    // should not try to create it again.
    //[Theory, MemberData(nameof(DateInfoData))]
    //public void WithYear(DateInfo info)
    //{
    //    var (y, m, d) = info.Yemoda;
    //    var date = GetDate(1, m, d);
    //    var exp = GetDate(y, m, d);
    //    // Act & Assert
    //    Assert.Equal(exp, date.WithYear(y));
    //}
}

public partial class IAdjustableDateFacts<TDate, TDataSet> // WithMonth()
{
    [Theory, MemberData(nameof(InvalidMonthFieldData))]
    public void WithMonth_InvalidMonth(int y, int newMonth)
    {
        var date = GetDate(y, 1, 1);
        // Act & Assert
        Assert.ThrowsAoorexn("newMonth", () => date.WithMonth(newMonth));
    }

    [Theory, MemberData(nameof(DateInfoData))]
    public void WithMonth_Invariance(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(y, m, d);
        // Act & Assert
        Assert.Equal(date, date.WithMonth(m));
    }

    [Theory, MemberData(nameof(MonthInfoData))]
    public void WithMonth(MonthInfo info)
    {
        var (y, m) = info.Yemo;
        var date = GetDate(y, 1, 1);
        var exp = GetDate(y, m, 1);
        // Act & Assert
        Assert.Equal(exp, date.WithMonth(m));
    }
}

public partial class IAdjustableDateFacts<TDate, TDataSet> // WithDay()
{
    [Theory, MemberData(nameof(InvalidDayFieldData))]
    public void WithDay_InvalidDay(int y, int m, int newDay)
    {
        var date = GetDate(y, m, 1);
        // Act & Assert
        Assert.ThrowsAoorexn("newDay", () => date.WithDay(newDay));
    }

    [Theory, MemberData(nameof(DateInfoData))]
    public void WithDay_Invariance(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(y, m, d);
        // Act & Assert
        Assert.Equal(date, date.WithDay(d));
    }

    [Theory, MemberData(nameof(DateInfoData))]
    public void WithDay(DateInfo info)
    {
        var (y, m, d) = info.Yemoda;
        var date = GetDate(y, m, 1);
        var exp = GetDate(y, m, d);
        // Act & Assert
        Assert.Equal(exp, date.WithDay(d));
    }
}
