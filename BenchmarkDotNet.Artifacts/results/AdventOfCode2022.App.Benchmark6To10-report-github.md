``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |     Error |    StdDev |
|---------- |---- |---------- |-----------:|----------:|----------:|
| **Benchmark** |   **6** |     **False** |  **91.954 μs** | **0.1871 μs** | **0.1562 μs** |
| **Benchmark** |   **6** |      **True** | **171.482 μs** | **0.5808 μs** | **0.5433 μs** |
| **Benchmark** |   **7** |     **False** | **476.825 μs** | **1.9172 μs** | **1.7934 μs** |
| **Benchmark** |   **7** |      **True** | **472.354 μs** | **1.2641 μs** | **1.1206 μs** |
| **Benchmark** |   **8** |     **False** | **772.475 μs** | **1.3885 μs** | **1.2988 μs** |
| **Benchmark** |   **8** |      **True** | **385.165 μs** | **1.2956 μs** | **1.2119 μs** |
| **Benchmark** |   **9** |     **False** | **556.131 μs** | **1.6443 μs** | **1.5381 μs** |
| **Benchmark** |   **9** |      **True** | **973.207 μs** | **2.4001 μs** | **2.2451 μs** |
| **Benchmark** |  **10** |     **False** |  **10.265 μs** | **0.0506 μs** | **0.0473 μs** |
| **Benchmark** |  **10** |      **True** |   **8.528 μs** | **0.0226 μs** | **0.0212 μs** |
