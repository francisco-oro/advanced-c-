# Integral Types and Limits
![integral_types_and_limits](assets/integral_types_and_limits.png)

## Integer specifics
- Each data type has a minimum and maximum value
	- E.g., `int.MinValue`, `long.MaxValue`
- Conversion functions from strings
	- `int value = int.Parse("123"); ` throws on invalid string
	- `bool success = int.TryParse("123", out int value)` returns false if failed
	- These are often hand-optimized in performance-critical scenarios
- default(T) returns 0 for all types 

## Overflow
- A situation where you need more bits than are available is called an `overflow`
- Example: adding 1 to the largest number, or subtracting 1 from the smallest
- Compile-time error:
	- var x = int.MinValue - 1;
	- eror CS0220: The operation overflows at compile time in checked mode
Run-time behavior:
	- Not handled by default, you get 'garbage' value 

## Division by Zero 
- Compile-time error
	- `var x = 1/0;`
	- error CS0020: Division by constant zero
- Run-time exception
	- var zero = 0; 
	- var x = 1/zero;
	- `System.DivideByZeroException`: Attempted to divide by zero. 
- Division by zero with integers is a problem
	- No bit state to represent a 'special value'
	- Thus, division by zero is simply not allowed anywhere

## Checked and Unchecked 
- Checked mode = please be sure to check this for me
- Unchecked mode = please don't check this, I take responsability 
- These scopes only affect overflow; they do not affect division by zero 
- Division by zero will always throw

## Overflow and Checked 
- By default, overflow is disallowed at compile-time (if found) and is totally allowed at runtime
- We can reverse both of these situations
- Allow obvious overflow at compile-time:

## Detect Overflow Everywhere

![detec](assets/detec.png)

- Project Settings => Build => Advanced... => Check for arithmetic overflow/underflow
- This will throw on all overflow situations
- Can wrap in ordinary try-catch (no checked context needed) to catch `OverflowException`


# BigInteger
- An integer of arbitrary size
- Lives in System.Runtime.Numerics 
- Similarities compared to integer value types: 
	- Also throws a `DivideByZeroException`
- Differences: 
	- Is a struct
	- Cannot overflow 
	- Is immutable
		- x++ does not modify underlying object, instead
		- creates new object that replaces the original one
		- In other words, BigInteger behaves just as string does
		- Performance implications!

# Floating-Point Values
- Only two data types 
- Both are signed types standardized under IEEE754
- float (System.Single)
	- 32 bits (single-precision floating-point)
	- -3.40282347E+308 to 1.7976931348623157E+308

## Special States 
- FP calculations do not cause exceptions 
- Division by zero is allowed!
	- Does not cause an exception
- Floating-point types define the idea of infinity 
	- 1.0 / 0.0 gives infinity (double.PositiveInfinity) 
	- -1.0f / 0.0 gives -infinity (double.PositiveInfinity)
- Arithmetic operations on infinity give infinity 
	- `double.PositiveInfinity + 1 == double.PositiveInfinity `
	- But you can flip the sign 
		- `1.0 * double.NegativeInfinity == double.PositiveInfinity*`
		- 
- But there is a special case.. 

## Not a Number (NaN)
- 0.0/0.0 and infinity/infinity gives the value of `NaN` (not a number)
- `NaN` is special in that
	- It has no sign
	- It is viral (any operation on NaN yields a NaN)
	- Comparison with `NaN` always fails: `double.NaN == (1.0/0.0) // false`
- Check for a `NaN` using `double.IsNaN(x)`
- It is possible to force .NET to throw exception on `NaN`: https://bit.ly/2BuWRVz

## Representation Problems
- Every integer within some range can be represented as a sum of powers of 2
- Not every rational number can be represented this way 
- 0.1 results in an infinite binary sequence
	- 0.0001100110011001100110011001100110011001100...
	- But if you `ToString()` it, you'll get "0.1"
- Thus, some numbers are not exactly what you intend them to be 
	- They can be slightly smaller or greater 
- This can lead to surprising results
	- `double d = 0.1 + 0.2; // 0.30000000000000004`
	- `(0.1 + 0.2 == 0.3) // false`
- Comparisons need to use a tolerance value 
	- `if (Math.Abs(x - y) < 1e-8) { ... }` 
	- In real world code (esp. iterative), tolerance values are hard to find

# System.Decimal 
- 128-bit decimal type 
- Range from - to +9,228,162,514,264,337,593,543,950,335
- Suitable for financial calculations 
- Not subject to FP representation errors
	- In other words, `0.1m + 0.2m == 0.3m` 
- Significantly slower than FP calculations 
- Not standardized/supported on other systems (e.g., GPU)

	


```c#
unchecked { 
	var n = int.MinValue - 1;
}
```
- This will compile just fine
- Detect overflow at runtime 
```c#
checked {
	try {
		int x2 = int.MinValue - 1;
	} catch(OverflowException e ){
		Console.WriteLine(e.Message);
	}

}
```


## Detect Overflow Everywhere 
d

- There is no `auto-vectorization` free lunch. 
- Use intrinsics for low-level control
	- Easy if you control hardware
	- Very tedious if you don't know what CPU you'll be running on 
- Use SIMD-enabled types 
	- `Vector<T>` adapts to architecture
	- Supports non-accelerated execution
- You can combine SIMD and multithreading
- GPGPU is likely to trump CPU SIMD calculations on non-divergent streamable loads 
	- But GPUs are not programmable in C# 
	- Very difficult to write GPU-neutral code 
You can combine SIMD/multicore with GPGPU for more power

## Vector<T> Scenario 
- I want to add two arrays of bytes
- Create and initialize arrays 
```c#
byte[] array1, array2, result;
```
- Determine how many fit into a `Vector<byte>`
- Loop by register size
- Use vector as a view into an array 

```c#
byte[] array1 = Enumerable.Range(1, 128).Select(x => (byte) x).ToArray()
byte[] array2 = Enumberable.Range(4, 128).Select(x => (byte) x).ToArray()
byte[] result = new byte[128];

int size = vector<byte>.Count;

for(int i = 0; i < array1.Length; i += size)
{
	var va = new vector<bytes>(array1, i);
	var vb = new vector<bytes>(array2, i);
	var vbresult = va + vb;
	vresult.CopyTo(result, i);
}

```