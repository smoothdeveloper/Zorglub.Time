﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

module Zorglub.Tests.Core.CalendricalPreValidatorTestSuite

open System

open Zorglub.Testing
open Zorglub.Testing.Data.Schemas
open Zorglub.Testing.Facts

open Zorglub.Time.Core
open Zorglub.Time.Core.Schemas

// We use the Copic13 schema because it may overflow when calling
// CountDaysInYear() or CountDaysInMonth(). Both rely on IsLeapYear() which
// overflows at Int32.MaxYear.
let private sch = schemaOf<Coptic13Schema>()
let private supportedYears = sch.SupportedYears

[<Sealed>]
type Copic13Tests() =
    inherit ICalendricalPreValidatorFacts<Coptic13DataSet>(
        new CalendricalPreValidator(sch),
        supportedYears.Min,
        supportedYears.Max)

    override x.ValidateMonthDay_AtAbsoluteMaxYear() =
        let validator = x.ValidatorUT
        (fun () -> validator.ValidateMonthDay(Int32.MaxValue, 1, 1)) |> overflows

    override x.ValidateDayOfYear_AtAbsoluteMaxYear() =
        let validator = x.ValidatorUT
        (fun () -> validator.ValidateDayOfYear(Int32.MaxValue, 1)) |> overflows
