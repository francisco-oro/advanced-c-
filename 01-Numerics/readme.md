# Numerics Section Cheat Sheet

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