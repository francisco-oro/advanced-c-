using System;

namespace _02_Reflection
{
    public class SystemTypeDemo
    {
        public static void Demo(string[] args)
        {
            Type t = typeof(int);
            Console.WriteLine(t.GetMethods());
            Type t2 = "hello".GetType();
            Console.WriteLine(t2.FullName);
            Console.WriteLine(t2.GetFields());
            Console.WriteLine(t2.GetMethods());

            var a = typeof(string).Assembly;
            Console.WriteLine(a);
            var types = a.GetTypes();
            Console.WriteLine(types[10]);
            Console.WriteLine(types[10].FullName);
            Console.WriteLine(types[10].GetMethods());
            var t3 = Type.GetType("System.Int64"); 
            Console.WriteLine(t3.FullName);
            Console.WriteLine(t3.GetMethods());
            var t4 = Type.GetType("System.Collections.Generic.List`1");
            Console.WriteLine(t4.FullName);
            Console.WriteLine(t4.GetMethods());
        }
    }
}
