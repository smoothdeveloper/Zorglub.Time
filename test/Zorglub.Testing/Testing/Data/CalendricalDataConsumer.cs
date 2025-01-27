﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data;

/// <summary>
/// Provides static access to calendrical data.
/// </summary>
public abstract class CalendricalDataConsumer<TDataSet>
    where TDataSet : ICalendricalDataSet, ISingleton<TDataSet>
{
    // NB: the order of fields is important.
    private static readonly TDataSet s_DataSet = TDataSet.Instance;
    private static readonly DataSetAdapter s_Adapter = new(s_DataSet);

    protected CalendricalDataConsumer() { }

    protected static TDataSet DataSet => s_DataSet;

    protected static int SampleCommonYear { get; } = s_DataSet.SampleCommonYear;
    protected static int SampleLeapYear { get; } = s_DataSet.SampleLeapYear;

    public static XunitData<DaysSinceEpochInfo> DaysSinceEpochInfoData => s_Adapter.DaysSinceEpochInfoData;

    public static XunitData<DateInfo> DateInfoData => s_Adapter.DateInfoData;
    public static XunitData<MonthInfo> MonthInfoData => s_Adapter.MonthInfoData;
    public static XunitData<YearInfo> YearInfoData => s_Adapter.YearInfoData;
    public static XunitData<CenturyInfo> CenturyInfoData => s_Adapter.CenturyInfoData;

    public static XunitData<YemodaAnd<int>> DaysInYearAfterDateData => s_Adapter.DaysInYearAfterDateData;
    public static XunitData<YemodaAnd<int>> DaysInMonthAfterDateData => s_Adapter.DaysInMonthAfterDateData;

    public static XunitData<Yemoda> StartOfYearPartsData => s_Adapter.StartOfYearPartsData;
    public static XunitData<Yemoda> EndOfYearPartsData => s_Adapter.EndOfYearPartsData;

    public static XunitData<YearDaysSinceEpoch> StartOfYearDaysSinceEpochData => s_Adapter.StartOfYearDaysSinceEpochData;
    public static XunitData<YearDaysSinceEpoch> EndOfYearDaysSinceEpochData => s_Adapter.EndOfYearDaysSinceEpochData;

    public static TheoryData<int, int> InvalidMonthFieldData => s_DataSet.InvalidMonthFieldData;
    public static TheoryData<int, int, int> InvalidDayFieldData => s_DataSet.InvalidDayFieldData;
    public static TheoryData<int, int> InvalidDayOfYearFieldData => s_DataSet.InvalidDayOfYearFieldData;

    private sealed class DataSetAdapter
    {
        private readonly TDataSet _dataSet;

        public DataSetAdapter(TDataSet dataSet)
        {
            _dataSet = dataSet ?? throw new ArgumentNullException(nameof(dataSet));
        }

        private XunitData<DaysSinceEpochInfo>? _daysSinceEpochInfoData;
        public XunitData<DaysSinceEpochInfo> DaysSinceEpochInfoData =>
            _daysSinceEpochInfoData ??= _dataSet.DaysSinceEpochInfoData.ToXunitData();

        private XunitData<DateInfo>? _dateInfoData;
        public XunitData<DateInfo> DateInfoData =>
            _dateInfoData ??= _dataSet.DateInfoData.ToXunitData();

        private XunitData<MonthInfo>? _monthInfoData;
        public XunitData<MonthInfo> MonthInfoData =>
            _monthInfoData ??= _dataSet.MonthInfoData.ToXunitData();

        private XunitData<YearInfo>? _yearInfoData;
        public XunitData<YearInfo> YearInfoData =>
            _yearInfoData ??= _dataSet.YearInfoData.ToXunitData();

        private XunitData<CenturyInfo>? _centuryInfoData;
        public XunitData<CenturyInfo> CenturyInfoData =>
            _centuryInfoData ??= _dataSet.CenturyInfoData.ToXunitData();

        private XunitData<YemodaAnd<int>>? _daysInYearAfterDateData;
        public XunitData<YemodaAnd<int>> DaysInYearAfterDateData =>
            _daysInYearAfterDateData ??= _dataSet.DaysInYearAfterDateData.ToXunitData();

        private XunitData<YemodaAnd<int>>? _daysInMonthAfterDateData;
        public XunitData<YemodaAnd<int>> DaysInMonthAfterDateData =>
            _daysInMonthAfterDateData ??= _dataSet.DaysInMonthAfterDateData.ToXunitData();

        private XunitData<Yemoda>? _startOfYearPartsData;
        public XunitData<Yemoda> StartOfYearPartsData =>
            _startOfYearPartsData ??= _dataSet.StartOfYearPartsData.ToXunitData();

        private XunitData<Yemoda>? _endOfYearPartsData;
        public XunitData<Yemoda> EndOfYearPartsData =>
            _endOfYearPartsData ??= _dataSet.EndOfYearPartsData.ToXunitData();

        private XunitData<YearDaysSinceEpoch>? _startOfYearDaysSinceEpochData;
        public XunitData<YearDaysSinceEpoch> StartOfYearDaysSinceEpochData =>
            _startOfYearDaysSinceEpochData ??= _dataSet.StartOfYearDaysSinceEpochData.ToXunitData();

        private XunitData<YearDaysSinceEpoch>? _endOfYearDaysSinceEpochData;
        public XunitData<YearDaysSinceEpoch> EndOfYearDaysSinceEpochData =>
            _endOfYearDaysSinceEpochData ??= _dataSet.EndOfYearDaysSinceEpochData.ToXunitData();
    }
}
