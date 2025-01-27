﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Facts.Simple;

using Zorglub.Testing.Data;
using Zorglub.Testing.Facts.Hemerology;
using Zorglub.Time.Simple;

public abstract partial class OrdinalDateAdjustmentFacts<TDataSet> :
    IAdjustableOrdinalFacts<OrdinalDate, TDataSet>
    where TDataSet : ICalendarDataSet, ISingleton<TDataSet>
{
    protected OrdinalDateAdjustmentFacts(Calendar calendar)
        : base(calendar?.SupportedYears ?? throw new ArgumentNullException(nameof(calendar)))
    {
        Debug.Assert(calendar != null);

        CalendarUT = calendar;
    }

    protected Calendar CalendarUT { get; }

    protected override OrdinalDate GetDate(int y, int doy) => CalendarUT.GetOrdinalDate(y, doy);
}
