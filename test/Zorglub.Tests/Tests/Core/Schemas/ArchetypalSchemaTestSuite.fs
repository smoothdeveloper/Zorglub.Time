﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

module Zorglub.Tests.Core.Schemas.ArchetypalSchemaTestSuite

open Zorglub.Testing
open Zorglub.Testing.Data.Schemas
open Zorglub.Testing.Facts.Core

open Zorglub.Time.Core
open Zorglub.Time.Core.Schemas

// TODO(code): Hebrew (unfinished, no data) and lunisolar (fake) schema.

/// Creates a new instance of the schema archetype of type 'a.
let private archetypeOf<'a when 'a : not struct and 'a :> ICalendricalSchema and 'a :> IBoxable<'a>> () =
    let sch = SchemaActivator.CreateInstance<'a>()
    new SchemaArchetype(sch)

[<Sealed>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type Coptic12Tests() =
    inherit ArchetypalSchemaFacts<Coptic12DataSet>(archetypeOf<Coptic12Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type Coptic13Tests() =
    inherit ArchetypalSchemaFacts<Coptic13DataSet>(archetypeOf<Coptic13Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type Egyptian12Tests() =
    inherit ArchetypalSchemaFacts<Egyptian12DataSet>(archetypeOf<Egyptian12Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type Egyptian13Tests() =
    inherit ArchetypalSchemaFacts<Egyptian13DataSet>(archetypeOf<Egyptian13Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type FrenchRepublican12Tests() =
    inherit ArchetypalSchemaFacts<FrenchRepublican12DataSet>(archetypeOf<FrenchRepublican12Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type FrenchRepublican13Tests() =
    inherit ArchetypalSchemaFacts<FrenchRepublican13DataSet>(archetypeOf<FrenchRepublican13Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type GregorianTests() =
    inherit ArchetypalSchemaFacts<GregorianDataSet>(archetypeOf<GregorianSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type InternationalFixedTests() =
    inherit ArchetypalSchemaFacts<InternationalFixedDataSet>(archetypeOf<InternationalFixedSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type JulianTests() =
    inherit ArchetypalSchemaFacts<JulianDataSet>(archetypeOf<JulianSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type LunisolarTests() =
    inherit ArchetypalSchemaFacts<LunisolarDataSet>(archetypeOf<LunisolarSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type PaxTests() =
    inherit ArchetypalSchemaFacts<PaxDataSet>(archetypeOf<PaxSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type Persian2820Tests() =
    inherit ArchetypalSchemaFacts<Persian2820DataSet>(archetypeOf<Persian2820Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type PositivistTests() =
    inherit ArchetypalSchemaFacts<PositivistDataSet>(archetypeOf<PositivistSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type TabularIslamicTests() =
    inherit ArchetypalSchemaFacts<TabularIslamicDataSet>(archetypeOf<TabularIslamicSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type TropicaliaTests() =
    inherit ArchetypalSchemaFacts<TropicaliaDataSet>(archetypeOf<TropicaliaSchema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type Tropicalia3031Tests() =
    inherit ArchetypalSchemaFacts<Tropicalia3031DataSet>(archetypeOf<Tropicalia3031Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type Tropicalia3130Tests() =
    inherit ArchetypalSchemaFacts<Tropicalia3130DataSet>(archetypeOf<Tropicalia3130Schema>())

[<Sealed>]
[<RedundantTestGroup>]
[<TestPerformance(TestPerformance.SlowGroup)>]
type WorldTests() =
    inherit ArchetypalSchemaFacts<WorldDataSet>(archetypeOf<WorldSchema>())
