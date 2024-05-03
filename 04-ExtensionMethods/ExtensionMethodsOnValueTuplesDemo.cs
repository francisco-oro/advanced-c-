using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    internal class ExtensionMethodsOnValueTuplesDemo
    {
        public static void Demo(string[] args)
        {
            // 2 5 2001
            //new DateTime(2001, 5, 2);
            var when = (2, 5, 2001).dmy();
            Console.WriteLine(when);

            var list1 = new List<int>{1, 2, 3};
            var list2 = new List<int>{3, 2, 1};
            var merged = (list1, list2).merge();
            Console.WriteLine(merged);
        }

    }

    static partial class ExtensionMethods
    {
        public static DateTime dmy(this (int day, int month, int year) when)
        {
            return new(when.year, when.month, when.day); 
        }

        public static List<T> merge<T>(this (IList<T> first, IList<T> second) lists)
        {
            var result = new List<T>();
            result.AddRange(lists.first);
            result.AddRange(lists.second);
            return result; 
        }
    }
}
