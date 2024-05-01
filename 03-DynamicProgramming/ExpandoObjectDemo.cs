using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming
{
    internal class ExpandoObjectDemo
    {
        public static void Demo(string[] args)
        {
            dynamic person = new ExpandoObject();
            person.FirstName = "John";
            person.Age = 22;
            Console.WriteLine($"{person.FirstName} is {person.Age} years old");

            person.Address = new ExpandoObject();
            person.Address.City = "London";
            person.Address.Country = "UK";

            person.SayHello = new Action(() =>
            {
                Console.WriteLine("Hello!");
            });
            person.SayHello();

            person.FallsIll = null;

            person.FallsIll += new EventHandler<dynamic>((sender, args) =>
            {
                Console.WriteLine($"We need a doctor for {args}");
            });

            EventHandler<dynamic> e = person.FallsIll;
            e?.Invoke(person, person.FirstName);

            var dict = (IDictionary<string, object>)person;
            Console.WriteLine(dict.ContainsKey("FirstName"));
            Console.WriteLine(dict.ContainsKey("LastName"));
            dict["LastName"] = "Smith";
            Console.WriteLine(person.LastName);
        }
    }
}
