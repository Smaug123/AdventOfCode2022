``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |         Error |      StdDev |
|---------- |---- |---------- |---------------:|--------------:|------------:|
| **Benchmark** |  **11** |     **False** |   **2,813.869 μs** |    **14.6687 μs** |  **12.2490 μs** |
| **Benchmark** |  **11** |      **True** |       **7.198 μs** |     **0.0554 μs** |   **0.0519 μs** |
| **Benchmark** |  **12** |     **False** |  **22,499.829 μs** |   **361.1070 μs** | **281.9286 μs** |
| **Benchmark** |  **12** |      **True** |  **18,836.926 μs** |   **136.6720 μs** | **121.1562 μs** |
| **Benchmark** |  **13** |     **False** |     **392.867 μs** |     **2.6808 μs** |   **2.3764 μs** |
| **Benchmark** |  **13** |      **True** |     **374.141 μs** |     **2.5758 μs** |   **2.4094 μs** |
| **Benchmark** |  **14** |     **False** |   **4,157.513 μs** |    **23.2704 μs** |  **20.6286 μs** |
| **Benchmark** |  **14** |      **True** |     **347.943 μs** |     **4.7107 μs** |   **4.4064 μs** |
| **Benchmark** |  **15** |     **False** |      **52.101 μs** |     **0.6099 μs** |   **0.5705 μs** |
| **Benchmark** |  **15** |      **True** | **179,334.650 μs** | **1,074.9237 μs** | **839.2297 μs** |
