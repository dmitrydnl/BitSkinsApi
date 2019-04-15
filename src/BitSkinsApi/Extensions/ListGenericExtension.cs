/*
 * BitSkinsApi
 * Copyright (C) 2019 Dmitry
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

namespace BitSkinsApi.Extensions
{
    static class ListGenericExtension
    {
        internal static string ToStringWithDelimiter(this List<string> list, string delimiter)
        {
            return String.Join(delimiter, list);
        }

        internal static string ToStringWithDelimiter(this List<double> list, string delimiter)
        {
            return String.Join(delimiter, list.ConvertAll(x => x.ToString(System.Globalization.CultureInfo.InvariantCulture)));
        }
    }
}
