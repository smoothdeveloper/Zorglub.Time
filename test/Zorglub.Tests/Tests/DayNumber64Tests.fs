﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

module Zorglub.Tests.DayNumber64Tests

open System
open System.Runtime.CompilerServices

open Zorglub.Testing
open Zorglub.Testing.Data

open Zorglub.Time
open Zorglub.Time.Hemerology

open Xunit
open FsCheck
open FsCheck.Xunit

// SYNC WITH DayNumberTests; see there for explanations.
// We use the exact same code but adapted to DayNumber64.
// When a test is too different from the one in DayNumberTests, we push it to Postlude.

module TestCommon =
    let xynArbitrary =
        Arb.fromGen <| gen {
            let! i, j, n = IntGenerators.orderedPair
            let v = DayNumber64.Zero + i
            let w = DayNumber64.Zero + j
            return v, w, int64(n)
        }

    [<Struct; IsReadOnly>]
    type DaysSinceZero64 = { Value: int64 } with
        override x.ToString() = x.Value.ToString()
        static member op_Explicit (x: DaysSinceZero64) = x.Value

    [<Sealed>]
    type Arbitraries =
        static member GetDaysSinceZero64Arbitrary() =
            DomainArbitraries.daysSinceZero64
            |> Arb.convert (fun i -> { DaysSinceZero64.Value = i }) int64

[<Properties(Arbitrary = [| typeof<TestCommon.Arbitraries> |] )>]
module Prelude =
    open TestCommon

    let dayNumber64ToDayOfWeekData = CalCalDataSet.DayNumber64ToDayOfWeekData

    [<Fact>]
    let ``Default value`` () =
        Unchecked.defaultof<DayNumber64> === DayNumber64.Zero

    [<Property>]
    let ``ToString() returns the string representation of DaysSinceZero using the current culture`` (i: DaysSinceZero64) =
        let dayNumber = DayNumber64.Zero + i.Value

        dayNumber.ToString() = i.Value.ToString(System.Globalization.CultureInfo.CurrentCulture)

    //
    // Properties
    //

    [<Fact>]
    let ``Static property Zero`` () =
        DayNumber64.Zero.DaysSinceZero === 0
        DayNumber64.Zero.Ordinal === Ord64.First

    [<Fact>]
    let ``Static property MinValue`` () =
        DayNumber64.MinValue.DaysSinceZero === DayNumber64.MinDaysSinceZero
        DayNumber64.MinValue.Ordinal === Ord64.MinValue
        DayNumber64.MinValue.DayOfWeek === DayOfWeek.Monday

    [<Fact>]
    let ``Static property MaxValue`` () =
        DayNumber64.MaxValue.DaysSinceZero === DayNumber64.MaxDaysSinceZero
        DayNumber64.MaxValue.Ordinal === Ord64.MaxValue
        DayNumber64.MaxValue.DayOfWeek === DayOfWeek.Sunday

    [<Property>]
    let ``Property DaysSinceZero`` (i: DaysSinceZero64) =
        let dayNumber = DayNumber64.Zero + i.Value

        dayNumber.DaysSinceZero = i.Value

    [<Property>]
    let ``Property Ordinal`` (i: DaysSinceZero64) =
        let dayNumber = DayNumber64.Zero + i.Value

        dayNumber.Ordinal = Ord64.First + i.Value

    [<Theory; MemberData(nameof(dayNumber64ToDayOfWeekData))>]
    let ``Property DayOfWeek`` (dayNumber: DayNumber64) dayOfWeek =
        dayNumber.DayOfWeek === dayOfWeek

    //
    // Properties of DayZero64
    //
    // See Postlude.

// TODO(code): tests from DayNumber.
// module Factories
// module Conversions
// module GregorianConversion
// module JulianConversion
// module DayOfWeekAdjustment
// module DayOfWeekAdjustment2

[<Properties(Arbitrary = [| typeof<TestCommon.Arbitraries> |] )>]
module Equality =
    open NonStructuralComparison
    open TestCommon

    // fsharplint:disable Hints
    [<Property>]
    let ``Equality when both operands are identical`` (x: DayNumber64) =
        x = x
        .&. not (x <> x)
        .&. x.Equals(x)
        .&. x.Equals(x :> obj)

    [<Property>]
    let ``Equality when both operands are distinct`` () = xynArbitrary @@@@ fun (x, y, _) ->
        not (x = y)
        .&. (x <> y)
        .&. not (x.Equals(y))
        .&. not (x.Equals(y :> obj))
        // Flipped
        .&. not (y = x)
        .&. (y <> x)
        .&. not (y.Equals(x))
        .&. not (y.Equals(x :> obj))
    // fsharplint:enable

    [<Property>]
    let ``Equals(obj) returns false when "obj" is null or is a plain object`` (x: DayNumber64) =
        not (x.Equals(null))
        .&. not (x.Equals(new obj()))

    [<Property>]
    let ``Equals(obj) returns false when "obj" is an integer whose value is equal to DaysSinceZero`` (i: DaysSinceZero64) =
            let dayNumber = DayNumber64.Zero + i.Value
            not (dayNumber.Equals(i.Value))

    [<Property>]
    let ``GetHashCode() is invariant`` (x: DayNumber64) =
        x.GetHashCode() = x.GetHashCode()

    [<Property>]
    let ``GetHashCode() returns the hashcode of DaysSinceZero`` (x: DayNumber64) =
        let hash = x.DaysSinceZero.GetHashCode()

        x.GetHashCode() = hash

module Comparison =
    open NonStructuralComparison
    open TestCommon

    // fsharplint:disable Hints
    [<Property>]
    let ``Comparisons when both operands are identical`` (x: DayNumber64) =
        not (x > x)
        .&. not (x < x)
        .&. (x >= x)
        .&. (x <= x)

    [<Property>]
    let ``Comparisons when both operands are distinct`` () = xynArbitrary @@@@ fun (x, y, _) ->
        not (x > y)
        .&. not (x >= y)
        .&. (x < y)
        .&. (x <= y)
        // Flipped
        .&. (y > x)
        .&. (y >= x)
        .&. not (y < x)
        .&. not (y <= x)
    // fsharplint:enable

    //
    // CompareTo()
    //

    [<Property>]
    let ``CompareTo() returns 0 when both objects are identical`` (x: DayNumber64) =
        (x.CompareTo(x) = 0)
        .&. (x.CompareTo(x :> obj) = 0)

    [<Property>]
    let ``CompareTo() when both objects are distinct`` () = xynArbitrary @@@@ fun (x, y, _) ->
        (x.CompareTo(y) <= 0)
        .&. (x.CompareTo(y :> obj) <= 0)
        // Flipped
        .&. (y.CompareTo(x) >= 0)
        .&. (y.CompareTo(x :> obj) >= 0)

    [<Property>]
    let ``CompareTo(obj) returns 1 when "obj" is null`` (x: DayNumber64) =
         x.CompareTo(null) = 1

    [<Property>]
    let ``CompareTo(obj) throws when "obj" is a plain object`` (x: DayNumber64) =
        argExn "obj" (fun () -> x.CompareTo(new obj()))

    //
    // Min() and Max()
    //

    [<Property>]
    let ``Min() when both values are identical`` (x: DayNumber64) =
        DayNumber64.Min(x, x) = x

    [<Property>]
    let ``Max() when both values are identical`` (x: DayNumber64) =
        DayNumber64.Max(x, x) = x

    [<Property>]
    let ``Min() when both values are distinct`` () = xynArbitrary @@@@ fun (x, y, _) ->
        DayNumber64.Min(x, y) === x
        DayNumber64.Min(y, x) === x

    [<Property>]
    let ``Max() when both values are distinct`` () = xynArbitrary @@@@ fun (x, y, _) ->
        DayNumber64.Max(x, y) === y
        DayNumber64.Max(y, x) === y

module Math =
    open TestCommon

    //
    // DayNumber64.Zero
    //

    [<Fact>]
    let ``DayNumber64.Zero + Int64.MaxValue overflows`` () =
        (fun () -> DayNumber64.Zero + Int64.MaxValue)         |> overflows
        (fun () -> DayNumber64.Zero.PlusDays(Int64.MaxValue)) |> overflows

    [<Fact>]
    let ``DayNumber64.Zero + (Int64.MaxValue - 1) = DayNumber64.MaxValue`` () =
        DayNumber64.Zero + (Int64.MaxValue - 1L)       === DayNumber64.MaxValue
        DayNumber64.Zero.PlusDays(Int64.MaxValue - 1L) === DayNumber64.MaxValue

    [<Fact>]
    let ``DayNumber64.MaxValue - (Int64.MaxValue - 1) = DayNumber64.Zero`` () =
        DayNumber64.MaxValue - (Int64.MaxValue - 1L)          === DayNumber64.Zero
        DayNumber64.MaxValue.PlusDays(-(Int64.MaxValue - 1L)) === DayNumber64.Zero

    [<Fact>]
    let ``DayNumber64.Zero + Int64.MinValue overflows`` () =
        (fun () -> DayNumber64.Zero + Int64.MinValue)         |> overflows
        (fun () -> DayNumber64.Zero.PlusDays(Int64.MinValue)) |> overflows

    [<Fact>]
    let ``DayNumber64.Zero + (Int64.MinValue + 1) = DayNumber64.MinValue`` () =
        DayNumber64.Zero + (Int64.MinValue + 1L)       === DayNumber64.MinValue
        DayNumber64.Zero.PlusDays(Int64.MinValue + 1L) === DayNumber64.MinValue

    [<Fact>]
    let ``DayNumber64.MinValue - (Int64.MinValue + 1) = DayNumber64.MinValue`` () =
        DayNumber64.MinValue - (Int64.MinValue + 1L)          === DayNumber64.Zero
        DayNumber64.MinValue.PlusDays(-(Int64.MinValue + 1L)) === DayNumber64.Zero

    //
    // DayNumber64.MinValue
    //

    [<Fact>]
    let ``DayNumber64.MinValue - 1 overflows`` () =
        (fun () -> DayNumber64.MinValue - 1L)          |> overflows
        (fun () -> DayNumber64.MinValue + (-1))        |> overflows
        (fun () -> DayNumber64.MinValue.PlusDays(-1))  |> overflows
        (fun () -> DayNumber64.MinValue.PreviousDay()) |> overflows

    [<Fact>]
    let ``DayNumber64.MinValue + Int64.MaxValue does not overflow`` () =
        DayNumber64.MinValue + Int64.MaxValue         === DayNumber64.Zero
        DayNumber64.MinValue.PlusDays(Int64.MaxValue) === DayNumber64.Zero

    //
    // DayNumber64.MaxValue
    //

    [<Fact>]
    let ``DayNumber64.MaxValue + 1 overflows`` () =
        (fun () -> DayNumber64.MaxValue + 1)         |> overflows
        (fun () -> DayNumber64.MaxValue - (-1L))     |> overflows
        (fun () -> DayNumber64.MaxValue.PlusDays(1)) |> overflows
        (fun () -> DayNumber64.MaxValue.NextDay())   |> overflows

    [<Fact>]
    let ``DayNumber64.MaxValue - Int64.MaxValue does not overflow`` () =
        DayNumber64.MaxValue - Int64.MaxValue          === DayNumber64.Zero - 1L
        DayNumber64.MaxValue.PlusDays(-Int64.MaxValue) === DayNumber64.Zero - 1L

    //
    // Difference
    //

    [<Fact>]
    let ``DayNumber64.MaxValue - DayNumber64.MinValue overflows`` () =
        (fun () -> DayNumber64.MaxValue - DayNumber64.MinValue) |> overflows

    //
    // Operations
    //

    // fsharplint:disable Hints
    [<Property>]
    let ``0 is a neutral element (operators)`` (x: DayNumber64) =
        (x + 0 = x)
        .&. (x - 0L = x)
        .&. (x - x = 0)
    // fsharplint:enable

    [<Property>]
    let ``0 is a neutral element (methods)`` (x: DayNumber64) =
        (x.PlusDays(0) = x)
        .&. (x.CountDaysSince(x) = 0)

    [<Property>]
    let ``Addition and subtraction operators`` () = xynArbitrary @@@@ fun (x, y, n) ->
        (x + n = y)
        .&. (y - n = x)

    [<Property>]
    let ``Difference operator`` () = xynArbitrary @@@@ fun (x, y, n) ->
        (y - x = n)
        .&. (x - y = -n)

    [<Property>]
    let ``Increment operator`` (x: DayNumber64) = x <> DayNumber64.MaxValue &&&& lazy (
        DayNumber64.op_Increment(x) = x + 1
    )

    [<Property>]
    let ``Decrement operator`` (x: DayNumber64) = x <> DayNumber64.MaxValue &&&& lazy (
        DayNumber64.op_Decrement(x) = x - 1L
    )

    [<Property>]
    let ``NextDay()`` (x: DayNumber64) = x <> DayNumber64.MaxValue &&&& lazy (
        x.NextDay() = x + 1
    )

    [<Property>]
    let ``PreviousDay()`` (x: DayNumber64) = x <> DayNumber64.MinValue &&&& lazy (
        x.PreviousDay() = x - 1L
    )

    [<Property>]
    let ``PlusDays()`` () = xynArbitrary @@@@ fun (x, y, n) ->
        (x.PlusDays(n) = y)
        .&. (y.PlusDays(-n) = x)

    [<Property>]
    let ``CountDaysSince()`` () = xynArbitrary @@@@ fun (x, y, n) ->
        (y.CountDaysSince(x) = n)
        .&. (x.CountDaysSince(y) = -n)

module Postlude =
    //
    // Custom version of tests found in DayNumberTests
    //

    [<Fact>]
    let ``Static property DayZero64.NewStyle`` () =
        DayZero64.NewStyle === DayNumber64.Zero
        DayZero64.NewStyle.Ordinal === Ord64.First

        DayZero64.NewStyle === DayNumber64.FromDayNumber DayZero.NewStyle

    [<Fact>]
    let ``Static property DayZero64.OldStyle`` () =
        DayZero64.OldStyle === DayNumber64.Zero - 2L
        DayZero64.OldStyle.Ordinal === Ord64.First - 2L

        DayZero64.OldStyle === DayNumber64.FromDayNumber DayZero.OldStyle

    [<Fact>]
    let ``Static property DayZero64.RataDie`` () =
        DayZero64.RataDie === DayNumber64.Zero - 1L
        DayZero64.RataDie.Ordinal === Ord64.First - 1L

    //
    // Tests not found in DayNumberTests
    //

    [<Fact>]
    let ``Age of the universe`` () =
        // ~14 billion Julian years.
        let aof = DayNumber64.MinSupportedYear
        let dayNumber = DayNumber64.FromJulianParts(aof, 1, 1)

        dayNumber.DaysSinceZero === -5_113_500_000_002L

        DayZero64.RataDie === DayNumber64.FromDayNumber DayZero.RataDie

    //
    // Postlude for real
    //

    /// Compare the core properties.
    let rec private compareTypes (dayNumber: DayNumber64) (date: CivilDate) =
        let y, m, d = dayNumber.GetGregorianParts()
        let passed =
            int y = date.Year
            && m = date.Month
            && d = date.Day
            && dayNumber.DayOfWeek = date.DayOfWeek
            && dayNumber.IsoDayOfWeek = date.IsoDayOfWeek

        if passed then
            if dayNumber.DaysSinceZero = CivilDate.MaxDaysSinceEpoch then
                (true, "OK")
            else
                compareTypes (dayNumber.NextDay()) (date.NextDay())
        else
            (false, sprintf "First failure: %O." dayNumber)

    [<Fact>]
    [<TestPerformance(TestPerformance.SlowUnit)>]
    [<TestExcludeFrom(TestExcludeFrom.CodeCoverage)>]
    let ``Deep comparison between DayNumber64 and CivilDate`` () =
        // NB: both start on Monday January 1, 1 (CE).
        compareTypes DayNumber64.Zero CivilDate.MinValue |> Assert.True
