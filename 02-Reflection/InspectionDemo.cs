using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _02_Reflection
{
    internal class InspectionDemo
    {
        public static void Demo(string[] args)
        {
            var t = typeof(Guid); 
            Console.WriteLine(t.FullName);
            Console.WriteLine(t.Name);
            var ctors = t.GetConstructors();
            Console.WriteLine(ctors);

            foreach (var ctor in ctors)
            {
                Console.WriteLine(" - Guid(");
                var pars = ctor.GetParameters();
                for (int i = 0; i < pars.Length; i++)
                {
                    var par = pars[i];
                    Console.Write($"{par.ParameterType} {par.Name}");
                    if (i+1 != pars.Length)
                    {
                        Console.Write(", ");
                    }
                }

                Console.WriteLine(")");
            }

            var methods = t.GetMethods();
            foreach (var methodInfo in methods)
            {
                Console.WriteLine(methodInfo.Name);
            }

            Console.WriteLine(t.GetProperties());
            Console.WriteLine(t.GetEvents());
        }
    }
}
