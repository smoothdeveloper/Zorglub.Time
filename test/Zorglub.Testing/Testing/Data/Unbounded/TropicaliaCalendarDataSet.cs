﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data.Unbounded;

using Zorglub.Testing.Data.Schemas;

/// <summary>
/// Provides test data for the (unbounded) "Tropicália" calendar and related date types.
/// </summary>
public sealed class TropicaliaCalendarDataSet :
    CalendarDataSet<TropicalistaDataSet>, ISingleton<TropicaliaCalendarDataSet>
{
    private TropicaliaCalendarDataSet() : base(TropicaliaDataSet.Instance, DayZero.NewStyle) { }

    public static TropicaliaCalendarDataSet Instance { get; } = new();

    private TheoryData<DayNumberInfo>? _dayNumberInfoData;
    public override TheoryData<DayNumberInfo> DayNumberInfoData =>
        _dayNumberInfoData ??= DataGroup.OfDayNumberInfo.Create(TropicaliaDataSet.DaysSinceRataDieInfos);
}

/// <summary>
/// Provides test data for the (unbounded) "Tropicália" calendar (30-31) and related date types.
/// </summary>
public sealed class Tropicalia3031CalendarDataSet :
    CalendarDataSet<Tropicalia3031DataSet>, ISingleton<Tropicalia3031CalendarDataSet>
{
    private Tropicalia3031CalendarDataSet() : base(Tropicalia3031DataSet.Instance, DayZero.NewStyle) { }

    public static Tropicalia3031CalendarDataSet Instance { get; } = new();

    private TheoryData<DayNumberInfo>? _dayNumberInfoData;
    public override TheoryData<DayNumberInfo> DayNumberInfoData =>
        _dayNumberInfoData ??= DataGroup.OfDayNumberInfo.Create(Tropicalia3031DataSet.DaysSinceEpochInfos, Epoch);
}

/// <summary>
/// Provides test data for the (unbounded) "Tropicália" calendar (31-30) and related date types.
/// </summary>
public sealed class Tropicalia3130CalendarDataSet :
    CalendarDataSet<Tropicalia3130DataSet>, ISingleton<Tropicalia3130CalendarDataSet>
{
    private Tropicalia3130CalendarDataSet() : base(Tropicalia3130DataSet.Instance, DayZero.NewStyle) { }

    public static Tropicalia3130CalendarDataSet Instance { get; } = new();

    private TheoryData<DayNumberInfo>? _dayNumberInfoData;
    public override TheoryData<DayNumberInfo> DayNumberInfoData =>
        _dayNumberInfoData ??= DataGroup.OfDayNumberInfo.Create(Tropicalia3130DataSet.DaysSinceEpochInfos, Epoch);
}
