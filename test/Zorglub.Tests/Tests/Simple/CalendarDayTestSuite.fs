﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

module Zorglub.Tests.Simple.CalendarDayTestSuite

open Zorglub.Testing
open Zorglub.Testing.Data.Bounded
open Zorglub.Testing.Facts.Simple

open Zorglub.Time.Simple

[<Sealed>]
type ArmenianTests() =
    inherit CalendarDayFacts<StandardArmenian12DataSet>(ArmenianCalendar.Instance, GregorianCalendar.Instance)

[<Sealed>]
[<RedundantTestGroup>]
type CopticTests() =
    inherit CalendarDayFacts<StandardCoptic12DataSet>(CopticCalendar.Instance, GregorianCalendar.Instance)

[<Sealed>]
[<RedundantTestGroup>]
type EthiopicTests() =
    inherit CalendarDayFacts<StandardEthiopic12DataSet>(EthiopicCalendar.Instance, GregorianCalendar.Instance)

[<Sealed>]
[<RedundantTestGroup>]
type GregorianTests() =
    inherit CalendarDayFacts<ProlepticGregorianDataSet>(GregorianCalendar.Instance, JulianCalendar.Instance)

[<Sealed>]
[<RedundantTestGroup>]
type JulianTests() =
    inherit CalendarDayFacts<ProlepticJulianDataSet>(JulianCalendar.Instance, GregorianCalendar.Instance)

[<Sealed>]
[<RedundantTestGroup>]
type TabularIslamicTests() =
    inherit CalendarDayFacts<StandardTabularIslamicDataSet>(TabularIslamicCalendar.Instance, GregorianCalendar.Instance)

[<Sealed>]
[<RedundantTestGroup>]
type ZoroastrianTests() =
    inherit CalendarDayFacts<StandardZoroastrian12DataSet>(ZoroastrianCalendar.Instance, GregorianCalendar.Instance)
