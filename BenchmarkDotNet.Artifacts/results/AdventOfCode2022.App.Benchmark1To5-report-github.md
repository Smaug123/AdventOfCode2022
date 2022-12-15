``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |   StdDev |
|---------- |---- |---------- |---------:|---------:|---------:|
| **Benchmark** |   **1** |     **False** | **32.80 μs** | **0.076 μs** | **0.071 μs** |
| **Benchmark** |   **1** |      **True** | **32.72 μs** | **0.158 μs** | **0.147 μs** |
| **Benchmark** |   **2** |     **False** | **83.97 μs** | **0.736 μs** | **0.688 μs** |
| **Benchmark** |   **2** |      **True** | **85.55 μs** | **0.919 μs** | **0.859 μs** |
| **Benchmark** |   **3** |     **False** | **73.60 μs** | **0.465 μs** | **0.435 μs** |
| **Benchmark** |   **3** |      **True** | **31.17 μs** | **0.207 μs** | **0.184 μs** |
| **Benchmark** |   **4** |     **False** | **54.68 μs** | **0.369 μs** | **0.327 μs** |
| **Benchmark** |   **4** |      **True** | **67.24 μs** | **0.349 μs** | **0.291 μs** |
| **Benchmark** |   **5** |     **False** | **82.63 μs** | **0.315 μs** | **0.294 μs** |
| **Benchmark** |   **5** |      **True** | **95.83 μs** | **0.339 μs** | **0.317 μs** |
