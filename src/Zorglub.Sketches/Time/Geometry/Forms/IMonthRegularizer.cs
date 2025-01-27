﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Geometry.Forms
{
    public interface IMonthRegularizer
    {
        void Regularize(ref int y, ref int m);

        void Deregularize(ref int y0, ref int m0);
    }
}
