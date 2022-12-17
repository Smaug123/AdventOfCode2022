``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **33.14 μs** | **0.350 μs** | **0.310 μs** |
| **Benchmark** |   **1** |      **True** | **33.37 μs** | **0.320 μs** | **0.284 μs** |
| **Benchmark** |   **2** |     **False** | **82.36 μs** | **0.254 μs** | **0.237 μs** |
| **Benchmark** |   **2** |      **True** | **83.74 μs** | **0.291 μs** | **0.243 μs** |
| **Benchmark** |   **3** |     **False** | **67.95 μs** | **1.356 μs** | **1.451 μs** |
| **Benchmark** |   **3** |      **True** | **29.75 μs** | **0.528 μs** | **0.494 μs** |
| **Benchmark** |   **4** |     **False** | **54.30 μs** | **0.234 μs** | **0.207 μs** |
| **Benchmark** |   **4** |      **True** | **65.93 μs** | **0.411 μs** | **0.384 μs** |
| **Benchmark** |   **5** |     **False** | **80.10 μs** | **0.490 μs** | **0.458 μs** |
| **Benchmark** |   **5** |      **True** | **92.79 μs** | **0.616 μs** | **0.576 μs** |
