using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    /// <summary>
    /// Maybe monad class to define extension methods
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Checks for presence or absence of something in the input object. 
        /// </summary>
        /// <typeparam name="TInput">Input type (must be a class)</typeparam>
        /// <typeparam name="TResult">Result type (must be a class)</typeparam>
        /// <param name="o">Input object</param>
        /// <param name="evaluator">Evaluator function that returns a value of type TResult</param>
        /// <returns>The property of type TResult  if it is found within the object or null if it's not found
        /// </returns>
        public static TResult? With<TInput, TResult>(this TInput? o, Func<TInput, TResult> evaluator)
        where TResult : class
        where TInput : class
        {
            if (o == null) return null;
            else return evaluator(o);
        }

        /// <summary>
        /// Checks if the input object satisfies an evaluator function
        /// </summary>
        /// <typeparam name="TInput">Input type (must be a class)</typeparam>
        /// <param name="o">Input object</param>
        /// <param name="evaluator">Evaluator function that returns a boolean</param>
        /// <returns>The input object if the evaluator returns true or null if the evaluator returns false</returns>
        public static TInput? If<TInput>(this TInput? o, Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }

        /// <summary>
        /// Performs an action that accepts the input object as argument
        /// </summary>
        /// <typeparam name="TInput">Input type (must be a class)</typeparam>
        /// <param name="o">Input object</param>
        /// <param name="action">An action to perform. It must accept the Input object as parameter</param>
        /// <returns>The input object if the action is performed successfully or null if there's no input object provided as type parameter</returns>
        public static TInput? Do<TInput>(this TInput? o, Action<TInput> action)
            where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        /// <summary>
        /// Returns the result of an evaluation provided the input object is not in a null state or a fallback value otherwise.
        /// </summary>
        /// <typeparam name="TInput">Input type (must be a class)</typeparam>
        /// <typeparam name="TResult">Return type</typeparam>
        /// <param name="o">Input object</param>
        /// <param name="evaluator">Evaluator function that returns an instance of TResult</param>
        /// <param name="failureValue">Fallback value to be returned if the input object is null</param>
        /// <returns>An instance of TResult if the input object is not null or failureValue if the input object is null</returns>
        public static TResult Return<TInput, TResult>(this TInput? o, Func<TInput, TResult> evaluator,
            TResult failureValue)
        where TInput : class
        {
            if(o == null) return failureValue;
            return evaluator(o);
        }

        /// <summary>
        /// Checks for presence or absence of a value type
        /// </summary>
        /// <typeparam name="TInput">Input type (must be a struct)</typeparam>
        /// <typeparam name="TResult">Return type</typeparam>
        /// <param name="o">Input value</param>
        /// <param name="evaluator">Evaluator function that returns an instance of TResult</param>
        /// <returns>The result of type TResult of the evaluator</returns>
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
