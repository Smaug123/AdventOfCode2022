``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |          Mean |       Error |      StdDev |
|---------- |---- |---------- |--------------:|------------:|------------:|
| **Benchmark** |  **11** |     **False** |      **7.318 μs** |   **0.0677 μs** |   **0.0634 μs** |
| **Benchmark** |  **11** |      **True** |  **2,928.997 μs** |  **16.3883 μs** |  **15.3296 μs** |
| **Benchmark** |  **12** |     **False** | **18,825.857 μs** | **166.2955 μs** | **155.5529 μs** |
| **Benchmark** |  **12** |      **True** | **22,745.934 μs** | **156.3040 μs** | **146.2068 μs** |
| **Benchmark** |  **13** |     **False** |    **345.806 μs** |   **2.1535 μs** |   **2.0144 μs** |
| **Benchmark** |  **13** |      **True** |    **637.357 μs** |  **11.2223 μs** |  **10.4973 μs** |
