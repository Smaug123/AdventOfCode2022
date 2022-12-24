``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |         Mean |       Error |      StdDev |
|---------- |---- |---------- |-------------:|------------:|------------:|
| **Benchmark** |  **21** |     **False** |     **640.9 μs** |     **1.31 μs** |     **1.23 μs** |
| **Benchmark** |  **21** |      **True** |     **579.0 μs** |     **8.23 μs** |     **7.70 μs** |
| **Benchmark** |  **22** |     **False** |     **326.6 μs** |     **2.11 μs** |     **1.97 μs** |
| **Benchmark** |  **22** |      **True** |     **217.7 μs** |     **1.04 μs** |     **0.97 μs** |
| **Benchmark** |  **23** |     **False** | **318,993.7 μs** | **4,929.31 μs** | **4,610.88 μs** |
| **Benchmark** |  **23** |      **True** |   **2,714.7 μs** |     **4.32 μs** |     **3.61 μs** |
| **Benchmark** |  **24** |     **False** |  **65,379.9 μs** |   **159.24 μs** |   **124.32 μs** |
| **Benchmark** |  **24** |      **True** |  **21,290.9 μs** |    **34.03 μs** |    **28.41 μs** |
