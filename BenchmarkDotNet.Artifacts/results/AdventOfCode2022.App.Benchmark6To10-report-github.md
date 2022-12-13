``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |       Mean |     Error |     StdDev |
|---------- |---- |---------- |-----------:|----------:|-----------:|
| **Benchmark** |   **6** |     **False** |  **88.040 μs** | **0.2647 μs** |  **0.2476 μs** |
| **Benchmark** |   **6** |      **True** | **170.379 μs** | **1.2751 μs** |  **1.1304 μs** |
| **Benchmark** |   **7** |     **False** | **461.797 μs** | **8.9784 μs** | **12.2898 μs** |
| **Benchmark** |   **7** |      **True** | **461.118 μs** | **5.6912 μs** |  **5.3236 μs** |
| **Benchmark** |   **8** |     **False** | **762.252 μs** | **5.8910 μs** |  **5.5104 μs** |
| **Benchmark** |   **8** |      **True** | **376.716 μs** | **3.3130 μs** |  **3.0990 μs** |
| **Benchmark** |   **9** |     **False** | **505.652 μs** | **2.9322 μs** |  **2.5993 μs** |
| **Benchmark** |   **9** |      **True** | **938.780 μs** | **8.5721 μs** |  **8.0183 μs** |
| **Benchmark** |  **10** |     **False** |   **7.479 μs** | **0.0655 μs** |  **0.0547 μs** |
| **Benchmark** |  **10** |      **True** |   **8.653 μs** | **0.0773 μs** |  **0.0723 μs** |
