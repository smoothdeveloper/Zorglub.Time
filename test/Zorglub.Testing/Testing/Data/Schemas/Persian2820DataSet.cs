﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data.Schemas;

using Zorglub.Time.Hemerology;

/// <summary>
/// Provides test data for <see cref="Persian2820Schema"/>.
/// </summary>
public sealed partial class Persian2820DataSet : SchemaDataSet, ISingleton<Persian2820DataSet>
{
    public const int CommonYear = 1;
    public const int LeapYear = 4;

    private Persian2820DataSet() : base(new Persian2820Schema(), CommonYear, LeapYear) { }

    public static Persian2820DataSet Instance => Singleton.Instance;

    private static class Singleton
    {
        internal static readonly Persian2820DataSet Instance = new();
        static Singleton() { }
    }
}

public partial class Persian2820DataSet // Infos
{
    public override DataGroup<DaysSinceEpochInfo> DaysSinceEpochInfoData { get; } =
        DataGroup.CreateDaysSinceEpochInfoData(DaysSinceRataDieInfos, CalendarEpoch.Persian);

    public override DataGroup<DateInfo> DateInfoData { get; } = new()
    {
        // Common year.
        new(CommonYear, 1, 1, 1, false, false),
        new(CommonYear, 1, 31, 31, false, false),
        new(CommonYear, 2, 31, 62, false, false),
        new(CommonYear, 3, 31, 93, false, false),
        new(CommonYear, 4, 31, 124, false, false),
        new(CommonYear, 5, 31, 155, false, false),
        new(CommonYear, 6, 31, 186, false, false),
        new(CommonYear, 7, 30, 216, false, false),
        new(CommonYear, 8, 30, 246, false, false),
        new(CommonYear, 9, 30, 276, false, false),
        new(CommonYear, 10, 30, 306, false, false),
        new(CommonYear, 11, 30, 336, false, false),
        new(CommonYear, 12, 29, 365, false, false),
        // Leap year.
        new(LeapYear, 1, 1, 1, false, false),
        new(LeapYear, 1, 31, 31, false, false),
        new(LeapYear, 2, 31, 62, false, false),
        new(LeapYear, 3, 31, 93, false, false),
        new(LeapYear, 4, 31, 124, false, false),
        new(LeapYear, 5, 31, 155, false, false),
        new(LeapYear, 6, 31, 186, false, false),
        new(LeapYear, 7, 30, 216, false, false),
        new(LeapYear, 8, 30, 246, false, false),
        new(LeapYear, 9, 30, 276, false, false),
        new(LeapYear, 10, 30, 306, false, false),
        new(LeapYear, 11, 30, 336, false, false),
        new(LeapYear, 12, 29, 365, false, false),
        new(LeapYear, 12, 30, 366, true, false),
    };

    public override DataGroup<MonthInfo> MonthInfoData { get; } = new()
    {
        // Common year.
        new(CommonYear, 1, 31, 0, 334, false),
        new(CommonYear, 2, 31, 31, 303, false),
        new(CommonYear, 3, 31, 62, 272, false),
        new(CommonYear, 4, 31, 93, 241, false),
        new(CommonYear, 5, 31, 124, 210, false),
        new(CommonYear, 6, 31, 155, 179, false),
        new(CommonYear, 7, 30, 186, 149, false),
        new(CommonYear, 8, 30, 216, 119, false),
        new(CommonYear, 9, 30, 246, 89, false),
        new(CommonYear, 10, 30, 276, 59, false),
        new(CommonYear, 11, 30, 306, 29, false),
        new(CommonYear, 12, 29, 336, 0, false),
        // Leap year.
        new(LeapYear, 1, 31, 0, 335, false),
        new(LeapYear, 2, 31, 31, 304, false),
        new(LeapYear, 3, 31, 62, 273, false),
        new(LeapYear, 4, 31, 93, 242, false),
        new(LeapYear, 5, 31, 124, 211, false),
        new(LeapYear, 6, 31, 155, 180, false),
        new(LeapYear, 7, 30, 186, 150, false),
        new(LeapYear, 8, 30, 216, 120, false),
        new(LeapYear, 9, 30, 246, 90, false),
        new(LeapYear, 10, 30, 276, 60, false),
        new(LeapYear, 11, 30, 306, 30, false),
        new(LeapYear, 12, 30, 336, 0, false),
    };

    public override DataGroup<YearInfo> YearInfoData { get; } = new()
    {
        new(0, 12, 366, true),
        new(CommonYear, 12, 365, false),
        new(2, 12, 365, false),
        new(3, 12, 365, false),
        new(LeapYear, 12, 366, true),

        // On teste plus en profondeur la propriété isLeapYear dans
        // PersianSchemaTests.
        // First whole 2820-year cycle.
        // First 29-year subcycle of the first 128-year cycle.
        new(475, 12, 365, false),
        new(476, 12, 365, false),
        new(477, 12, 365, false),
        new(478, 12, 365, false),
        new(479, 12, 366, true), // leap
        new(480, 12, 365, false),
        new(481, 12, 365, false),
        new(482, 12, 365, false),
        new(483, 12, 366, true), // leap
        new(484, 12, 365, false),
        // Last 37-year subcycle of the 132-year cycle.
        new(3285, 12, 365, false),
        new(3286, 12, 366, true), // leap
        new(3287, 12, 365, false),
        new(3288, 12, 365, false),
        new(3289, 12, 365, false),
        new(3290, 12, 366, true), // leap
        new(3291, 12, 365, false),
        new(3292, 12, 365, false),
        new(3293, 12, 365, false),
        new(3294, 12, 366, true), // leap
    };

    internal static IEnumerable<DaysSinceRataDieInfo> DaysSinceRataDieInfos
    {
        get
        {
            // Epoch.
            yield return new(226_529, -1, 12, 29);
            yield return new(226_530, 0, 1, 1);
            yield return new(226_895, 0, 12, 30);
            yield return new(226_896, 1, 1, 1);
            yield return new(226_897, 1, 1, 2);
            yield return new(227_260, 1, 12, 29);
            yield return new(227_261, 2, 1, 1);

            // Special data to test the limits of a 2820-year cycle.
            // See PersianData.GetYearyield return new().
            // First 2820-year cycle: years 1-474.
            yield return new(400_020, 474, 12, 30);
            // Second cycle.
            yield return new(400_021, 475, 1, 1);
            yield return new(1_430_003, 3294, 12, 30);
            // Third cycle.
            yield return new(1_430_004, 3295, 1, 1);
            yield return new(2_459_986, 6114, 12, 30);

            // D.&R. Appendix C.
            yield return new(-214_193, -1207, 5, 1);
            yield return new(-61_387, -789, 9, 14);
            yield return new(25_469, -551, 7, 2);
            yield return new(49_217, -486, 7, 9);
            yield return new(171_307, -152, 10, 18);
            yield return new(210_155, -45, 2, 30);
            yield return new(253_427, 73, 8, 19);
            yield return new(369_740, 392, 2, 5);
            yield return new(400_085, 475, 3, 3);
            yield return new(434_355, 569, 1, 3);
            yield return new(452_605, 618, 12, 20);
            yield return new(470_160, 667, 1, 14);
            yield return new(473_837, 677, 2, 8);
            yield return new(507_850, 770, 3, 22);
            yield return new(524_156, 814, 11, 13);
            yield return new(544_676, 871, 1, 21);
            yield return new(567_118, 932, 6, 28);
            yield return new(569_477, 938, 12, 14);
            yield return new(601_716, 1027, 3, 21);
            yield return new(613_424, 1059, 4, 10);
            yield return new(626_596, 1095, 5, 2);
            yield return new(645_554, 1147, 3, 30);
            yield return new(664_224, 1198, 5, 10);
            yield return new(671_401, 1218, 1, 7);
            yield return new(694_799, 1282, 1, 29);
            yield return new(704_424, 1308, 6, 3);
            yield return new(708_842, 1320, 7, 7);
            yield return new(709_409, 1322, 1, 29);
            yield return new(709_580, 1322, 7, 14);
            yield return new(727_274, 1370, 12, 27);
            yield return new(728_714, 1374, 12, 6);
            yield return new(744_313, 1417, 8, 19);
            yield return new(764_652, 1473, 4, 28);
        }
    }
}

public partial class Persian2820DataSet // Start and end of year
{
    public override DataGroup<Yemoda> EndOfYearPartsData { get; } = new()
    {
        new(CommonYear, 12, 29),
        new(LeapYear, 12, 30),
    };

    public override DataGroup<YearDaysSinceEpoch> StartOfYearDaysSinceEpochData { get; } = new()
    {
        new(-2819, -Persian2820Schema.DaysPer2820YearCycle),
        new(-5, -2192),
        new(-4, -1827), // leap year
        new(-3, -1461),
        new(-2, -1096),
        new(-1, -731),
        new(0, -366), // leap year
        new(1, 0),
        new(2, 365),
        new(3, 730),
        new(4, 1095), // leap year
        new(5, 1461),
        new(2821, Persian2820Schema.DaysPer2820YearCycle),
    };
}

public partial class Persian2820DataSet // Invalid date parts
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
        { CommonYear, 1, 32 },
        { CommonYear, 2, 0 },
        { CommonYear, 2, 32 },
        { CommonYear, 3, 0 },
        { CommonYear, 3, 32 },
        { CommonYear, 4, 0 },
        { CommonYear, 4, 32 },
        { CommonYear, 5, 0 },
        { CommonYear, 5, 32 },
        { CommonYear, 6, 0 },
        { CommonYear, 6, 32 },
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
        { CommonYear, 12, 30 },
        // Leap year.
        { LeapYear, 1, 0 },
        { LeapYear, 1, 32 },
        { LeapYear, 2, 0 },
        { LeapYear, 2, 32 },
        { LeapYear, 3, 0 },
        { LeapYear, 3, 32 },
        { LeapYear, 4, 0 },
        { LeapYear, 4, 32 },
        { LeapYear, 5, 0 },
        { LeapYear, 5, 32 },
        { LeapYear, 6, 0 },
        { LeapYear, 6, 32 },
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
        { LeapYear, 12, 31 },
    };
}
