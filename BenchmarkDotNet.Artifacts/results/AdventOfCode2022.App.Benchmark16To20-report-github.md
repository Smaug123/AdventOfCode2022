``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |         Mean |      Error |      StdDev |
|---------- |---- |---------- |-------------:|-----------:|------------:|
| **Benchmark** |  **16** |     **False** | **4,825.261 ms** | **96.3610 ms** | **183.3368 ms** |
| **Benchmark** |  **16** |      **True** | **1,204.199 ms** | **11.6398 ms** |  **10.8879 ms** |
| **Benchmark** |  **17** |     **False** |     **4.928 ms** |  **0.1057 ms** |   **0.3049 ms** |
| **Benchmark** |  **17** |      **True** |     **1.875 ms** |  **0.0363 ms** |   **0.0446 ms** |
