/*
 * BitSkinsApi
 * Copyright (C) 2019 dmitrydnl
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

namespace BitSkinsApi.CheckParameters
{
    internal static class Checking
    {
        internal static void UptoInt(int number, string numberName, int upto)
        {
            if (number > upto)
            {
                throw new ArgumentException($"{numberName} must be upto {upto}.");
            }
        }

        internal static void PositiveInt(int number, string numberName)
        {
            if (number < 1)
            {
                throw new ArgumentException($"{numberName} must be positive number.");
            }
        }

        internal static void OverThanDouble(double number, string numberName, double overThan)
        {
            if (number < overThan)
            {
                throw new ArgumentException($"{numberName} must be over {overThan}.");
            }
        }

        internal static void PositiveDouble(double number, string numberName)
        {
            if (number <= 0)
            {
                throw new ArgumentException($"{numberName} must be positive number.");
            }
        }

        internal static void NotEmptyString(string str, string strName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{strName} must be not empty.");
            }
        }

        internal static void NotEmptyList<T>(List<T> list, string listName)
        {
            if (list == null)
            {
                throw new ArgumentNullException(listName, $"{listName} must be not null.");
            }
            if (list.Count < 1)
            {
                throw new ArgumentException($"In {listName} count must be at least one.");
            }
        }

        internal static void CountUptoList<T>(List<T> list, string listName, int countUpto)
        {
            if (list.Count > countUpto)
            {
                throw new ArgumentException($"In {listName} count must be upto {countUpto}.");
            }
        }
    }
}
