//var s = "abracadabra";
//var t = typeof(string);
//Console.WriteLine(t);

//// Parameterless invocations
//var trimMethod = t.GetMethod("Trim", Array.Empty<Type>());
//Console.WriteLine(trimMethod);

//var result = trimMethod?.Invoke(s, Array.Empty<object>());
//Console.WriteLine(result);

//// TryDo invocations
//var numberString = "123";
//var parseMethod = typeof(int).GetMethod("TryParse",
//    new[] { typeof(string), typeof(int).MakeByRefType() });
//Console.WriteLine(parseMethod);

//object[] objects = { numberString, null };
//var ok = parseMethod?.Invoke(null, objects);
//Console.WriteLine(ok);
//Console.WriteLine(objects[1]);

////Generic invocations
//var at = typeof(Activator);
//var method = at.GetMethod("CreateInstance", Array.Empty<Type>());
//Console.WriteLine(method);
//var ciGeneric = method?.MakeGenericMethod(typeof(Guid));
//Console.WriteLine(ciGeneric);
//var guid = ciGeneric?.Invoke(null, null);
//Console.WriteLine(guid);