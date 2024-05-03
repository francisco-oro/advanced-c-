using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
        where TResult : class
        where TInput : class
        {
            if (o == null) return null;
            else return evaluator(o);
        }

        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }


        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator,
            TResult failureValue)
        where TInput : class
        {
            if(o == null) return failureValue;
            return evaluator(o);
        }

        public static TResult WithValue<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
        where TInput : struct
        {
            return evaluator(o);
        }
    }

    public partial class Person
    {
        public Address Address { get; set; }
    }

    public class Address
    {
        public string PostCode { get; set; }
    }

    internal class MaybeMonadDemo
    {
        public void MyMethod(Person p)
        {
            string postcode;
            if (p != null)
            {
                if (HasMedicalRecord(p) && p.Address != null)
                {
                    CheckAddress(p.Address);
                    if (p.Address.PostCode != null)
                    {
                        postcode = p.Address.PostCode.ToString();
                    }
                    else
                    {
                        postcode = "UNKNOWN";
                    }
                }
            }

            postcode = p.With(x => x.Address).With(x => x.PostCode);

            postcode = p
                .If(HasMedicalRecord)
                .With(x => x.Address)
                .Do(CheckAddress)
                .Return(x => x.PostCode, "UNKNOWN");
        }

        private void CheckAddress(Address address)
        {
            Console.WriteLine("CheckAddress method");
        }

        private bool HasMedicalRecord(Person person)
        {
            Console.WriteLine("HasMedicalRecord method");
            return true;
        }

        static void Main(string[] args)
        {
            MaybeMonadDemo demo = new MaybeMonadDemo();
            demo.MyMethod(new Person() { Address = new Address()
            {
                PostCode = "513612"
            },
                Age = 29, 
                Name = "Guest"
            });
        }
    }
}
