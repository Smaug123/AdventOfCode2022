``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |         Mean |       Error |      StdDev |
|---------- |---- |---------- |-------------:|------------:|------------:|
| **Benchmark** |  **21** |     **False** |     **641.1 μs** |     **1.80 μs** |     **1.69 μs** |
| **Benchmark** |  **21** |      **True** |     **580.3 μs** |     **6.82 μs** |     **6.05 μs** |
| **Benchmark** |  **22** |     **False** |     **323.9 μs** |     **0.66 μs** |     **0.52 μs** |
| **Benchmark** |  **22** |      **True** |     **218.4 μs** |     **4.35 μs** |     **4.28 μs** |
| **Benchmark** |  **23** |     **False** | **318,156.3 μs** | **4,910.78 μs** | **4,593.55 μs** |
| **Benchmark** |  **23** |      **True** |   **2,684.9 μs** |     **5.12 μs** |     **4.27 μs** |
| **Benchmark** |  **24** |     **False** |  **47,410.3 μs** |    **63.64 μs** |    **56.42 μs** |
| **Benchmark** |  **24** |      **True** |  **15,613.9 μs** |    **81.16 μs** |    **71.95 μs** |
