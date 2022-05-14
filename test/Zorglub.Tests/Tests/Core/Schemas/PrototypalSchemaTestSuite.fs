﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

module Zorglub.Tests.Core.Schemas.PrototypalSchemaTestSuite

open Zorglub.Testing
open Zorglub.Testing.Data.Schemas
open Zorglub.Testing.Facts.Core

open Zorglub.Time.Core
open Zorglub.Time.Core.Schemas

// TODO(code): Hebrew (unfinished, no data) and lunisolar (fake) schema.

/// Creates a new instance of the schema prototype of type 'a.
let private prototypeOf<'a when 'a : not struct and 'a :> ICalendricalSchema and 'a :> IBoxable<'a>> () =
    let sch = SchemaActivator.CreateInstance<'a>()
    new PrototypalSchema(sch, sch.MinDaysInYear, sch.MinDaysInMonth)

// =====================================================================================================================

//[<Sealed>]
//type NoCachePrototypalSchema(kernel: ICalendricalKernel, minDaysInYear, minDaysInMonth) as self =
//    inherit PrototypalSchema(kernel, minDaysInYear, minDaysInMonth)
//        do self.DisableStartOfYearCache <- true

//let private noCachePrototypeOf<'a when 'a : not struct and 'a :> ICalendricalSchema and 'a :> IBoxable<'a>> () =
//    let sch = SchemaActivator.CreateInstance<'a>()
//    new NoCachePrototypalSchema(sch, sch.MinDaysInYear, sch.MinDaysInMonth)

let private prototypeOf2<'a when 'a : not struct and 'a :> ICalendricalSchema and 'a :> IBoxable<'a>> () =
    let sch = SchemaActivator.CreateInstance<'a>()
    new PrototypalSchema2(sch, sch.MinDaysInYear, sch.MinDaysInMonth)

[<Sealed>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.CodeCoverage)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type Coptic12BasicTests() =
    inherit ArchetypalSchemaBasicFacts<Coptic12DataSet>(prototypeOf2<Coptic12Schema>())

// =====================================================================================================================

[<Sealed>]
[<TestPerformance(TestPerformance.SlowGroup)>]
// TODO(code): temporary trait.
[<TestExcludeFrom(TestExcludeFrom.CodeCoverage)>]
type Coptic12Tests() =
    inherit ArchetypalSchemaFacts<Coptic12DataSet>(prototypeOf<Coptic12Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type Coptic13Tests() =
    inherit ArchetypalSchemaFacts<Coptic13DataSet>(prototypeOf<Coptic13Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type Egyptian12Tests() =
    inherit ArchetypalSchemaFacts<Egyptian12DataSet>(prototypeOf<Egyptian12Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type Egyptian13Tests() =
    inherit ArchetypalSchemaFacts<Egyptian13DataSet>(prototypeOf<Egyptian13Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type FrenchRepublican12Tests() =
    inherit ArchetypalSchemaFacts<FrenchRepublican12DataSet>(prototypeOf<FrenchRepublican12Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type FrenchRepublican13Tests() =
    inherit ArchetypalSchemaFacts<FrenchRepublican13DataSet>(prototypeOf<FrenchRepublican13Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type GregorianTests() =
    inherit ArchetypalSchemaFacts<GregorianDataSet>(prototypeOf<GregorianSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type InternationalFixedTests() =
    inherit ArchetypalSchemaFacts<InternationalFixedDataSet>(prototypeOf<InternationalFixedSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type JulianTests() =
    inherit ArchetypalSchemaFacts<JulianDataSet>(prototypeOf<JulianSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type LunisolarTests() =
    inherit ArchetypalSchemaFacts<LunisolarDataSet>(prototypeOf<LunisolarSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type PaxTests() =
    inherit ArchetypalSchemaFacts<PaxDataSet>(prototypeOf<PaxSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type Persian2820Tests() =
    inherit ArchetypalSchemaFacts<Persian2820DataSet>(prototypeOf<Persian2820Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type PositivistTests() =
    inherit ArchetypalSchemaFacts<PositivistDataSet>(prototypeOf<PositivistSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type TabularIslamicTests() =
    inherit ArchetypalSchemaFacts<TabularIslamicDataSet>(prototypeOf<TabularIslamicSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type TropicaliaTests() =
    inherit ArchetypalSchemaFacts<TropicaliaDataSet>(prototypeOf<TropicaliaSchema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type Tropicalia3031Tests() =
    inherit ArchetypalSchemaFacts<Tropicalia3031DataSet>(prototypeOf<Tropicalia3031Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type Tropicalia3130Tests() =
    inherit ArchetypalSchemaFacts<Tropicalia3130DataSet>(prototypeOf<Tropicalia3130Schema>())

[<Sealed>]
[<RedundantTesting>]
[<TestPerformance(TestPerformance.SlowGroup)>]
[<TestExcludeFrom(TestExcludeFrom.Smoke)>]
type WorldTests() =
    inherit ArchetypalSchemaFacts<WorldDataSet>(prototypeOf<WorldSchema>())
