﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Facts;

using Zorglub.Bulgroz.Formulae;
using Zorglub.Testing.Data;

/// <summary>
/// Provides facts about <see cref="IInterconversionFormulae"/>.
/// </summary>
public abstract class IInterconversionFormulaeFacts<TFormulae, TDataSet> :
    CalendricalDataConsumer<TDataSet>
    where TFormulae : IInterconversionFormulae
    where TDataSet : ICalendricalDataSet, ISingleton<TDataSet>
{
    protected IInterconversionFormulaeFacts(TFormulae formulae)
    {
        FormulaeUT = formulae ?? throw new ArgumentNullException(nameof(formulae));
    }

    /// <summary>
    /// Gets the schema under test.
    /// </summary>
    protected TFormulae FormulaeUT { get; }

    [Theory, MemberData(nameof(DaysSinceEpochInfoData))]
    public void CountDaysSinceEpoch(DaysSinceEpochInfo info)
    {
        var (daysSinceEpoch, y, m, d) = info;

        // Act
        int actual = FormulaeUT.CountDaysSinceEpoch(y, m, d);
        // Assert
        Assert.Equal(daysSinceEpoch, actual);
    }

    [Theory, MemberData(nameof(DaysSinceEpochInfoData))]
    public void GetDateParts(DaysSinceEpochInfo info)
    {
        var (daysSinceEpoch, y, m, d) = info;

        // Act
        FormulaeUT.GetDateParts(daysSinceEpoch, out int year, out int month, out int day);
        // Assert
        Assert.Equal(y, year);
        Assert.Equal(m, month);
        Assert.Equal(d, day);
    }
}
