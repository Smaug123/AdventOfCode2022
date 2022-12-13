``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **32.03 μs** | **0.108 μs** | **0.101 μs** |
| **Benchmark** |   **1** |      **True** | **32.00 μs** | **0.051 μs** | **0.045 μs** |
| **Benchmark** |   **2** |     **False** | **81.14 μs** | **0.177 μs** | **0.166 μs** |
| **Benchmark** |   **2** |      **True** | **79.17 μs** | **0.158 μs** | **0.140 μs** |
| **Benchmark** |   **3** |     **False** | **30.16 μs** | **0.395 μs** | **0.369 μs** |
| **Benchmark** |   **3** |      **True** | **68.15 μs** | **1.358 μs** | **1.510 μs** |
| **Benchmark** |   **4** |     **False** | **66.18 μs** | **0.187 μs** | **0.175 μs** |
| **Benchmark** |   **4** |      **True** | **54.17 μs** | **0.171 μs** | **0.152 μs** |
| **Benchmark** |   **5** |     **False** | **90.90 μs** | **0.109 μs** | **0.091 μs** |
| **Benchmark** |   **5** |      **True** | **78.93 μs** | **0.262 μs** | **0.245 μs** |
