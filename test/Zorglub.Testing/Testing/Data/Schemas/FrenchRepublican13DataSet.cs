﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data.Schemas;

using Zorglub.Time.Hemerology;

/// <summary>
/// Provides test data for <see cref="FrenchRepublican13Schema"/>.
/// </summary>
public sealed partial class FrenchRepublican13DataSet :
    SchemaDataSet, IEpagomenalDataSet, ISingleton<FrenchRepublican13DataSet>
{
    public const int CommonYear = 1;
    public const int LeapYear = 4;
    public const int CommonCentury = 4000;
    public const int LeapCentury = 400;

    private FrenchRepublican13DataSet() : base(new FrenchRepublican13Schema(), CommonYear, LeapYear) { }

    public static FrenchRepublican13DataSet Instance => Singleton.Instance;

    private static class Singleton
    {
        internal static readonly FrenchRepublican13DataSet Instance = new();
        static Singleton() { }
    }
}

public partial class FrenchRepublican13DataSet // Infos
{
    public override DataGroup<DaysSinceEpochInfo> DaysSinceEpochInfoData { get; } =
        DataGroup.CreateDaysSinceEpochInfoData(DaysSinceRataDieInfos, CalendarEpoch.FrenchRepublican);

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
        new(CommonYear, 13, 1, 361, false, true),
        new(CommonYear, 13, 2, 362, false, true),
        new(CommonYear, 13, 3, 363, false, true),
        new(CommonYear, 13, 4, 364, false, true),
        new(CommonYear, 13, 5, 365, false, true),
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
        new(LeapYear, 13, 1, 361, false, true),
        new(LeapYear, 13, 2, 362, false, true),
        new(LeapYear, 13, 3, 363, false, true),
        new(LeapYear, 13, 4, 364, false, true),
        new(LeapYear, 13, 5, 365, false, true),
        new(LeapYear, 13, 6, 366, true, true),
        // Common century.
        new(CommonCentury, 1, 1, 1, false, false),
        new(CommonCentury, 1, 30, 30, false, false),
        new(CommonCentury, 2, 30, 60, false, false),
        new(CommonCentury, 3, 30, 90, false, false),
        new(CommonCentury, 4, 30, 120, false, false),
        new(CommonCentury, 5, 30, 150, false, false),
        new(CommonCentury, 6, 30, 180, false, false),
        new(CommonCentury, 7, 30, 210, false, false),
        new(CommonCentury, 8, 30, 240, false, false),
        new(CommonCentury, 9, 30, 270, false, false),
        new(CommonCentury, 10, 30, 300, false, false),
        new(CommonCentury, 11, 30, 330, false, false),
        new(CommonCentury, 12, 30, 360, false, false),
        new(CommonCentury, 13, 1, 361, false, true),
        new(CommonCentury, 13, 2, 362, false, true),
        new(CommonCentury, 13, 3, 363, false, true),
        new(CommonCentury, 13, 4, 364, false, true),
        new(CommonCentury, 13, 5, 365, false, true),
        // Leap century.
        new(LeapCentury, 1, 1, 1, false, false),
        new(LeapCentury, 1, 30, 30, false, false),
        new(LeapCentury, 2, 30, 60, false, false),
        new(LeapCentury, 3, 30, 90, false, false),
        new(LeapCentury, 4, 30, 120, false, false),
        new(LeapCentury, 5, 30, 150, false, false),
        new(LeapCentury, 6, 30, 180, false, false),
        new(LeapCentury, 7, 30, 210, false, false),
        new(LeapCentury, 8, 30, 240, false, false),
        new(LeapCentury, 9, 30, 270, false, false),
        new(LeapCentury, 10, 30, 300, false, false),
        new(LeapCentury, 11, 30, 330, false, false),
        new(LeapCentury, 12, 30, 360, false, false),
        new(LeapCentury, 13, 1, 361, false, true),
        new(LeapCentury, 13, 2, 362, false, true),
        new(LeapCentury, 13, 3, 363, false, true),
        new(LeapCentury, 13, 4, 364, false, true),
        new(LeapCentury, 13, 5, 365, false, true),
        new(LeapCentury, 13, 6, 366, true, true),
    };

    public override DataGroup<MonthInfo> MonthInfoData { get; } = new()
    {
        // Année commune.
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
        new(CommonYear, 12, 30, 330, 5, false),
        new(CommonYear, 13, 5, 360, 0, false),
        // Année bissextile.
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
        new(LeapYear, 12, 30, 330, 6, false),
        new(LeapYear, 13, 6, 360, 0, false),
        // Année séculaire commune.
        new(CommonCentury, 1, 30, 0, 335, false),
        new(CommonCentury, 2, 30, 30, 305, false),
        new(CommonCentury, 3, 30, 60, 275, false),
        new(CommonCentury, 4, 30, 90, 245, false),
        new(CommonCentury, 5, 30, 120, 215, false),
        new(CommonCentury, 6, 30, 150, 185, false),
        new(CommonCentury, 7, 30, 180, 155, false),
        new(CommonCentury, 8, 30, 210, 125, false),
        new(CommonCentury, 9, 30, 240, 95, false),
        new(CommonCentury, 10, 30, 270, 65, false),
        new(CommonCentury, 11, 30, 300, 35, false),
        new(CommonCentury, 12, 30, 330, 5, false),
        new(CommonCentury, 13, 5, 360, 0, false),
        // Année séculaire bissextile.
        new(LeapCentury, 1, 30, 0, 336, false),
        new(LeapCentury, 2, 30, 30, 306, false),
        new(LeapCentury, 3, 30, 60, 276, false),
        new(LeapCentury, 4, 30, 90, 246, false),
        new(LeapCentury, 5, 30, 120, 216, false),
        new(LeapCentury, 6, 30, 150, 186, false),
        new(LeapCentury, 7, 30, 180, 156, false),
        new(LeapCentury, 8, 30, 210, 126, false),
        new(LeapCentury, 9, 30, 240, 96, false),
        new(LeapCentury, 10, 30, 270, 66, false),
        new(LeapCentury, 11, 30, 300, 36, false),
        new(LeapCentury, 12, 30, 330, 6, false),
        new(LeapCentury, 13, 6, 360, 0, false),
    };

    public override DataGroup<YearInfo> YearInfoData { get; } = new()
    {
        new(-9, 13, 365, false),
        new(-8, 13, 366, true),
        new(-7, 13, 365, false),
        new(-6, 13, 365, false),
        new(-5, 13, 365, false),
        new(-4, 13, 366, true),
        new(-3, 13, 365, false),
        new(-2, 13, 365, false),
        new(-1, 13, 365, false),
        new(0, 13, 365, false), // année séculaire divisible par 400 et 4000.
        new(CommonYear, 13, 365, false),
        new(2, 13, 365, false),
        new(3, 13, 365, false),
        new(LeapYear, 13, 366, true),
        new(5, 13, 365, false),
        new(6, 13, 365, false),
        new(7, 13, 365, false),
        new(8, 13, 366, true),
        new(9, 13, 365, false),
        // Années séculaires non divisibles par 400.
        new(-900, 13, 365, false),
        new(-700, 13, 365, false),
        new(-600, 13, 365, false),
        new(-500, 13, 365, false),
        new(-300, 13, 365, false),
        new(-200, 13, 365, false),
        new(-100, 13, 365, false),
        new(100, 13, 365, false),
        new(200, 13, 365, false),
        new(300, 13, 365, false),
        new(500, 13, 365, false),
        new(600, 13, 365, false),
        new(700, 13, 365, false),
        new(900, 13, 365, false),
        // Années séculaires multiples de 400 mais pas de 4000.
        new(-800, 13, 366, true),
        new(-400, 13, 366, true),
        new(LeapCentury, 13, 366, true),
        new(800, 13, 366, true),
        // Années séculaires mutiples de 4000.
        new(-8000, 13, 365, false),
        new(-4000, 13, 365, false),
        new(CommonCentury, 13, 365, false),
        new(8000, 13, 365, false),
    };

    internal static IEnumerable<DaysSinceRataDieInfo> DaysSinceRataDieInfos
    {
        get
        {
            // Epoch.
            yield return new(654_415, 1, 1, 1);

            // D.&R. Appendix C.
            yield return new(-214_193, -2378, 11, 4);
            yield return new(-61_387, -1959, 3, 13);
            yield return new(25_469, -1721, 1, 2);
            yield return new(49_217, -1656, 1, 10);
            yield return new(171_307, -1322, 4, 18);
            yield return new(210_155, -1216, 9, 1);
            yield return new(253_427, -1097, 2, 19);
            yield return new(369_740, -779, 8, 4);
            yield return new(400_085, -696, 9, 5);
            yield return new(434_355, -602, 7, 1);
            yield return new(452_605, -552, 6, 20);
            yield return new(470_160, -504, 7, 13);
            yield return new(473_837, -494, 8, 8);
            yield return new(507_850, -401, 9, 23);
            yield return new(524_156, -356, 5, 13);
            yield return new(544_676, -300, 7, 19);
            yield return new(567_118, -239, 13, 1);
            yield return new(569_477, -232, 6, 14);
            yield return new(601_716, -144, 9, 22);
            yield return new(613_424, -112, 10, 12);
            yield return new(626_596, -76, 11, 6);
            yield return new(645_554, -24, 10, 1);
            yield return new(664_224, 27, 11, 14);
            yield return new(671_401, 47, 7, 6);
            yield return new(694_799, 111, 7, 29);
            yield return new(704_424, 137, 12, 7);
            yield return new(708_842, 150, 1, 7);
            yield return new(709_409, 151, 7, 29);
            yield return new(709_580, 152, 1, 15);
            yield return new(727_274, 200, 6, 27);
            yield return new(728_714, 204, 6, 7);
            yield return new(744_313, 247, 2, 20);
            yield return new(764_652, 302, 11, 1);
        }
    }
}

public partial class FrenchRepublican13DataSet // Start and end of year
{
    public override DataGroup<Yemoda> EndOfYearPartsData { get; } = new()
    {
        new(CommonYear, 13, 5),
        new(LeapYear, 13, 6),
        new(CommonCentury, 13, 5),
        new(LeapCentury, 13, 6),
    };

    public override DataGroup<YearDaysSinceEpoch> StartOfYearDaysSinceEpochData { get; } = new()
    {
        new(-3999, -(int)FrenchRepublican13Schema.DaysPer4000YearCycle),
        new(-10, -4017),
        new(-9, -3652),
        new(-8, -3287), // leap year
        new(-7, -2921),
        new(-6, -2556),
        new(-5, -2191),
        new(-4, -1826), // leap year
        new(-3, -1460),
        new(-2, -1095),
        new(-1, -730),
        new(0, -365),
        new(1, 0),
        new(2, 365),
        new(3, 730),
        new(4, 1095), // leap year
        new(5, 1461),
        new(6, 1826),
        new(7, 2191),
        new(8, 2556), // leap year
        new(9, 2922),
        new(10, 3287),
        new(4001, (int)FrenchRepublican13Schema.DaysPer4000YearCycle),
    };
}

public partial class FrenchRepublican13DataSet // Invalid date parts
{
    public override TheoryData<int, int> InvalidMonthFieldData { get; } = new()
    {
        { CommonYear, 0 },
        { CommonYear, 14 },
        { LeapYear, 0 },
        { LeapYear, 14 },
        { CommonCentury, 0 },
        { CommonCentury, 14 },
        { LeapCentury, 0 },
        { LeapCentury, 14 },
    };

    public override TheoryData<int, int> InvalidDayOfYearFieldData { get; } = new()
    {
        { CommonYear, 0 },
        { CommonYear, 366 },
        { LeapYear, 0 },
        { LeapYear, 367 },
        { CommonCentury, 0 },
        { CommonCentury, 366 },
        { LeapCentury, 0 },
        { LeapCentury, 367 },
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
        { CommonYear, 12, 31 },
        { CommonYear, 13, 0 },
        { CommonYear, 13, 6 },
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
        { LeapYear, 12, 31 },
        { LeapYear, 13, 0 },
        { LeapYear, 13, 7 },
        // Common century.
        { CommonCentury, 1, 0 },
        { CommonCentury, 1, 31 },
        { CommonCentury, 2, 0 },
        { CommonCentury, 2, 31 },
        { CommonCentury, 3, 0 },
        { CommonCentury, 3, 31 },
        { CommonCentury, 4, 0 },
        { CommonCentury, 4, 31 },
        { CommonCentury, 5, 0 },
        { CommonCentury, 5, 31 },
        { CommonCentury, 6, 0 },
        { CommonCentury, 6, 31 },
        { CommonCentury, 7, 0 },
        { CommonCentury, 7, 31 },
        { CommonCentury, 8, 0 },
        { CommonCentury, 8, 31 },
        { CommonCentury, 9, 0 },
        { CommonCentury, 9, 31 },
        { CommonCentury, 10, 0 },
        { CommonCentury, 10, 31 },
        { CommonCentury, 11, 0 },
        { CommonCentury, 11, 31 },
        { CommonCentury, 12, 0 },
        { CommonCentury, 12, 31 },
        { CommonCentury, 13, 0 },
        { CommonCentury, 13, 6 },
        // Leap century.
        { LeapCentury, 1, 0 },
        { LeapCentury, 1, 31 },
        { LeapCentury, 2, 0 },
        { LeapCentury, 2, 31 },
        { LeapCentury, 3, 0 },
        { LeapCentury, 3, 31 },
        { LeapCentury, 4, 0 },
        { LeapCentury, 4, 31 },
        { LeapCentury, 5, 0 },
        { LeapCentury, 5, 31 },
        { LeapCentury, 6, 0 },
        { LeapCentury, 6, 31 },
        { LeapCentury, 7, 0 },
        { LeapCentury, 7, 31 },
        { LeapCentury, 8, 0 },
        { LeapCentury, 8, 31 },
        { LeapCentury, 9, 0 },
        { LeapCentury, 9, 31 },
        { LeapCentury, 10, 0 },
        { LeapCentury, 10, 31 },
        { LeapCentury, 11, 0 },
        { LeapCentury, 11, 31 },
        { LeapCentury, 12, 0 },
        { LeapCentury, 12, 31 },
        { LeapCentury, 13, 0 },
        { LeapCentury, 13, 7 },
    };
}

public partial class FrenchRepublican13DataSet // IEpagomenalDataSet
{
    public DataGroup<YemodaAnd<int>> EpagomenalDayInfoData { get; } = new()
    {
        // Common year.
        new(CommonYear, 13, 1, 1),
        new(CommonYear, 13, 2, 2),
        new(CommonYear, 13, 3, 3),
        new(CommonYear, 13, 4, 4),
        new(CommonYear, 13, 5, 5),
        // Leap year.
        new(LeapYear, 13, 1, 1),
        new(LeapYear, 13, 2, 2),
        new(LeapYear, 13, 3, 3),
        new(LeapYear, 13, 4, 4),
        new(LeapYear, 13, 5, 5),
        new(LeapYear, 13, 6, 6),
    };
}
