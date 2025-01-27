﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Bulgroz.Formulae;

public sealed class JulianTroeschFormulaeTests
    : IInterconversionFormulaeFacts<JulianTroeschFormulae, JulianDataSet>
{
    public JulianTroeschFormulaeTests() : base(new JulianTroeschFormulae()) { }

    [Theory, MemberData(nameof(StartOfYearDaysSinceEpochData))]
    public void GetStartOfYear(YearDaysSinceEpoch info)
    {
        // Act
        var actual = JulianTroeschFormulae.GetStartOfYear(info.Year);
        // Assert
        Assert.Equal(info.DaysSinceEpoch, actual);
    }
}
