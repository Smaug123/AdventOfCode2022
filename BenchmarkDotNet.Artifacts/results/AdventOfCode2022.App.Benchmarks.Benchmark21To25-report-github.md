``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |         Mean |       Error |      StdDev |
|---------- |---- |---------- |-------------:|------------:|------------:|
| **Benchmark** |  **21** |     **False** |     **640.0 μs** |     **4.20 μs** |     **3.73 μs** |
| **Benchmark** |  **21** |      **True** |     **579.2 μs** |     **2.69 μs** |     **2.38 μs** |
| **Benchmark** |  **22** |     **False** |     **325.0 μs** |     **1.48 μs** |     **1.38 μs** |
| **Benchmark** |  **22** |      **True** |     **218.2 μs** |     **0.49 μs** |     **0.43 μs** |
| **Benchmark** |  **23** |     **False** | **318,951.5 μs** | **4,809.31 μs** | **4,498.63 μs** |
| **Benchmark** |  **23** |      **True** |   **2,715.4 μs** |     **4.39 μs** |     **3.43 μs** |
| **Benchmark** |  **24** |     **False** |  **47,682.2 μs** |    **44.89 μs** |    **39.79 μs** |
| **Benchmark** |  **24** |      **True** |  **15,713.4 μs** |   **208.12 μs** |   **173.79 μs** |
