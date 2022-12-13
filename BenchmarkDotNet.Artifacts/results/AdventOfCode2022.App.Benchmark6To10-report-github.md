``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |     Error |    StdDev |
|---------- |---- |---------- |-----------:|----------:|----------:|
| **Benchmark** |   **6** |     **False** |  **87.944 μs** | **0.1189 μs** | **0.0929 μs** |
| **Benchmark** |   **6** |      **True** | **167.267 μs** | **0.4510 μs** | **0.4219 μs** |
| **Benchmark** |   **7** |     **False** | **441.047 μs** | **1.0432 μs** | **0.8711 μs** |
| **Benchmark** |   **7** |      **True** | **441.957 μs** | **1.5370 μs** | **1.3625 μs** |
| **Benchmark** |   **8** |     **False** | **743.344 μs** | **1.2323 μs** | **1.0290 μs** |
| **Benchmark** |   **8** |      **True** | **366.429 μs** | **2.2239 μs** | **2.0802 μs** |
| **Benchmark** |   **9** |     **False** | **486.852 μs** | **1.9595 μs** | **1.7371 μs** |
| **Benchmark** |   **9** |      **True** | **903.340 μs** | **2.4338 μs** | **2.1575 μs** |
| **Benchmark** |  **10** |     **False** |   **7.344 μs** | **0.0137 μs** | **0.0129 μs** |
| **Benchmark** |  **10** |      **True** |   **8.250 μs** | **0.0198 μs** | **0.0185 μs** |
