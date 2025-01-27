﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

module Zorglub.Tests.Simple.Specialized.RomanKalendarTests

open System

open Zorglub.Testing
open Zorglub.Testing.Data

open Zorglub.Time.Simple
open Zorglub.Time.Simple.Specialized

open Xunit

let epiphanyData = RomanKalendarDataSet.EpiphanyData
let easterData = RomanKalendarDataSet.EasterData
let paschalMoonData = RomanKalendarDataSet.PaschalMoonData

[<Theory; MemberData(nameof(epiphanyData))>]
let ``Property EpiphanySunday`` y d =
    let epiphanySunday = new CalendarDate(y, 1, d)
    let kalendar = new RomanKalendar(y)

    epiphanySunday.DayOfWeek === DayOfWeek.Sunday
    kalendar.EpiphanySunday  === epiphanySunday

[<Theory; MemberData(nameof(easterData))>]
let ``Property Easter`` y m d =
    let easter = new CalendarDate(y, m, d)
    let kalendar = new RomanKalendar(y)

    easter.DayOfWeek === DayOfWeek.Sunday
    kalendar.Easter  === easter

[<Theory(Skip = "D&R data does not match our definition of the Paschal Moon?"); MemberData(nameof(paschalMoonData))>]
let ``Property PaschalMoon`` y m d =
    let moon = new CalendarDate(y, m, d)
    let kalendar = new RomanKalendar(y)

    kalendar.PaschalMoon === moon
