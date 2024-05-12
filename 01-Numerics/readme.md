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

# Vector Types 

- The CPu has registers larger than 64 bit
	- 128,256 or even 512 bit - depends on the CPU 
- Those registers are not for data types with wider range
- Instead, they are used for packed values 
- A 128-bit register can pack
	- 16 bytes 
	- 4 int values 
	- 4 float values 
	- 2 double values 

## Motivation
- Processor instructions (e.g. add) have a cost
	- Number of clock cycles 
- Add < multiply < divide < square root 
- Exact costs vary by CPU and data type (float vs. double)
- A lot of effort is spent on optimization
	- `ax^2 + bx => x(ax + b)` 
- Operations need to be performed on sets of values 
	- E.g., add two arrays 
- Divide & conquer 
- SIMD let us accelerate these calculations 
	- In the context of a single CPU Core 

## Vector Types 


## Registers II 
- SIMD = Single Instruction Multiple Data 
- Modern CPUs provides several large registers for SIMD 
- Registers vary in size (128, 256, 512 bits)
	- Larger register = more data that can be packed 
- Need to be supported by the CPU
	- Implies compatibility issues  
- You are unlikely to achieve full equivalence between ordinary and vectorized FP calculations 


## Registers III 
- SIMD technologies
	- AMD: 3DNow!
	- Intel: MMX, SSE 
	- Both: AVX 
- Streaming SIMD (Extensions SSE)
	- 128-bit registers (xmm0...xmm7)
- Advanced Vector Extensions (AVX)
	- New instructions supported by AMD/Intel (since ~ 2011)
	- AVX extends xmm to ymm (bits 128...255)
	- AVX-512 futher adds zmm (bits 256...511)

# SIMD Intrinsics 
## Instructinos 
- SIMD is supported with special instructions 
- Integral & floating0point operations 
- Scalar vs. packed data 
	- addss adds just the low single-precision value 
	- addps adds all four single-precision values 
- How to use in .NET?
	- Use hardware intrinsics (`System.Runtime.Intrinsics`)
	- Use specific data types
	- Use general `Vector<T>`

## Instrinsics
- Intrinsics are simple wrappers around SIMD types and operations 
- `System.Runtime.Intrinsics` namespace
- You can check the level of SIMD support using
	- SSE classes (Sse, Sse2, SSe3, ...Ssse3 [not a typo!], Sse42)
	- AVX classes (Avx, Avx2)
- E.g., `if(Avx.IsSupported) { ... }`
- If we do have support, then: 
	- You can use static members (e.g. Avx2.Add()) 
	- Those members operate on VectorXxx<T> types

## Intrinsic Vector Types 
- You can create a 64/128/256/...-bit vector using VectorXxx.Create()
- `Var x = Vector128.Create(1.0f);`
	- Creates an object of type `Vector128<float>`

	



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