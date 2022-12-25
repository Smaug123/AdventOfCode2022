``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **31.92 μs** | **0.103 μs** | **0.091 μs** |
| **Benchmark** |   **1** |      **True** | **31.86 μs** | **0.049 μs** | **0.044 μs** |
| **Benchmark** |   **2** |     **False** | **79.57 μs** | **0.094 μs** | **0.078 μs** |
| **Benchmark** |   **2** |      **True** | **81.06 μs** | **0.315 μs** | **0.295 μs** |
| **Benchmark** |   **3** |     **False** | **65.75 μs** | **0.546 μs** | **0.510 μs** |
| **Benchmark** |   **3** |      **True** | **31.96 μs** | **0.439 μs** | **0.411 μs** |
| **Benchmark** |   **4** |     **False** | **52.70 μs** | **0.155 μs** | **0.130 μs** |
| **Benchmark** |   **4** |      **True** | **63.71 μs** | **0.230 μs** | **0.204 μs** |
| **Benchmark** |   **5** |     **False** | **78.74 μs** | **0.781 μs** | **0.692 μs** |
| **Benchmark** |   **5** |      **True** | **91.26 μs** | **0.278 μs** | **0.247 μs** |
