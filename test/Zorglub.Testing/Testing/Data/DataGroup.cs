﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data;

using System.Linq;

/// <summary>
/// Provides factory methods for <see cref="DataGroup{T}"/>.
/// </summary>
public static class DataGroup
{
    [Pure]
    public static DataGroup<T> Create<T>(IEnumerable<T> source)
    {
        return new DataGroup<T>(source);
    }

    [Pure]
    public static DataGroup<TResult> Create<TSource, TResult>(
        IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        var q = source.Select(selector);
        return new DataGroup<TResult>(q);
    }
}

public static class DaysSinceEpochInfoDataGroup
{
    [Pure]
    public static DataGroup<DaysSinceEpochInfo> Create<T>(IEnumerable<T> source, DayNumber epoch)
        where T : IConvertibleToDayNumberInfo
    {
        var q = from x in source select x.ToDayNumberInfo() - epoch;
        return new DataGroup<DaysSinceEpochInfo>(q);
    }
}

public static class DayNumberInfoDataGroup
{
    [Pure]
    public static DataGroup<DayNumberInfo> Create<T>(IEnumerable<T> source)
        where T : IConvertibleToDayNumberInfo
    {
        var q = from x in source select x.ToDayNumberInfo();
        return new DataGroup<DayNumberInfo>(q);
    }

    [Pure]
    public static DataGroup<DayNumberInfo> Create<T>(
        IEnumerable<T> source, DayNumber sourceEpoch, DayNumber resultEpoch)
        where T : IConvertibleToDayNumberInfo
    {
        int shift = resultEpoch - sourceEpoch;
        var q = from x in source select x.ToDayNumberInfo() + shift;
        return new DataGroup<DayNumberInfo>(q);
    }

    [Pure]
    public static DataGroup<DayNumberInfo> Create(IEnumerable<DaysSinceEpochInfo> source, DayNumber epoch)
    {
        var q = from x in source select x.ToDayNumberInfo(epoch);
        return new DataGroup<DayNumberInfo>(q);
    }
}

public sealed class DataGroup<T> : TheoryData<T>, IEnumerable<T>
{
    private readonly IEnumerable<T> _seq;

    public DataGroup(IEnumerable<T> seq)
    {
        _seq = seq ?? throw new ArgumentNullException(nameof(seq));

        foreach (T item in seq) { AddRow(item); }
    }

    [Pure]
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => _seq.GetEnumerator();
}