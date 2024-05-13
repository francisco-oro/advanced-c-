namespace _02_Reflection
{
    public class InvocationDemo
    {
        public static void Demo(string[] args)
        {
            var s = "abracadabra";
            var t = typeof(string);
            Console.WriteLine(t);

            var trimMethod = t.GetMethod("Trim", Array.Empty<Type>());
            Console.WriteLine(trimMethod);
            var result = trimMethod?.Invoke(s, Array.Empty<object>());
            Console.WriteLine(result);

            // bool int.TryParse(str, out int n)
            var numberString = "123";
            var parseMethod = typeof(int).GetMethod("TryParse", new []{ typeof(string), typeof(int).MakeByRefType() });
            Console.WriteLine(parseMethod);

            object[] args2 = { numberString, null };
            var ok = parseMethod.Invoke(null, args2);
            Console.WriteLine(ok);
            Console.WriteLine(args2[1]);

            // Activator.CreateInstance
            var at = typeof(Activator);
            var method = at.GetMethod("CreateInstance", Array.Empty<Type>());
            Console.WriteLine(method);

            var ciGeneric = method.MakeGenericMethod(typeof(Guid));
            Console.WriteLine(ciGeneric);
            var guid = ciGeneric.Invoke(null, null);
        }
    }

}

