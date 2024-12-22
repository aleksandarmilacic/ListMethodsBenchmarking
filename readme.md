# Benchmarking List and Matrix Modification Methods

This project benchmarks various methods for modifying a list of integers and performing matrix multiplications in C#. Using [BenchmarkDotNet](https://benchmarkdotnet.org/), we analyze the performance and memory allocation of each method to understand their trade-offs.

## Benchmarked Scenarios

### List Modification Methods
1. **For Loop**: A traditional `for` loop that modifies elements in-place.
2. **LinqSelect**: Uses LINQ `Select` to create a new transformed array.
3. **Parallel.For**: A multithreaded approach to modify the array in parallel.
4. **Span**: Uses `Span<T>` for optimized in-place modification.
5. **Plinq**: Utilizes Parallel LINQ for parallel data processing and transformation.
6. **ForEachLoop**: Uses a `foreach` loop to iterate over elements and modify them.

### Matrix Multiplication Methods
1. **MultiplyUsingForLoops**: A nested `for` loop approach for traditional matrix multiplication.
2. **MultiplyUsingParallelFor**: A multithreaded approach using `Parallel.For` for matrix multiplication.
3. **MultiplyUsingPlinq**: A LINQ-based parallel approach for handling rows during matrix multiplication.

## Prerequisites

- **.NET 9 SDK** or later.
- Visual Studio or another C# IDE.
- Install BenchmarkDotNet via NuGet:

  ```bash
  dotnet add package BenchmarkDotNet
  ```

## Running the Benchmarks

1. Clone the repository or copy the code to your local environment.
2. Open the project in your IDE.
3. Set the build configuration to **Release**.
4. Run the program. BenchmarkDotNet will execute the benchmarks and output the results.

## Sample Output

### List Modification Results
```plaintext
| Method       | Mean    | Error  | StdDev  | Allocated |
|--------------|---------|--------|---------|-----------|
| ForLoop      | 143.4 탎| 2.85 탎|  8.04 탎|  390.69 KB|
| ForEachLoop  | 153.3 탎| 3.01 탎|  4.12 탎|  390.69 KB|
| LinqSelect   | 248.4 탎| 4.93 탎| 10.93 탎|  781.43 KB|
| ParallelFor  | 271.0 탎| 5.31 탎|  9.83 탎|  395.37 KB|
| Span         | 143.9 탎| 2.84 탎|  6.42 탎|  390.69 KB|
| Plinq        | 469.5 탎| 9.31 탎| 22.84 탎| 1816.63 KB|
```

### Matrix Multiplication Results
```plaintext
| Method                  | Mean     | Error   | StdDev  | Allocated |
|-------------------------|----------|---------|---------|-----------|
| MultiplyUsingForLoops   | 266.34 ms| 5.061 ms| 5.828 ms| 976.8 KB  |
| MultiplyUsingParallelFor|  52.76 ms| 0.998 ms| 1.109 ms| 987.19 KB |
| MultiplyUsingPlinq      |  32.40 ms| 0.648 ms| 1.232 ms| 1985.75 KB|
```

## Observations

### List Modification
- **For Loop** and **Span** are the fastest methods with minimal memory allocation.
- **LinqSelect** and **Plinq** are slower due to additional allocations for new arrays.
- **Parallel.For** can be beneficial for large datasets but introduces threading overhead.
- **ForEachLoop** is slightly slower than the `for` loop but still competitive in memory usage.

### Matrix Multiplication
- **MultiplyUsingPlinq** outperforms others in execution time but uses the most memory due to parallel overhead.
- **MultiplyUsingParallelFor** provides a good balance between speed and memory usage, making it a great choice for multi-core systems.
- **MultiplyUsingForLoops** is the slowest but the simplest and most memory-efficient method.

## Use Cases

### List Modification
- **For Loop / Span**: Use when performance and minimal memory allocation are critical.
- **LinqSelect**: Suitable for declarative coding when readability is a priority.
- **Parallel.For / Plinq**: Ideal for CPU-intensive operations over large datasets.
- **ForEachLoop**: Good for simplicity and when index-based modification isn't required.

### Matrix Multiplication
- **MultiplyUsingPlinq**: Best for computationally intensive workloads on multi-core systems.
- **MultiplyUsingParallelFor**: A balanced approach for parallel matrix operations.
- **MultiplyUsingForLoops**: Use when simplicity and minimal memory overhead are required.

## How to Extend

To add more benchmarks:
1. Add a new method to the relevant benchmark class.
2. Decorate it with the `[Benchmark]` attribute.
3. Re-run the program to include the new benchmark in the results.

## License

This project is licensed under the MIT License.
