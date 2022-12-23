``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |         Mean |     Error |    StdDev |
|---------- |---- |---------- |-------------:|----------:|----------:|
| **Benchmark** |  **21** |     **False** |     **649.0 μs** |   **4.46 μs** |   **3.96 μs** |
| **Benchmark** |  **21** |      **True** |     **584.9 μs** |   **4.29 μs** |   **3.80 μs** |
| **Benchmark** |  **22** |     **False** |     **331.7 μs** |   **2.26 μs** |   **2.12 μs** |
| **Benchmark** |  **22** |      **True** |     **220.6 μs** |   **0.70 μs** |   **0.62 μs** |
| **Benchmark** |  **23** |     **False** | **346,889.4 μs** | **900.55 μs** | **798.31 μs** |
| **Benchmark** |  **23** |      **True** |   **3,128.1 μs** |   **4.91 μs** |   **4.35 μs** |
