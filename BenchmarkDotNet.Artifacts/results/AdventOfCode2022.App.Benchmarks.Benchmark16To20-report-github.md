``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |        Error |       StdDev |
|---------- |---- |---------- |---------------:|-------------:|-------------:|
| **Benchmark** |  **16** |     **False** | **3,374,503.8 μs** | **52,507.02 μs** | **49,115.10 μs** |
| **Benchmark** |  **16** |      **True** |   **327,698.3 μs** |    **600.56 μs** |    **501.49 μs** |
| **Benchmark** |  **17** |     **False** |     **2,998.1 μs** |      **6.00 μs** |      **5.01 μs** |
| **Benchmark** |  **17** |      **True** |     **1,558.3 μs** |      **4.51 μs** |      **4.22 μs** |
| **Benchmark** |  **18** |     **False** |    **42,879.1 μs** |     **61.32 μs** |     **54.36 μs** |
| **Benchmark** |  **18** |      **True** |       **128.1 μs** |      **0.71 μs** |      **0.67 μs** |
| **Benchmark** |  **19** |     **False** |   **560,825.7 μs** |    **715.96 μs** |    **597.86 μs** |
| **Benchmark** |  **19** |      **True** |   **679,746.9 μs** |  **5,022.34 μs** |  **4,697.90 μs** |
| **Benchmark** |  **20** |     **False** |   **117,412.5 μs** |    **126.43 μs** |    **112.07 μs** |
| **Benchmark** |  **20** |      **True** |    **12,717.7 μs** |     **11.25 μs** |      **9.39 μs** |
