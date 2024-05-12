using System;

namespace _02_Reflection
{
    public class SystemTypeDemo
    {
        public static void Main(string[] args)
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
        }
    }
}
