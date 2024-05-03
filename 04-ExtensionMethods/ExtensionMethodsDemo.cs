using System.Runtime.Serialization;
using System.Diagnostics;

namespace ExtensionMethods
{
    public class Foo
    {
        public virtual string Name => "Foo";
    }

    public class FooDerived : Foo
    {
        public override string Name => "FooDerived";
    }


    public partial class Person
    {
        public string Name;
        public int Age;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }


    public static class FooExtensionMethods
    {
        public static int Measure(this Foo foo)
        {
            return foo.Name.Length;
        }
        public static int Measure(this FooDerived foo)
        {
            return 42;
        }
        public static string ToBinary(this int n)
        {
            return Convert.ToString(n, 2);
        }

        public static void Save(this ISerializable serializable)
        {
            // ...
        }

        // object
        public static string ToString(this Foo foo)
        {
            return "test";
        }

        public static Person ToPerson(this (string name, int age) data)
        {
            return new Person() { Name = data.name, Age = data.age }; 
        }

        public static int Measure<T, U>(this Tuple<T, U> t)
        {
            return t.Item2!.ToString()!.Length;
        }

        public static Stopwatch Measure(this Func<int> f)
        {
            var st = new Stopwatch();
            st.Start();
            f();
            st.Stop();
            return st;
        }

        //
        public static void DeepCopy<T>(this T obj)
        where T: ISerializable, new()
        {

        }
    }

    public class ExtensionMethodsDemo
    {
        public static void Demo(string[] args)
        {
            Func<int> calculate = delegate
            {
                Thread.Sleep(1000);
                return 42;
            }; 

            var st = calculate.Measure();
            Console.WriteLine($"Took {st.ElapsedMilliseconds}msec");
        }
    }
};
