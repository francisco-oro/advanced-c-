﻿namespace _02_Reflection
{
    public class ConstructionDemo
    {
        public static void Main(string[] args)
        {
            var t = typeof(bool );
            var b = Activator.CreateInstance( t );
            Console.WriteLine(b);
            var b2 = Activator.CreateInstance<bool>();
            Console.WriteLine(b2);
            var wc = Activator.CreateInstance("System", "System.Timers.Timer");
            Console.WriteLine(wc);
            Console.WriteLine(wc.Unwrap());

            var alType = Type.GetType("System.Collections.ArrayList");
            Console.WriteLine(alType);

            var alCtor = alType.GetConstructor(Array.Empty<Type>());
            Console.WriteLine(alCtor);
            var al = alCtor.Invoke(Array.Empty<object>());
            Console.WriteLine(al);
            var st = typeof(string);
            var ctor = st.GetConstructor(new[] { typeof(char[]) });
            Console.WriteLine(ctor);
            var str = ctor.Invoke(new object?[]
            {
                new[] { 't', 'e', 's', 't' }
            });
            Console.WriteLine(str);
            var listType = Type.GetType("System.Collections.Generic.List`1");
            Console.WriteLine(listType);
        }
    }
}

