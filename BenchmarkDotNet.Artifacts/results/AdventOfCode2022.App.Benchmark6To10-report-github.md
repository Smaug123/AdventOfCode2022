``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |     Error |    StdDev |
|---------- |---- |---------- |-----------:|----------:|----------:|
| **Benchmark** |   **6** |     **False** | **172.061 μs** | **0.6541 μs** | **0.6118 μs** |
| **Benchmark** |   **6** |      **True** |  **91.573 μs** | **0.4029 μs** | **0.3571 μs** |
| **Benchmark** |   **7** |     **False** | **474.356 μs** | **2.4094 μs** | **2.2537 μs** |
| **Benchmark** |   **7** |      **True** | **471.122 μs** | **1.8145 μs** | **1.6973 μs** |
| **Benchmark** |   **8** |     **False** | **383.782 μs** | **1.8874 μs** | **1.7655 μs** |
| **Benchmark** |   **8** |      **True** | **773.921 μs** | **2.0381 μs** | **1.9064 μs** |
| **Benchmark** |   **9** |     **False** | **968.365 μs** | **3.3664 μs** | **2.9842 μs** |
| **Benchmark** |   **9** |      **True** | **544.818 μs** | **2.7093 μs** | **2.5343 μs** |
| **Benchmark** |  **10** |     **False** |   **8.523 μs** | **0.0358 μs** | **0.0335 μs** |
| **Benchmark** |  **10** |      **True** |   **7.651 μs** | **0.0135 μs** | **0.0120 μs** |
