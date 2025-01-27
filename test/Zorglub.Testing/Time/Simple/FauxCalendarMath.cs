﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Simple;

using Zorglub.Time.Core.Intervals;

public sealed partial class FauxCalendarMath : CalendarMath
{
    public FauxCalendarMath() : this(new FauxUserCalendar(), default) { }

    // Base constructors.
    public FauxCalendarMath(Calendar calendar, AddAdjustment adjustment) : base(calendar, adjustment) { }

    // Constructors in order to test the base constructors.
    public FauxCalendarMath(Calendar calendar) : this(calendar, default) { }
    public FauxCalendarMath(AddAdjustment adjustment) : this(new FauxUserCalendar(), adjustment) { }

    public Range<int> SupportedYearsDisclosed => SupportedYears;

    public bool AddYearsCoreDateWasCalled { get; private set; }
    public bool AddMonthsCoreDateWasCalled { get; private set; }
    public bool CountYearsBetweenCoreDateWasCalled { get; private set; }
    public bool CountMonthsBetweenCoreDateWasCalled { get; private set; }

    public bool AddYearsCoreOrdinalDateWasCalled { get; private set; }
    public bool CountYearsBetweenCoreOrdinalDateWasCalled { get; private set; }

    public bool AddYearsCoreMonthWasCalled { get; private set; }
    public bool AddMonthsCoreMonthWasCalled { get; private set; }
    public bool CountYearsBetweenCoreMonthWasCalled { get; private set; }
    public bool CountMonthsBetweenCoreMonthWasCalled { get; private set; }
}

public partial class FauxCalendarMath // CalendarDate
{
    [Pure]
    protected internal override CalendarDate AddYearsCore(CalendarDate date, int years)
    { AddYearsCoreDateWasCalled = true; return date; }

    [Pure]
    protected internal override CalendarDate AddMonthsCore(CalendarDate date, int months)
    { AddMonthsCoreDateWasCalled = true; return date; }

    [Pure]
    protected internal override int CountYearsBetweenCore(CalendarDate start, CalendarDate end)
    { CountYearsBetweenCoreDateWasCalled = true; return 0; }

    [Pure]
    protected internal override int CountMonthsBetweenCore(CalendarDate start, CalendarDate end)
    { CountMonthsBetweenCoreDateWasCalled = true; return 0; }
}

public partial class FauxCalendarMath // OrdinalDate
{
    [Pure]
    protected internal override OrdinalDate AddYearsCore(OrdinalDate date, int years)
    { AddYearsCoreOrdinalDateWasCalled = true; return date; }

    [Pure]
    protected internal override int CountYearsBetweenCore(OrdinalDate start, OrdinalDate end)
    { CountYearsBetweenCoreOrdinalDateWasCalled = true; return 0; }
}

public partial class FauxCalendarMath // CalendarMonth
{
    [Pure]
    protected internal override CalendarMonth AddYearsCore(CalendarMonth month, int years)
    { AddYearsCoreMonthWasCalled = true; return month; }

    [Pure]
    protected internal override CalendarMonth AddMonthsCore(CalendarMonth month, int months)
    { AddMonthsCoreMonthWasCalled = true; return month; }

    [Pure]
    protected internal override int CountYearsBetweenCore(CalendarMonth start, CalendarMonth end)
    { CountYearsBetweenCoreMonthWasCalled = true; return 0; }

    [Pure]
    protected internal override int CountMonthsBetweenCore(CalendarMonth start, CalendarMonth end)
    { CountMonthsBetweenCoreMonthWasCalled = true; return 0; }
}
