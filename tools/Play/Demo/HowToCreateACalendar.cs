﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Play.Demo;

using Samples;

using Zorglub.Time;
using Zorglub.Time.Core.Schemas;
using Zorglub.Time.Core.Utilities;
using Zorglub.Time.Hemerology;
using Zorglub.Time.Simple;

using static Zorglub.Time.Extensions.BoxExtensions;
using static Zorglub.Time.Extensions.Unboxing;

public static class HowToCreateACalendar
{
    #region Create a simple calendar

    public static Calendar CreateCalendar() =>
        GregorianSchema.GetInstance()
            .CreateCalendar("CreateCalendar", DayZero.NewStyle, proleptic: true);

    // Hand-written version.
    public static Calendar CreateCalendar_Plain() =>
        (from x in GregorianSchema.GetInstance()
         select CalendarCatalog.Add("CreateCalendar_HWV", x, DayZero.NewStyle, proleptic: true)
         ).Unbox();

    #endregion
    #region Try create a simple calendar

    public static bool TryCreateCalendar(out Calendar? calendar) =>
        GregorianSchema.GetInstance()
            .TryCreateCalendar("TryCreateCalendar", DayZero.NewStyle, out calendar, proleptic: true);

    // Hand-written version.
    public static Calendar TryCreateCalendar_Plain()
    {
        return GregorianSchema.GetInstance().Select(TryAdd).Unbox();

        static Calendar? TryAdd(GregorianSchema schema)
        {
            _ = CalendarCatalog.TryAdd(
                "TryCreateCalendar_HWV", schema, DayZero.NewStyle, proleptic: true, out var chr);

            return chr;
        }
    }

    #endregion

    // Pre-defined calendar.
    public static MinMaxYearCalendar CreateMinMaxYearCalendar() =>
         (from x in GregorianSchema.GetInstance()
          select new MinMaxYearCalendar("CreateMinMaxYearCalendar", x, DayZero.NewStyle, 1, 9999)
          ).Unbox();

    // User-defined calendar.
    public static MyNakedCalendar CreateNakedCalendar() =>
        (from x in GregorianSchema.GetInstance()
         select new MyNakedCalendar("CreateNakedCalendar", x, DayZero.NewStyle)
         ).Unbox();

    #region Scopes

    public static Box<MinMaxYearScope> GetScope() =>
        GregorianSchema.GetInstance()
            .Select(x => new MinMaxYearScope(x, DayZero.NewStyle, 1, 9999));

    public static Box<MinMaxYearScope> GetScope_QEP() =>
        from x in GregorianSchema.GetInstance()
        select new MinMaxYearScope(x, DayZero.NewStyle, 1, 9999);

    private static Box<MinMaxYearScope> GetScope(Box<GregorianSchema> schema) =>
        schema.Select(x => new MinMaxYearScope(x, DayZero.NewStyle, 1, 9999));

    #endregion

    // Ways to construct the same calendar.

    public static MyNakedCalendar Select_QEP() =>
        (from x in GregorianSchema.GetInstance()
         let y = new MinMaxYearScope(x, DayZero.NewStyle, 1, 9999)
         select new MyNakedCalendar("Select_QEP", x, y)
         ).Unbox();

    // Versions below are here to demonstrate ZipWith() and SelectMany(),
    // but Select_QEP() is a better and simpler solution; the intermediate
    // Box<MinMaxYearScope> is unnecessary.
    // Prerequisite: BoxExtensions from Zorglub.Sketches.

    public static MyNakedCalendar ZipWith()
    {
        Box<GregorianSchema> schema = GregorianSchema.GetInstance();
        Box<MinMaxYearScope> scope = GetScope(schema);

        return schema.ZipWith(scope, (x, y) => new MyNakedCalendar("ZipWith", x, y)).Unbox();
    }

    public static MyNakedCalendar SelectMany_QEP()
    {
        Box<GregorianSchema> schema = GregorianSchema.GetInstance();
        Box<MinMaxYearScope> scope = GetScope(schema);

        return (from x in schema
                from y in scope
                select new MyNakedCalendar("SelectMany_QEP", x, y)
                ).Unbox();
    }

    // Abscons.
    public static MyNakedCalendar SelectMany()
    {
        Box<GregorianSchema> schema = GregorianSchema.GetInstance();
        Box<MinMaxYearScope> scope = GetScope(schema);

        return schema
            .SelectMany(
                _ => scope,
                (x, y) => new MyNakedCalendar("SelectMany", x, y))
            .Unbox();
    }
}
