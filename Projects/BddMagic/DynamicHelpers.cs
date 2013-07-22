using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BddMagic
{
    /// <summary>
    /// Collection of methods to get values from C# dynamic value.
    /// </summary>
    /// <remarks>
    /// These methods are mostly useful for VB.NET programmers. VB.NET does not have dynamic
    /// keyword and without these helpers Option Strict Off would be required to access values
    /// from the step parameters.
    /// </remarks>
    public static class DynamicHelpers
    {
        public static DateTime GetDateTime(dynamic obj, int index)
        {
            return Convert.ToDateTime(obj[index]);
        }

        public static int GetDouble(dynamic obj, int index)
        {
            return Convert.ToDouble(obj[index]);
        }

        public static int GetInt(dynamic obj, int index)
        {
            return Convert.ToInt32(obj[index]);
        }

        public static string GetString(dynamic obj, int index)
        {
            return obj[index].ToString();
        }

        public static object GetValue(dynamic obj, int index)
        {
            return (object[])obj(index);
        }

    }
}
