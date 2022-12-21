``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |     Mean |    Error |  StdDev |
|---------- |---- |---------- |---------:|---------:|--------:|
| **Benchmark** |  **21** |     **False** | **743.3 μs** | **10.59 μs** | **9.39 μs** |
| **Benchmark** |  **21** |      **True** | **721.6 μs** |  **1.83 μs** | **1.72 μs** |
