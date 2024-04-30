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