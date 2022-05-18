﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Testing.Data;

using System.Collections;
using System.Linq;

// The advantage of a DataGroup<T> over a TheoryData<T> is that we can enumerate
// and manipulate a group of data using the actual underlying data type directly.
// This is particularly useful when dealing with <i>bounded</i> calendar datasets.
// If one does not need to transform the data, there is no reason to use a
// DataGroup<T>; one should stick to TheoryData<T>.
//
// The same with TheoryData<T> would require an array access and boxing:
// > public TheoryData<T> WhereT(Func<T, bool> predicate)
// > {
// >     var q = from item in this
// >             let value = (T)item[0]
// >             where predicate(value)
// >             select value;
// >     Debug.Assert(q.Any());
// >     return DataGroup.Create(q);
// > }
//
// See https://github.com/xunit/xunit/blob/main/src/xunit.v3.core/TheoryData.cs

/// <summary>
/// Provides factory methods for <see cref="DataGroup{T}"/>.
/// </summary>
public static class DataGroup
{
    [Pure]
    public static DataGroup<T> Create<T>(IEnumerable<T> source, bool deferred) => new(source, deferred);

    //
    // CreateDaysSinceEpochInfoData()
    //

    [Pure]
    public static DataGroup<DaysSinceEpochInfo> CreateDaysSinceEpochInfoData<T>(
        IEnumerable<T> source, DayNumber epoch)
        where T : IConvertibleToDayNumberInfo
    {
        var q = from x in source select x.ToDayNumberInfo() - epoch;
        return Create(q, deferred: false);
    }

    //
    // CreateDayNumberInfoData()
    //

    [Pure]
    public static DataGroup<DayNumberInfo> CreateDayNumberInfoData(
        IEnumerable<DaysSinceEpochInfo> source, DayNumber epoch)
    {
        var q = from x in source select x.ToDayNumberInfo(epoch);
        return Create(q, deferred: true);
    }

    [Pure]
    public static DataGroup<DayNumberInfo> CreateDayNumberInfoData<T>(
        IEnumerable<T> source)
        where T : IConvertibleToDayNumberInfo
    {
        var q = from x in source select x.ToDayNumberInfo();
        return Create(q, deferred: true);
    }

    [Pure]
    public static DataGroup<DayNumberInfo> CreateDayNumberInfoData<T>(
        IEnumerable<T> source, DayNumber sourceEpoch, DayNumber resultEpoch)
        where T : IConvertibleToDayNumberInfo
    {
        int shift = resultEpoch - sourceEpoch;
        var q = from x in source select x.ToDayNumberInfo() + shift;
        return Create(q, deferred: true);
    }
}

/// <summary>
/// Represents a group of data for a theory.
/// </summary>
public sealed class DataGroup<T> : IReadOnlyCollection<object?[]>
{
    private readonly IContainer _container;

    public DataGroup()
    {
        _container = IContainer.Create();
    }

    public DataGroup(IEnumerable<T> values, bool deferred)
    {
        _container = IContainer.Create(values, deferred);
    }

    public bool IsDeferred => _container.IsDeferred;

    // Collection initializer.
    public void Add(T v) => _container.Add(v);

    public DataGroup<TResult> SelectT<TResult>(Func<T, TResult> selector)
    {
        var q = _container.Values.Select(selector);
        return DataGroup.Create(q, deferred: true);
    }

    public DataGroup<T> WhereT(Func<T, bool> predicate)
    {
        var q = _container.Values.Where(predicate);

        Debug.Assert(q.Any());

        return DataGroup.Create(q, deferred: true);
    }

    public XunitData<T> ToXunitData()
    {
        var q = _container.ToEnumerable();
        return new(q);
    }

    //
    // IReadOnlyCollection
    //

    public int Count => _container.Count();

    public IEnumerator<object?[]> GetEnumerator()
    {
        var q = _container.ToEnumerable();
        return q.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #region Store

    private interface IContainer
    {
        bool IsDeferred { get; }

        IEnumerable<T> Values { get; }

        void Add(T v);

        int Count();

        public IEnumerable<object?[]> ToEnumerable() => Values.Select(v => new object?[] { v });

        public static IContainer Create() => new Container();

        public static IContainer Create(IEnumerable<T> values, bool deferred) =>
            deferred ? new DeferredContainer(values) : new Container(values);
    }

    private sealed class Container : IContainer
    {
        private readonly List<T> _values;

        public Container()
        {
            _values = new List<T>();
        }

        public Container(IEnumerable<T> values)
        {
            _values = new List<T>(values);
        }

        public bool IsDeferred => false;

        public IEnumerable<T> Values => _values;

        public int Count() => _values.Count;

        public void Add(T v) => _values.Add(v);
    }

    private sealed class DeferredContainer : IContainer
    {
        public DeferredContainer(IEnumerable<T> values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public bool IsDeferred => true;

        public IEnumerable<T> Values { get; }

        public int Count() => Values.Count();

        public void Add(T v) => throw new NotSupportedException();
    }

    #endregion
}
