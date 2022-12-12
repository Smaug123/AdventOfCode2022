``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


```
|    Method | Day | IsPartOne |          Mean |       Error |      StdDev |
|---------- |---- |---------- |--------------:|------------:|------------:|
| **Benchmark** |   **1** |     **False** |     **32.141 μs** |   **0.2322 μs** |   **0.2058 μs** |
| **Benchmark** |   **1** |      **True** |     **32.234 μs** |   **0.1053 μs** |   **0.0879 μs** |
| **Benchmark** |   **2** |     **False** |     **81.493 μs** |   **0.4374 μs** |   **0.3878 μs** |
| **Benchmark** |   **2** |      **True** |     **80.254 μs** |   **0.3678 μs** |   **0.3441 μs** |
| **Benchmark** |   **3** |     **False** |     **32.799 μs** |   **0.6158 μs** |   **0.6845 μs** |
| **Benchmark** |   **3** |      **True** |     **67.518 μs** |   **0.4719 μs** |   **0.4415 μs** |
| **Benchmark** |   **4** |     **False** |     **67.787 μs** |   **0.7091 μs** |   **0.6633 μs** |
| **Benchmark** |   **4** |      **True** |     **54.713 μs** |   **0.4908 μs** |   **0.4098 μs** |
| **Benchmark** |   **5** |     **False** |     **92.630 μs** |   **0.6449 μs** |   **0.6032 μs** |
| **Benchmark** |   **5** |      **True** |     **80.029 μs** |   **0.7128 μs** |   **0.6667 μs** |
| **Benchmark** |   **6** |     **False** |     **87.383 μs** |   **1.5401 μs** |   **1.3653 μs** |
| **Benchmark** |   **6** |      **True** |    **168.588 μs** |   **1.6324 μs** |   **1.5269 μs** |
| **Benchmark** |   **7** |     **False** |    **455.791 μs** |   **4.1958 μs** |   **3.7195 μs** |
| **Benchmark** |   **7** |      **True** |    **470.090 μs** |   **8.8935 μs** |   **9.8851 μs** |
| **Benchmark** |   **8** |     **False** |    **770.817 μs** |   **9.3998 μs** |   **8.7926 μs** |
| **Benchmark** |   **8** |      **True** |    **371.655 μs** |   **5.7277 μs** |   **4.7829 μs** |
| **Benchmark** |   **9** |     **False** |    **507.394 μs** |   **8.8597 μs** |   **9.0983 μs** |
| **Benchmark** |   **9** |      **True** |    **933.275 μs** |   **6.8702 μs** |   **5.7369 μs** |
| **Benchmark** |  **10** |     **False** |      **7.589 μs** |   **0.1097 μs** |   **0.1026 μs** |
| **Benchmark** |  **10** |      **True** |      **8.606 μs** |   **0.0672 μs** |   **0.0629 μs** |
| **Benchmark** |  **11** |     **False** |      **7.400 μs** |   **0.0986 μs** |   **0.0923 μs** |
| **Benchmark** |  **11** |      **True** |  **2,908.736 μs** |  **25.5263 μs** |  **21.3156 μs** |
| **Benchmark** |  **12** |     **False** | **21,693.302 μs** | **166.2399 μs** | **155.5009 μs** |
| **Benchmark** |  **12** |      **True** | **23,433.128 μs** |  **51.6383 μs** |  **43.1203 μs** |