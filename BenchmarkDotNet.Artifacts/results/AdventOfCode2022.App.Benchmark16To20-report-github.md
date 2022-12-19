``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |        Error |       StdDev |
|---------- |---- |---------- |---------------:|-------------:|-------------:|
| **Benchmark** |  **16** |     **False** | **3,276,777.7 μs** | **47,105.18 μs** | **44,062.21 μs** |
| **Benchmark** |  **16** |      **True** |   **328,457.3 μs** |  **1,233.98 μs** |  **1,154.27 μs** |
| **Benchmark** |  **17** |     **False** |     **2,994.8 μs** |      **9.41 μs** |      **7.86 μs** |
| **Benchmark** |  **17** |      **True** |     **1,562.7 μs** |      **5.96 μs** |      **5.57 μs** |
| **Benchmark** |  **18** |     **False** |    **43,162.9 μs** |     **92.81 μs** |     **82.28 μs** |
| **Benchmark** |  **18** |      **True** |       **137.7 μs** |      **2.75 μs** |      **2.95 μs** |
| **Benchmark** |  **19** |     **False** |   **574,489.3 μs** |  **2,008.49 μs** |  **1,780.47 μs** |
| **Benchmark** |  **19** |      **True** |   **701,588.1 μs** |  **4,772.02 μs** |  **4,230.27 μs** |
