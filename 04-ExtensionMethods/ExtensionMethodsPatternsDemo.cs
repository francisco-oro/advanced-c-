using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ExtensionMethods
{
    public static partial class ExtensionMethods
    {
        public static StringBuilder al(this StringBuilder sb, string text)
        {
            return sb.Append(text);
        }

        //AppendLine()
        //AppendFormat()
        public static StringBuilder AppendFormatLine(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendFormat(format, args).AppendLine();
        }

        public static ulong Xor(this IList<ulong> values)
        {
            ulong first = values[0];
            foreach (var x in values.Skip(1))
            {
                first ^= x;
            }
            return first;
        }

        public static void AddRange<T>(this IList<T> list, params T[] objects)
        {
            list.AddRange(objects);
        }

        public static string f(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static DateTime June(this int day, int year)
        {
            return new DateTime(year, 6, day);
        }
    }
    internal class ExtensionMethodsPatternsDemo
    {
        public static void Demo(string[] args)
        {
            //string.Format("foo {0}", 23);
            var notToday = 10.June(2020);
        }
    }
}
