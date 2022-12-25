``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method |        args |           Mean |         Error |        StdDev |
|---------- |------------ |---------------:|--------------:|--------------:|
| **Benchmark** | **(21, False)** |     **637.049 μs** |     **1.0056 μs** |     **0.8397 μs** |
| **Benchmark** |  **(21, True)** |     **575.965 μs** |     **6.5023 μs** |     **5.4297 μs** |
| **Benchmark** | **(22, False)** |     **329.169 μs** |     **1.9539 μs** |     **1.8277 μs** |
| **Benchmark** |  **(22, True)** |     **216.176 μs** |     **0.5694 μs** |     **0.5327 μs** |
| **Benchmark** | **(23, False)** | **318,432.306 μs** | **5,235.8275 μs** | **4,897.5963 μs** |
| **Benchmark** |  **(23, True)** |   **2,694.296 μs** |     **4.5002 μs** |     **3.7579 μs** |
| **Benchmark** | **(24, False)** |  **47,718.692 μs** |   **106.8933 μs** |    **89.2607 μs** |
| **Benchmark** |  **(24, True)** |  **15,540.162 μs** |    **23.0957 μs** |    **19.2860 μs** |
| **Benchmark** |  **(25, True)** |       **4.388 μs** |     **0.0072 μs** |     **0.0056 μs** |
