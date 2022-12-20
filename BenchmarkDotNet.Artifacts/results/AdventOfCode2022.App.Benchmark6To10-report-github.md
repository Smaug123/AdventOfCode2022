``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |     Error |    StdDev |
|---------- |---- |---------- |-----------:|----------:|----------:|
| **Benchmark** |   **6** |     **False** | **162.713 μs** | **0.9106 μs** | **0.7110 μs** |
| **Benchmark** |   **6** |      **True** |  **83.291 μs** | **0.2351 μs** | **0.2084 μs** |
| **Benchmark** |   **7** |     **False** | **441.797 μs** | **0.8446 μs** | **0.6594 μs** |
| **Benchmark** |   **7** |      **True** | **440.557 μs** | **0.7502 μs** | **0.7017 μs** |
| **Benchmark** |   **8** |     **False** | **365.736 μs** | **2.9974 μs** | **2.8038 μs** |
| **Benchmark** |   **8** |      **True** | **748.357 μs** | **0.7033 μs** | **0.5873 μs** |
| **Benchmark** |   **9** |     **False** | **923.710 μs** | **1.6158 μs** | **1.5114 μs** |
| **Benchmark** |   **9** |      **True** | **513.015 μs** | **1.3454 μs** | **1.2585 μs** |
| **Benchmark** |  **10** |     **False** |   **8.294 μs** | **0.0213 μs** | **0.0189 μs** |
| **Benchmark** |  **10** |      **True** |   **7.552 μs** | **0.0127 μs** | **0.0106 μs** |
