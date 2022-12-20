``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |           Mean |        Error |        StdDev |
|---------- |---- |---------- |---------------:|-------------:|--------------:|
| **Benchmark** |  **16** |     **False** | **3,095,380.2 μs** | **84,912.16 μs** | **250,365.43 μs** |
| **Benchmark** |  **16** |      **True** |   **327,650.0 μs** |    **912.56 μs** |     **712.46 μs** |
| **Benchmark** |  **17** |     **False** |     **3,038.3 μs** |     **14.24 μs** |      **13.32 μs** |
| **Benchmark** |  **17** |      **True** |     **1,557.7 μs** |      **4.21 μs** |       **3.73 μs** |
| **Benchmark** |  **18** |     **False** |    **42,873.0 μs** |     **63.68 μs** |      **53.18 μs** |
| **Benchmark** |  **18** |      **True** |       **131.4 μs** |      **0.35 μs** |       **0.29 μs** |
| **Benchmark** |  **19** |     **False** |   **561,661.4 μs** |    **920.21 μs** |     **815.75 μs** |
| **Benchmark** |  **19** |      **True** |   **682,697.4 μs** |  **3,253.31 μs** |   **3,043.15 μs** |
| **Benchmark** |  **20** |     **False** |   **117,615.9 μs** |    **393.47 μs** |     **368.06 μs** |
| **Benchmark** |  **20** |      **True** |    **12,723.1 μs** |     **18.14 μs** |      **16.08 μs** |
