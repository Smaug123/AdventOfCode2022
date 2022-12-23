``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |        Error |       StdDev |
|---------- |---- |---------- |---------------:|-------------:|-------------:|
| **Benchmark** |  **16** |     **False** | **3,528,338.8 μs** | **69,294.15 μs** | **82,489.76 μs** |
| **Benchmark** |  **16** |      **True** |   **377,458.7 μs** |    **975.99 μs** |    **815.00 μs** |
| **Benchmark** |  **17** |     **False** |     **3,102.7 μs** |     **60.69 μs** |     **53.80 μs** |
| **Benchmark** |  **17** |      **True** |     **1,593.5 μs** |      **6.95 μs** |      **5.80 μs** |
| **Benchmark** |  **18** |     **False** |    **43,915.2 μs** |    **313.88 μs** |    **293.61 μs** |
| **Benchmark** |  **18** |      **True** |       **139.9 μs** |      **1.99 μs** |      **1.86 μs** |
| **Benchmark** |  **19** |     **False** |   **570,969.8 μs** |  **3,119.46 μs** |  **2,917.94 μs** |
| **Benchmark** |  **19** |      **True** |   **690,794.4 μs** |  **5,494.15 μs** |  **5,139.23 μs** |
| **Benchmark** |  **20** |     **False** |   **118,895.5 μs** |    **260.48 μs** |    **230.91 μs** |
| **Benchmark** |  **20** |      **True** |    **12,892.8 μs** |     **35.90 μs** |     **33.58 μs** |
