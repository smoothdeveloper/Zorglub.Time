﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

module Zorglub.Tests.Testing.EnumDataSetTests

open System
open System.Linq

open Zorglub.Testing
open Zorglub.Testing.Data

open Zorglub.Time

open Xunit

[<Fact>]
let ``AddAdjustmentData is exhaustive`` () =
    let count = Enum.GetValues(typeof<AddAdjustment>).Length

    EnumDataSet.AddAdjustmentData.Count() === count

[<Fact>]
let ``CalendricalAlgorithmData is exhaustive`` () =
    let count = Enum.GetValues(typeof<CalendricalAlgorithm>).Length

    EnumDataSet.CalendricalAlgorithmData.Count() === count

[<Fact>]
let ``CalendricalFamilyData is exhaustive`` () =
    let count = Enum.GetValues(typeof<CalendricalFamily>).Length

    EnumDataSet.CalendricalFamilyData.Count() === count

[<Fact>]
let ``CalendarIdData is exhaustive`` () =
    let count = Enum.GetValues(typeof<CalendarId>).Length

    EnumDataSet.CalendarIdData.Count() === count
