﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data.Unbounded;

using Zorglub.Testing.Data.Schemas;
using Zorglub.Time.Hemerology;

/// <summary>
/// Provides test data for the (unbounded) Zoroastrian calendar (alternative form) and related date types.
/// </summary>
public sealed class Zoroastrian13CalendarDataSet :
    CalendarDataSet<Egyptian13DataSet>, IEpagomenalDataSet, ISingleton<Zoroastrian13CalendarDataSet>
{
    private static readonly DayNumber s_Epoch = CalendarEpoch.Zoroastrian;

    private Zoroastrian13CalendarDataSet() : base(Egyptian13DataSet.Instance, s_Epoch) { }

    public static Zoroastrian13CalendarDataSet Instance { get; } = new();

    public sealed override DataGroup<DayNumberInfo> DayNumberInfoData =>
        DataGroup.CreateDayNumberInfoData(Egyptian13DataSet.DaysSinceRataDieInfos, CalendarEpoch.Egyptian, s_Epoch);

    public DataGroup<YemodaAnd<int>> EpagomenalDayInfoData => DataSet.EpagomenalDayInfoData;
}

/// <summary>
/// Provides test data for the (unbounded) Zoroastrian calendar and related date types.
/// </summary>
public sealed class Zoroastrian12CalendarDataSet :
    CalendarDataSet<Egyptian12DataSet>, IEpagomenalDataSet, ISingleton<Zoroastrian12CalendarDataSet>
{
    private static readonly DayNumber s_Epoch = CalendarEpoch.Zoroastrian;

    private Zoroastrian12CalendarDataSet() : base(Egyptian12DataSet.Instance, s_Epoch) { }

    public static Zoroastrian12CalendarDataSet Instance { get; } = new();

    public sealed override DataGroup<DayNumberInfo> DayNumberInfoData =>
        DataGroup.CreateDayNumberInfoData(Egyptian12DataSet.DaysSinceRataDieInfos, CalendarEpoch.Egyptian, s_Epoch);

    public DataGroup<YemodaAnd<int>> EpagomenalDayInfoData => DataSet.EpagomenalDayInfoData;
}
