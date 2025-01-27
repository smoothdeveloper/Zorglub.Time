﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Bulgroz.Formulae;

using Zorglub.Time.Core.Schemas;

/// <summary>
/// Alternative formulae for the "Tropicália" (31-30) calendar.
/// <para>This class cannot be inherited.</para>
/// </summary>
public static class Tropicalia3130Formulae
{
    [Pure]
    // We remove the unused param "y".
    public static int CountDaysInYearBeforeMonth(int m) => 30 * (m - 1) + (m >> 1);

    [Pure]
    public static int CountDaysInMonth(int y, int m) =>
        m != 12 ? 30 + (m & 1)
        : TropicalistaSchema.IsLeapYearImpl(y) ? 30
        : 29;

    // We remove the unused param "y".
    [Pure]
    public static int GetMonth(int doy, out int d)
    {
        int d0y = doy - 1;

        int b = d0y / 61;
        int j = d0y % 61;
        int n = j / 31;
        int m = 1 + 2 * b + n;
        d = 1 + j - 31 * n;
        return m;
    }
}
