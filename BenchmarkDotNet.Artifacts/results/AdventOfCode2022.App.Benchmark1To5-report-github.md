``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **32.19 μs** | **0.076 μs** | **0.067 μs** |
| **Benchmark** |   **1** |      **True** | **32.07 μs** | **0.114 μs** | **0.107 μs** |
| **Benchmark** |   **2** |     **False** | **79.54 μs** | **0.419 μs** | **0.392 μs** |
| **Benchmark** |   **2** |      **True** | **81.04 μs** | **0.143 μs** | **0.127 μs** |
| **Benchmark** |   **3** |     **False** | **67.11 μs** | **0.604 μs** | **0.565 μs** |
| **Benchmark** |   **3** |      **True** | **29.12 μs** | **0.296 μs** | **0.277 μs** |
| **Benchmark** |   **4** |     **False** | **53.06 μs** | **0.171 μs** | **0.160 μs** |
| **Benchmark** |   **4** |      **True** | **64.65 μs** | **0.209 μs** | **0.195 μs** |
| **Benchmark** |   **5** |     **False** | **78.68 μs** | **0.322 μs** | **0.252 μs** |
| **Benchmark** |   **5** |      **True** | **91.09 μs** | **0.345 μs** | **0.323 μs** |
