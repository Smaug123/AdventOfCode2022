``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |       Error |      StdDev |
|---------- |---- |---------- |---------------:|------------:|------------:|
| **Benchmark** |  **11** |     **False** |   **2,862.919 μs** |  **19.7895 μs** |  **16.5251 μs** |
| **Benchmark** |  **11** |      **True** |       **7.262 μs** |   **0.0519 μs** |   **0.0460 μs** |
| **Benchmark** |  **12** |     **False** |  **20,219.075 μs** |  **39.5568 μs** |  **35.0661 μs** |
| **Benchmark** |  **12** |      **True** |  **18,677.669 μs** | **198.5503 μs** | **176.0097 μs** |
| **Benchmark** |  **13** |     **False** |     **646.469 μs** |   **9.7092 μs** |   **8.6069 μs** |
| **Benchmark** |  **13** |      **True** |     **351.817 μs** |   **1.0058 μs** |   **0.8916 μs** |
| **Benchmark** |  **14** |     **False** |   **4,311.168 μs** |   **9.2836 μs** |   **7.7522 μs** |
| **Benchmark** |  **14** |      **True** |     **376.619 μs** |   **0.8042 μs** |   **0.6715 μs** |
| **Benchmark** |  **15** |     **False** |      **33.923 μs** |   **0.1404 μs** |   **0.1313 μs** |
| **Benchmark** |  **15** |      **True** | **180,017.411 μs** | **448.7256 μs** | **374.7062 μs** |
