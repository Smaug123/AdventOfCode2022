``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |         Mean |       Error |      StdDev |
|---------- |---- |---------- |-------------:|------------:|------------:|
| **Benchmark** |  **21** |     **False** |     **646.1 μs** |     **3.88 μs** |     **3.63 μs** |
| **Benchmark** |  **21** |      **True** |     **576.8 μs** |     **4.40 μs** |     **4.12 μs** |
| **Benchmark** |  **22** |     **False** |     **328.5 μs** |     **1.49 μs** |     **1.39 μs** |
| **Benchmark** |  **22** |      **True** |     **216.0 μs** |     **1.63 μs** |     **1.52 μs** |
| **Benchmark** |  **23** |     **False** | **357,191.8 μs** | **1,404.56 μs** | **1,313.82 μs** |
| **Benchmark** |  **23** |      **True** |   **3,190.7 μs** |     **7.80 μs** |     **6.91 μs** |
