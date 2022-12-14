``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |          Mean |      Error |     StdDev |
|---------- |---- |---------- |--------------:|-----------:|-----------:|
| **Benchmark** |  **11** |     **False** |      **7.227 μs** |  **0.0235 μs** |  **0.0220 μs** |
| **Benchmark** |  **11** |      **True** |  **2,870.251 μs** |  **5.5620 μs** |  **5.2027 μs** |
| **Benchmark** |  **12** |     **False** | **20,360.326 μs** | **56.7953 μs** | **50.3475 μs** |
| **Benchmark** |  **12** |      **True** | **21,665.562 μs** | **63.5696 μs** | **56.3528 μs** |
| **Benchmark** |  **13** |     **False** |    **335.692 μs** |  **1.3893 μs** |  **1.2995 μs** |
| **Benchmark** |  **13** |      **True** |    **615.878 μs** |  **1.9194 μs** |  **1.6028 μs** |
| **Benchmark** |  **14** |     **False** |    **357.696 μs** |  **1.3375 μs** |  **1.1169 μs** |
| **Benchmark** |  **14** |      **True** |  **4,645.477 μs** | **11.4562 μs** | **10.1556 μs** |
