``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |       Error |      StdDev |
|---------- |---- |---------- |---------------:|------------:|------------:|
| **Benchmark** |  **11** |     **False** |   **2,899.131 μs** |   **7.8108 μs** |   **7.3063 μs** |
| **Benchmark** |  **11** |      **True** |       **7.382 μs** |   **0.0168 μs** |   **0.0157 μs** |
| **Benchmark** |  **12** |     **False** |  **22,757.168 μs** |  **48.8043 μs** |  **40.7538 μs** |
| **Benchmark** |  **12** |      **True** |  **20,172.666 μs** |  **36.7869 μs** |  **32.6106 μs** |
| **Benchmark** |  **13** |     **False** |     **643.723 μs** |   **1.8549 μs** |   **1.6443 μs** |
| **Benchmark** |  **13** |      **True** |     **353.664 μs** |   **1.0564 μs** |   **0.9882 μs** |
| **Benchmark** |  **14** |     **False** |   **4,240.325 μs** |  **14.9432 μs** |  **13.9779 μs** |
| **Benchmark** |  **14** |      **True** |     **376.356 μs** |   **1.1372 μs** |   **1.0637 μs** |
| **Benchmark** |  **15** |     **False** |      **33.683 μs** |   **0.0796 μs** |   **0.0744 μs** |
| **Benchmark** |  **15** |      **True** | **179,272.343 μs** | **458.4560 μs** | **357.9323 μs** |
