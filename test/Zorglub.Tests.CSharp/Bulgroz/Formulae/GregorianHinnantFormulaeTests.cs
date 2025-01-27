﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Bulgroz.Formulae;

public sealed class GregorianHinnantFormulaeTests
    : IInterconversionFormulaeFacts<GregorianHinnantFormulae, GregorianDataSet>
{
    public GregorianHinnantFormulaeTests() : base(new GregorianHinnantFormulae()) { }

    [Theory, MemberData(nameof(MonthInfoData))]
    public void CountDaysInYearBeforeMonth(MonthInfo info)
    {
        var (y, m) = info.Yemo;
        // Act
        int actual = FormulaeUT.CountDaysInYearBeforeMonth(y, m);
        // Assert
        Assert.Equal(info.DaysInYearBeforeMonth, actual);
    }
}
