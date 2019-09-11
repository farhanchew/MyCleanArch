using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CleanArch.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static Dictionary<int, string> ToDict(this Enum theEnum)
        {
            var enumDict = new Dictionary<int, string>();
            foreach (int enumValue in Enum.GetValues(theEnum.GetType()))
            {
                enumDict.Add(enumValue, enumValue.ToString());
            }

            return enumDict;
        }

        public static string GetDescriptionSplit(this Enum value)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var s = (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            return r.Replace(s, " ");
        }
    }
}
