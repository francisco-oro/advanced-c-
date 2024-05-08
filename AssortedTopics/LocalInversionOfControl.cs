using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssortedTopics
{
    public static class ExtensionMethods
    {
        public static T AddTo<T>(this T self, params ICollection<T>[] colls)
        {
            foreach (ICollection<T> coll in colls)
            {
                coll.Add(self);
            }
            return self;
        }

        public static bool IsOneOf<T>(this T self, params T[] values)
        {
            return values.Contains(self);
        }

        public static bool HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return !props(self).Any();
        }

        public static bool HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return !props(self).Any();
        }

    }

    public class Person
    {
        public List<string> Names = new List<string>();
        public List<Person> ChildrenList = new List<Person>();
    }
    public class MyClass2
    {
        public void AddingNumbers()
        {
            var list = new List<int>();
            var list2 = new List<int>();
            24.AddTo(list).AddTo(list2);
        }

        public void ProcessCommand(string opcode)
        {
            //if (opcode == "AND" || opcode == "OR" || opcode == "XOR")
            //if (new[] {"AND", "OR", "XOR"}.Contains(opcode))
            //if ("AND OR XOR".Split(' ').Contains(opcode)) {}
            if (opcode.IsOneOf("AND", "OR", "XOR"))
            {
            }

        }

        public void Process(Person person)
        {
            //if (!person.Names.Any()){}
            if(person.HasNo(p => p.Names)){}
        }
    }
    internal class LocalInversionOfControl
    {
    }
}
