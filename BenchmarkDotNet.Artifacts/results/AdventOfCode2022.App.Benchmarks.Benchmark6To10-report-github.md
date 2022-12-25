``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |     Error |    StdDev |
|---------- |---- |---------- |-----------:|----------:|----------:|
| **Benchmark** |   **6** |     **False** |  **53.708 μs** | **1.0291 μs** | **1.3015 μs** |
| **Benchmark** |   **6** |      **True** |  **16.920 μs** | **0.1420 μs** | **0.1108 μs** |
| **Benchmark** |   **7** |     **False** | **442.469 μs** | **1.3215 μs** | **1.2362 μs** |
| **Benchmark** |   **7** |      **True** | **442.077 μs** | **1.1099 μs** | **1.0382 μs** |
| **Benchmark** |   **8** |     **False** | **278.609 μs** | **1.9329 μs** | **1.8080 μs** |
| **Benchmark** |   **8** |      **True** | **552.077 μs** | **4.2246 μs** | **3.9517 μs** |
| **Benchmark** |   **9** |     **False** | **877.123 μs** | **3.1055 μs** | **2.9049 μs** |
| **Benchmark** |   **9** |      **True** | **453.615 μs** | **1.6385 μs** | **1.5327 μs** |
| **Benchmark** |  **10** |     **False** |   **8.216 μs** | **0.0179 μs** | **0.0150 μs** |
| **Benchmark** |  **10** |      **True** |   **7.433 μs** | **0.0146 μs** | **0.0129 μs** |
