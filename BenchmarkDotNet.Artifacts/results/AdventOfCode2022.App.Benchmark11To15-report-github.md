``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |         Error |        StdDev |
|---------- |---- |---------- |---------------:|--------------:|--------------:|
| **Benchmark** |  **11** |     **False** |   **2,820.457 μs** |     **3.7483 μs** |     **3.3227 μs** |
| **Benchmark** |  **11** |      **True** |       **7.169 μs** |     **0.0174 μs** |     **0.0155 μs** |
| **Benchmark** |  **12** |     **False** |  **21,549.304 μs** |    **27.9526 μs** |    **23.3417 μs** |
| **Benchmark** |  **12** |      **True** |  **20,387.439 μs** |    **37.2080 μs** |    **31.0703 μs** |
| **Benchmark** |  **13** |     **False** |     **613.099 μs** |     **0.6537 μs** |     **0.5459 μs** |
| **Benchmark** |  **13** |      **True** |     **335.878 μs** |     **0.7367 μs** |     **0.6531 μs** |
| **Benchmark** |  **14** |     **False** |   **4,092.648 μs** |     **6.5945 μs** |     **5.5067 μs** |
| **Benchmark** |  **14** |      **True** |     **356.154 μs** |     **0.6613 μs** |     **0.6186 μs** |
| **Benchmark** |  **15** |     **False** |      **52.149 μs** |     **0.3480 μs** |     **0.3085 μs** |
| **Benchmark** |  **15** |      **True** | **176,786.977 μs** | **2,760.3078 μs** | **2,155.0668 μs** |
