``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |      Error |    StdDev |
|---------- |---- |---------- |-----------:|-----------:|----------:|
| **Benchmark** |   **6** |     **False** |  **52.298 μs** |  **1.0260 μs** | **1.8762 μs** |
| **Benchmark** |   **6** |      **True** |  **16.584 μs** |  **0.1464 μs** | **0.1369 μs** |
| **Benchmark** |   **7** |     **False** | **444.348 μs** |  **2.6326 μs** | **2.4625 μs** |
| **Benchmark** |   **7** |      **True** | **446.293 μs** |  **1.9890 μs** | **1.8605 μs** |
| **Benchmark** |   **8** |     **False** | **280.641 μs** |  **1.2356 μs** | **1.1558 μs** |
| **Benchmark** |   **8** |      **True** | **551.138 μs** |  **5.4089 μs** | **4.5166 μs** |
| **Benchmark** |   **9** |     **False** | **886.578 μs** | **10.4698 μs** | **9.7934 μs** |
| **Benchmark** |   **9** |      **True** | **450.062 μs** |  **5.8633 μs** | **5.4845 μs** |
| **Benchmark** |  **10** |     **False** |   **8.373 μs** |  **0.0165 μs** | **0.0128 μs** |
| **Benchmark** |  **10** |      **True** |   **7.495 μs** |  **0.0311 μs** | **0.0259 μs** |
