# Overview
- In parameters
- Ref readonly parameters



## Types of Memory
- Managed memory(`new` operator)
	- Small objects <85k (generational part of managed heap)
	- Large objects >85k (Large Object Heap< LOH)
	- Both released by GC

- Unmanaged memory 
	- Allocated on unmanaged heap with Marshal.AllocHGlobal/CoTaskMem
	- Released with Marshal.FreeHGlobal/CoTaskMem
	- GC not involved 

- Stack memory (`stackalloc` keyword)
	- Very fast allocation/dellocation
	- Very small (typically <1Mb), overallocate and you get SO/processes dies
	- Nobody uses it :)

## The problem
- Imagine tou want to refer to a part of the string 
	- ... without making a copy of the substring

- You could refer to the start/end indices or use pointers 
- More generally, you could refer to
	- Location where a memory range begins
	- Location/index where you need to start taking the values
	- How many values to take/index of final element

- We need a general way of referring to a range of values in memory (for ereading, copying, etc.)
- That generalization is expressed as `Span<T>`

## Ref Struct Type
- A value type that must be stack-allocated 
- Can never be created on the heap as member of another class
- Main motivation to support `Span<T>`
- Compiler-enforced rules
	- Cannot box a ref struct (i.e. cannot assign to object, dynamic or an interface type)
	- Cannot declare a ref struct as a member of class or normal struct
	- Cannot declare local ref struct variables in async methods or synchronous methods that return Task or Task-like types
	- Cannot declare ref struct locals in iterators
	- Cannot capture ref struct vars in lambda expressions or local functions
- Rules prevent a ref struct from being promoted to the managed heap 
- *Waning:* in .NET Framework, current `Span<T>` in `System.Memory` NuGet is not a ref struct, therefore all of those limitations have not yet kicked in. 