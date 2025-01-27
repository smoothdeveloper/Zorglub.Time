﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data.Schemas;

using Zorglub.Time.Hemerology;

/// <summary>
/// Provides test data for <see cref="Coptic12Schema"/>.
/// </summary>
public sealed partial class Coptic12DataSet :
    SchemaDataSet, IEpagomenalDataSet, ISingleton<Coptic12DataSet>
{
    public const int CommonYear = 1;
    public const int LeapYear = 3;

    private Coptic12DataSet() : base(new Coptic12Schema(), CommonYear, LeapYear) { }

    public static Coptic12DataSet Instance => Singleton.Instance;

    private static class Singleton
    {
        internal static readonly Coptic12DataSet Instance = new();
        static Singleton() { }
    }
}

public partial class Coptic12DataSet // Infos
{
    public override DataGroup<DaysSinceEpochInfo> DaysSinceEpochInfoData { get; } =
        DataGroup.CreateDaysSinceEpochInfoData(DaysSinceRataDieInfos, CalendarEpoch.Coptic);

    public override DataGroup<DateInfo> DateInfoData { get; } = new()
    {
        // Common year.
        new(CommonYear, 1, 1, 1, false, false),
        new(CommonYear, 1, 30, 30, false, false),
        new(CommonYear, 2, 30, 60, false, false),
        new(CommonYear, 3, 30, 90, false, false),
        new(CommonYear, 4, 30, 120, false, false),
        new(CommonYear, 5, 30, 150, false, false),
        new(CommonYear, 6, 30, 180, false, false),
        new(CommonYear, 7, 30, 210, false, false),
        new(CommonYear, 8, 30, 240, false, false),
        new(CommonYear, 9, 30, 270, false, false),
        new(CommonYear, 10, 30, 300, false, false),
        new(CommonYear, 11, 30, 330, false, false),
        new(CommonYear, 12, 30, 360, false, false),
        new(CommonYear, 12, 31, 361, false, true),
        new(CommonYear, 12, 32, 362, false, true),
        new(CommonYear, 12, 33, 363, false, true),
        new(CommonYear, 12, 34, 364, false, true),
        new(CommonYear, 12, 35, 365, false, true),
        // Leap year.
        new(LeapYear, 1, 1, 1, false, false),
        new(LeapYear, 1, 30, 30, false, false),
        new(LeapYear, 2, 30, 60, false, false),
        new(LeapYear, 3, 30, 90, false, false),
        new(LeapYear, 4, 30, 120, false, false),
        new(LeapYear, 5, 30, 150, false, false),
        new(LeapYear, 6, 30, 180, false, false),
        new(LeapYear, 7, 30, 210, false, false),
        new(LeapYear, 8, 30, 240, false, false),
        new(LeapYear, 9, 30, 270, false, false),
        new(LeapYear, 10, 30, 300, false, false),
        new(LeapYear, 11, 30, 330, false, false),
        new(LeapYear, 12, 30, 360, false, false),
        new(LeapYear, 12, 31, 361, false, true),
        new(LeapYear, 12, 32, 362, false, true),
        new(LeapYear, 12, 33, 363, false, true),
        new(LeapYear, 12, 34, 364, false, true),
        new(LeapYear, 12, 35, 365, false, true),
        new(LeapYear, 12, 36, 366, true, true),
    };

    public override DataGroup<MonthInfo> MonthInfoData { get; } = new()
    {
        // Common year.
        new(CommonYear, 1, 30, 0, 335, false),
        new(CommonYear, 2, 30, 30, 305, false),
        new(CommonYear, 3, 30, 60, 275, false),
        new(CommonYear, 4, 30, 90, 245, false),
        new(CommonYear, 5, 30, 120, 215, false),
        new(CommonYear, 6, 30, 150, 185, false),
        new(CommonYear, 7, 30, 180, 155, false),
        new(CommonYear, 8, 30, 210, 125, false),
        new(CommonYear, 9, 30, 240, 95, false),
        new(CommonYear, 10, 30, 270, 65, false),
        new(CommonYear, 11, 30, 300, 35, false),
        new(CommonYear, 12, 35, 330, 0, false),
        // Leap year.
        new(LeapYear, 1, 30, 0, 336, false),
        new(LeapYear, 2, 30, 30, 306, false),
        new(LeapYear, 3, 30, 60, 276, false),
        new(LeapYear, 4, 30, 90, 246, false),
        new(LeapYear, 5, 30, 120, 216, false),
        new(LeapYear, 6, 30, 150, 186, false),
        new(LeapYear, 7, 30, 180, 156, false),
        new(LeapYear, 8, 30, 210, 126, false),
        new(LeapYear, 9, 30, 240, 96, false),
        new(LeapYear, 10, 30, 270, 66, false),
        new(LeapYear, 11, 30, 300, 36, false),
        new(LeapYear, 12, 36, 330, 0, false),
    };

    public override DataGroup<YearInfo> YearInfoData { get; } = new()
    {
        new(-9, 12, 366, true),
        new(-8, 12, 365, false),
        new(-7, 12, 365, false),
        new(-6, 12, 365, false),
        new(-5, 12, 366, true),
        new(-4, 12, 365, false),
        new(-3, 12, 365, false),
        new(-2, 12, 365, false),
        new(-1, 12, 366, true),
        new(0, 12, 365, false),
        new(CommonYear, 12, 365, false),
        new(2, 12, 365, false),
        new(LeapYear, 12, 366, true),
        new(4, 12, 365, false),
        new(5, 12, 365, false),
        new(6, 12, 365, false),
        new(7, 12, 366, true),
        new(8, 12, 365, false),
        new(9, 12, 365, false),
    };

    internal static IEnumerable<DaysSinceRataDieInfo> DaysSinceRataDieInfos
    {
        get
        {
            // Epoch.
            yield return new(103_605, 1, 1, 1);

            // D.&R. Appendix C.
            yield return new(-214_193, -870, 12, 6);
            yield return new(-61_387, -451, 4, 12);
            yield return new(25_469, -213, 1, 29);
            yield return new(49_217, -148, 2, 5);
            yield return new(171_307, 186, 5, 12);
            yield return new(210_155, 292, 9, 23);
            yield return new(253_427, 411, 3, 11);
            yield return new(369_740, 729, 8, 24);
            yield return new(400_085, 812, 9, 23);
            yield return new(434_355, 906, 7, 20);
            yield return new(452_605, 956, 7, 7);
            yield return new(470_160, 1004, 7, 30);
            yield return new(473_837, 1014, 8, 25);
            yield return new(507_850, 1107, 10, 10);
            yield return new(524_156, 1152, 5, 29);
            yield return new(544_676, 1208, 8, 5);
            yield return new(567_118, 1270, 1, 12);
            yield return new(569_477, 1276, 6, 29);
            yield return new(601_716, 1364, 10, 6);
            yield return new(613_424, 1396, 10, 26);
            yield return new(626_596, 1432, 11, 19);
            yield return new(645_554, 1484, 10, 14);
            yield return new(664_224, 1535, 11, 27);
            yield return new(671_401, 1555, 7, 19);
            yield return new(694_799, 1619, 8, 11);
            yield return new(704_424, 1645, 12, 19);
            yield return new(708_842, 1658, 1, 19);
            yield return new(709_409, 1659, 8, 11);
            yield return new(709_580, 1660, 1, 26);
            yield return new(727_274, 1708, 7, 8);
            yield return new(728_714, 1712, 6, 17);
            yield return new(744_313, 1755, 3, 1);
            yield return new(764_652, 1810, 11, 11);
        }
    }
}

public partial class Coptic12DataSet // Start and end of year
{
    public override DataGroup<Yemoda> EndOfYearPartsData { get; } = new()
    {
        new(CommonYear, 12, 35),
        new(LeapYear, 12, 36),
    };

    public override DataGroup<YearDaysSinceEpoch> StartOfYearDaysSinceEpochData { get; } = new()
    {
        new(-10, -4018),
        new(-9, -3653), // leap year
        new(-8, -3287),
        new(-7, -2922),
        new(-6, -2557),
        new(-5, -2192), // leap year
        new(-4, -1826),
        new(-3, -Coptic12Schema.DaysPer4YearCycle),
        new(-2, -1096),
        new(-1, -731), // leap year
        new(0, -365),
        new(1, 0),
        new(2, 365),
        new(3, 730), // leap year
        new(4, 1096),
        new(5, Coptic12Schema.DaysPer4YearCycle),
        new(6, 1826),
        new(7, 2191), // leap year
        new(8, 2557),
        new(9, 2922),
        new(10, 3287),
    };
}

public partial class Coptic12DataSet // Invalid date parts
{
    public override TheoryData<int, int> InvalidMonthFieldData { get; } = new()
    {
        { CommonYear, 0 },
        { CommonYear, 13 },
        { LeapYear, 0 },
        { LeapYear, 13 },
    };

    public override TheoryData<int, int> InvalidDayOfYearFieldData { get; } = new()
    {
        { CommonYear, 0 },
        { CommonYear, 366 },
        { LeapYear, 0 },
        { LeapYear, 367 },
    };

    public override TheoryData<int, int, int> InvalidDayFieldData { get; } = new()
    {
        // Common year.
        { CommonYear, 1, 0 },
        { CommonYear, 1, 31 },
        { CommonYear, 2, 0 },
        { CommonYear, 2, 31 },
        { CommonYear, 3, 0 },
        { CommonYear, 3, 31 },
        { CommonYear, 4, 0 },
        { CommonYear, 4, 31 },
        { CommonYear, 5, 0 },
        { CommonYear, 5, 31 },
        { CommonYear, 6, 0 },
        { CommonYear, 6, 31 },
        { CommonYear, 7, 0 },
        { CommonYear, 7, 31 },
        { CommonYear, 8, 0 },
        { CommonYear, 8, 31 },
        { CommonYear, 9, 0 },
        { CommonYear, 9, 31 },
        { CommonYear, 10, 0 },
        { CommonYear, 10, 31 },
        { CommonYear, 11, 0 },
        { CommonYear, 11, 31 },
        { CommonYear, 12, 0 },
        { CommonYear, 12, 36 },
        // Leap year.
        { LeapYear, 1, 0 },
        { LeapYear, 1, 31 },
        { LeapYear, 2, 0 },
        { LeapYear, 2, 31 },
        { LeapYear, 3, 0 },
        { LeapYear, 3, 31 },
        { LeapYear, 4, 0 },
        { LeapYear, 4, 31 },
        { LeapYear, 5, 0 },
        { LeapYear, 5, 31 },
        { LeapYear, 6, 0 },
        { LeapYear, 6, 31 },
        { LeapYear, 7, 0 },
        { LeapYear, 7, 31 },
        { LeapYear, 8, 0 },
        { LeapYear, 8, 31 },
        { LeapYear, 9, 0 },
        { LeapYear, 9, 31 },
        { LeapYear, 10, 0 },
        { LeapYear, 10, 31 },
        { LeapYear, 11, 0 },
        { LeapYear, 11, 31 },
        { LeapYear, 12, 0 },
        { LeapYear, 12, 37 },
    };
}

public partial class Coptic12DataSet // IEpagomenalDataSet
{
    public DataGroup<YemodaAnd<int>> EpagomenalDayInfoData { get; } = new()
    {
        // Common year.
        new(CommonYear, 12, 31, 1),
        new(CommonYear, 12, 32, 2),
        new(CommonYear, 12, 33, 3),
        new(CommonYear, 12, 34, 4),
        new(CommonYear, 12, 35, 5),
        // Leap year.
        new(LeapYear, 12, 31, 1),
        new(LeapYear, 12, 32, 2),
        new(LeapYear, 12, 33, 3),
        new(LeapYear, 12, 34, 4),
        new(LeapYear, 12, 35, 5),
        new(LeapYear, 12, 36, 6),
    };
}
