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
