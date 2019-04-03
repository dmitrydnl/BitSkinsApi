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
