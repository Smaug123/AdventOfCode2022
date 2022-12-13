``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **32.24 μs** | **0.046 μs** | **0.043 μs** |
| **Benchmark** |   **1** |      **True** | **32.81 μs** | **0.370 μs** | **0.346 μs** |
| **Benchmark** |   **2** |     **False** | **83.36 μs** | **1.135 μs** | **1.061 μs** |
| **Benchmark** |   **2** |      **True** | **81.54 μs** | **1.379 μs** | **1.290 μs** |
| **Benchmark** |   **3** |     **False** | **33.58 μs** | **0.662 μs** | **0.906 μs** |
| **Benchmark** |   **3** |      **True** | **70.99 μs** | **1.382 μs** | **1.357 μs** |
| **Benchmark** |   **4** |     **False** | **65.90 μs** | **0.479 μs** | **0.448 μs** |
| **Benchmark** |   **4** |      **True** | **54.70 μs** | **0.489 μs** | **0.458 μs** |
| **Benchmark** |   **5** |     **False** | **93.13 μs** | **1.223 μs** | **1.084 μs** |
| **Benchmark** |   **5** |      **True** | **81.07 μs** | **0.257 μs** | **0.228 μs** |
