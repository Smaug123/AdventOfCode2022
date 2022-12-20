``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **32.13 μs** | **0.239 μs** | **0.212 μs** |
| **Benchmark** |   **1** |      **True** | **31.73 μs** | **0.072 μs** | **0.060 μs** |
| **Benchmark** |   **2** |     **False** | **79.43 μs** | **0.149 μs** | **0.132 μs** |
| **Benchmark** |   **2** |      **True** | **80.64 μs** | **0.194 μs** | **0.172 μs** |
| **Benchmark** |   **3** |     **False** | **65.79 μs** | **0.631 μs** | **0.590 μs** |
| **Benchmark** |   **3** |      **True** | **33.10 μs** | **0.206 μs** | **0.172 μs** |
| **Benchmark** |   **4** |     **False** | **52.37 μs** | **0.135 μs** | **0.120 μs** |
| **Benchmark** |   **4** |      **True** | **63.40 μs** | **0.137 μs** | **0.128 μs** |
| **Benchmark** |   **5** |     **False** | **79.16 μs** | **0.365 μs** | **0.304 μs** |
| **Benchmark** |   **5** |      **True** | **91.81 μs** | **0.304 μs** | **0.284 μs** |
