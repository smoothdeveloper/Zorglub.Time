﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data.Unbounded;

using Zorglub.Testing.Data;
using Zorglub.Testing.Data.Schemas;
using Zorglub.Time.Hemerology;

/// <summary>
/// Provides test data for the (unbounded) Pax calendar.
/// </summary>
public sealed class UnboundedPaxDataSet : UnboundedCalendarDataSet<PaxDataSet>, ISingleton<UnboundedPaxDataSet>
{
    private static readonly DayNumber s_Epoch = CalendarEpoch.SundayBeforeGregorian;

    private UnboundedPaxDataSet() : base(PaxDataSet.Instance, s_Epoch) { }

    public static UnboundedPaxDataSet Instance => Singleton.Instance;

    private static class Singleton
    {
        internal static readonly UnboundedPaxDataSet Instance = new();
        static Singleton() { }
    }

    public override DataGroup<DayNumberInfo> DayNumberInfoData { get; } =
        DataGroup.CreateDayNumberInfoData(PaxDataSet.DaysSinceEpochInfos, s_Epoch);

    public static DataGroup<DayNumberYewedaInfo> DayNumberYewedaInfoData { get; } =
        DataGroup.CreateDayNumberYewedaInfo(PaxDataSet.DaysSinceEpochYewedaInfos, s_Epoch);
}
