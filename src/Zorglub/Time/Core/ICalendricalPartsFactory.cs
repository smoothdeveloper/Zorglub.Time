﻿// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2020 Narvalo.Org. All rights reserved.

namespace Zorglub.Time.Core
{
    #region Developer Notes

    // Types Implementing ICalendricalSchemaPlus
    // -----------------------------------------
    //
    // ICalendricalSchema
    // └─ ICalendricalSchemaPlus
    //    ├─ SystemSchema
    //    ├─ CalendricalSchemaPlusChecked
    //    └─ CalendricalSchemaPlusUnchecked
    //
    // Comments
    // --------
    // The methods here are not part of ICalendricalSchema because the schema
    // may support a range of years larger than the one supported by
    // Yemoda/Yedoy.
    //
    // Unchecked constructions of Yemoda/Yedoy objects being only visible to
    // "friendly" assemblies, implementing this interface outside this assembly
    // can be unefficient. Solution? For instance, when creating a new type of
    // calendar or date type, instead of ICalendricalSchemaPlus.GetDateParts(),
    // use ICalendricalSchema.GetDateParts(), then validate the result and
    // create the parts in one step using PartsFactory.

    #endregion

    /// <summary>
    /// Provides methods you can use to create new calendrical parts.
    /// </summary>
    public partial interface ICalendricalPartsFactory { }

    public partial interface ICalendricalPartsFactory // Conversions
    {
        /// <summary>
        /// Obtains the date parts for the specified day count (the number of consecutive days from
        /// the epoch to a date).
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yemoda GetDateParts(int daysSinceEpoch);

        /// <summary>
        /// Obtains the ordinal date parts for the specified day count (the number of consecutive
        /// days from the epoch to a date).
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yedoy GetOrdinalParts(int daysSinceEpoch);

        /// <summary>
        /// Obtains the ordinal date parts for the specified date.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yedoy GetOrdinalParts(int y, int m, int d);

        /// <summary>
        /// Obtains the date parts for the specified ordinal date.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yemoda GetDateParts(int y, int doy);
    }

    public partial interface ICalendricalPartsFactory // Dates in a given year or month
    {
        /// <summary>
        /// Obtains the date parts for the first day of the specified year.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yemoda GetStartOfYearParts(int y);

        /// <summary>
        /// Obtains the ordinal date parts for the first day of the specified year.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yedoy GetStartOfYearOrdinalParts(int y);

        /// <summary>
        /// Obtains the date parts for the last day of the specified year.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yemoda GetEndOfYearParts(int y);

        /// <summary>
        /// Obtains the ordinal date parts for the last day of the specified year.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yedoy GetEndOfYearOrdinalParts(int y);

        /// <summary>
        /// Obtains the date parts for the first day of the specified month.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yemoda GetStartOfMonthParts(int y, int m);

        /// <summary>
        /// Obtains the ordinal date parts for the first day of the specified month.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yedoy GetStartOfMonthOrdinalParts(int y, int m);

        /// <summary>
        /// Obtains the date parts for the last day of the specified month.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yemoda GetEndOfMonthParts(int y, int m);

        /// <summary>
        /// Obtains the ordinal date parts for the last day of the specified month.
        /// </summary>
        /// <exception cref="AoorException">The result is not representable by the system.</exception>
        [Pure] Yedoy GetEndOfMonthOrdinalParts(int y, int m);
    }
}
