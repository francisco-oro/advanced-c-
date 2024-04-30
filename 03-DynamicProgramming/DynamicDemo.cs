using Microsoft.CSharp.RuntimeBinder;

namespace DynamicProgramming
{
    public class Demo
    {
        public static void Test(string[] args)
        {
            // late binding
            dynamic d = "hello";

            Console.WriteLine(d.GetType());
            Console.WriteLine(d.Length);

            d += " world"; 
            Console.WriteLine(d.ToString());

            try
            {
                int n = d.Area;
            }
            catch (RuntimeBinderException e)
            {
                Console.WriteLine(e.Message);
            }

            d = 321; 
            Console.WriteLine(d.GetType());
            d--;
            Console.WriteLine(d.ToString());
        }
    }
}