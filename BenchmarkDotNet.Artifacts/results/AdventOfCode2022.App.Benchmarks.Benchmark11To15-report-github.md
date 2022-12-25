``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |       Error |      StdDev |
|---------- |---- |---------- |---------------:|------------:|------------:|
| **Benchmark** |  **11** |     **False** |   **2,816.897 μs** |   **3.4524 μs** |   **2.8829 μs** |
| **Benchmark** |  **11** |      **True** |       **7.143 μs** |   **0.0171 μs** |   **0.0152 μs** |
| **Benchmark** |  **12** |     **False** |  **20,082.913 μs** |  **58.7261 μs** |  **52.0592 μs** |
| **Benchmark** |  **12** |      **True** |  **20,100.938 μs** |  **35.8436 μs** |  **27.9844 μs** |
| **Benchmark** |  **13** |     **False** |     **388.554 μs** |   **1.4083 μs** |   **1.3173 μs** |
| **Benchmark** |  **13** |      **True** |     **370.303 μs** |   **0.8468 μs** |   **0.7921 μs** |
| **Benchmark** |  **14** |     **False** |   **4,047.039 μs** |   **6.5252 μs** |   **5.7845 μs** |
| **Benchmark** |  **14** |      **True** |     **336.346 μs** |   **1.2479 μs** |   **1.1062 μs** |
| **Benchmark** |  **15** |     **False** |      **51.410 μs** |   **0.2912 μs** |   **0.2724 μs** |
| **Benchmark** |  **15** |      **True** | **176,371.580 μs** | **855.0546 μs** | **714.0093 μs** |
