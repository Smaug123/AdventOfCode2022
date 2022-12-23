``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |         Mean |     Error |    StdDev |
|---------- |---- |---------- |-------------:|----------:|----------:|
| **Benchmark** |  **21** |     **False** |     **720.1 μs** |  **11.79 μs** |  **11.03 μs** |
| **Benchmark** |  **21** |      **True** |     **588.8 μs** |   **3.52 μs** |   **3.12 μs** |
| **Benchmark** |  **22** |     **False** |     **336.7 μs** |   **2.82 μs** |   **2.50 μs** |
| **Benchmark** |  **22** |      **True** |     **220.5 μs** |   **1.51 μs** |   **1.41 μs** |
| **Benchmark** |  **23** |     **False** | **333,927.6 μs** | **577.75 μs** | **512.16 μs** |
| **Benchmark** |  **23** |      **True** |   **2,782.4 μs** |   **7.04 μs** |   **6.58 μs** |
