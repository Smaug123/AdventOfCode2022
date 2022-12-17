``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |     Error |    StdDev |
|---------- |---- |---------- |-----------:|----------:|----------:|
| **Benchmark** |   **6** |     **False** | **175.372 μs** | **0.9155 μs** | **0.8564 μs** |
| **Benchmark** |   **6** |      **True** |  **87.361 μs** | **0.6016 μs** | **0.5628 μs** |
| **Benchmark** |   **7** |     **False** | **458.427 μs** | **5.3378 μs** | **4.9930 μs** |
| **Benchmark** |   **7** |      **True** | **455.400 μs** | **4.3545 μs** | **4.0732 μs** |
| **Benchmark** |   **8** |     **False** | **385.780 μs** | **2.2483 μs** | **1.9930 μs** |
| **Benchmark** |   **8** |      **True** | **773.037 μs** | **1.7013 μs** | **1.5914 μs** |
| **Benchmark** |   **9** |     **False** | **977.263 μs** | **5.4201 μs** | **5.0699 μs** |
| **Benchmark** |   **9** |      **True** | **550.864 μs** | **2.3154 μs** | **2.1658 μs** |
| **Benchmark** |  **10** |     **False** |   **8.738 μs** | **0.0265 μs** | **0.0207 μs** |
| **Benchmark** |  **10** |      **True** |   **7.804 μs** | **0.1063 μs** | **0.0942 μs** |
