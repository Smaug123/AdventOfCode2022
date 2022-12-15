``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **32.57 μs** | **0.230 μs** | **0.215 μs** |
| **Benchmark** |   **1** |      **True** | **32.34 μs** | **0.174 μs** | **0.145 μs** |
| **Benchmark** |   **2** |     **False** | **84.01 μs** | **0.695 μs** | **0.616 μs** |
| **Benchmark** |   **2** |      **True** | **81.61 μs** | **0.181 μs** | **0.169 μs** |
| **Benchmark** |   **3** |     **False** | **36.69 μs** | **0.218 μs** | **0.204 μs** |
| **Benchmark** |   **3** |      **True** | **73.46 μs** | **0.473 μs** | **0.442 μs** |
| **Benchmark** |   **4** |     **False** | **68.84 μs** | **0.729 μs** | **0.682 μs** |
| **Benchmark** |   **4** |      **True** | **55.35 μs** | **0.458 μs** | **0.406 μs** |
| **Benchmark** |   **5** |     **False** | **96.76 μs** | **1.803 μs** | **1.771 μs** |
| **Benchmark** |   **5** |      **True** | **83.76 μs** | **0.354 μs** | **0.332 μs** |
