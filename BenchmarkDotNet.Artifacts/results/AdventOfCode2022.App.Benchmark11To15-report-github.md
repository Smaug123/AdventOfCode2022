``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |          Mean |      Error |     StdDev |
|---------- |---- |---------- |--------------:|-----------:|-----------:|
| **Benchmark** |  **11** |     **False** |      **7.213 μs** |  **0.0146 μs** |  **0.0129 μs** |
| **Benchmark** |  **11** |      **True** |  **2,829.950 μs** |  **4.1460 μs** |  **3.8782 μs** |
| **Benchmark** |  **12** |     **False** | **18,335.694 μs** | **27.0932 μs** | **25.3430 μs** |
| **Benchmark** |  **12** |      **True** | **22,075.764 μs** | **21.4952 μs** | **20.1067 μs** |
| **Benchmark** |  **13** |     **False** |    **337.691 μs** |  **1.1381 μs** |  **1.0646 μs** |
| **Benchmark** |  **13** |      **True** |    **614.667 μs** |  **1.4381 μs** |  **1.3452 μs** |
