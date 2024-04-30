using System;

namespace _02_Reflection
{
    public class RepeatAttribute : Attribute
    {
        public int Times { get; }

        public RepeatAttribute(int times)
        {
            Times = times;
        }


    }


    public class AttributeDemo
    {
        [Repeat(3)]
        public void SomeMethod()
        {

        }

        public static void Main(string[] args)
        {
            var sm = typeof(AttributeDemo).GetMethod("SomeMethod");
            foreach (var att in sm.GetCustomAttributes(true)) 
            {
                Console.WriteLine("Found an attribute: " + att.GetType());
                if (att is RepeatAttribute ra)
                {
                    Console.WriteLine($"Need to repeat {ra.Times} times");
                }
            }
        }
    }
}
