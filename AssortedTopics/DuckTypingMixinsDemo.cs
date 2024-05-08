using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssortedTopics
{
    interface IScalar<T> : IEnumerable<T>
    {
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield return (T)this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MyClass1: IScalar<MyClass1>
    {
        public override string ToString()
        {
            return "MyClass";
        }
    }

    internal class DuckTypingMixinsDemo
    {
        public static void Demo(string[] args)
        {
            // duck typing 

            // GetEnumerator() -- foreach (IEnumerable<T>)\
            // Dispose() -- using (IDisposable) 
            

            // mixin

            var mc = new MyClass1();
            foreach (var x in mc)
            {
                Console.WriteLine(x);
            }
        }
    }
}
