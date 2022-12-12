# AdventOfCode2022

Advent of Code 2022, in F#.
Just `dotnet build` and `dotnet test`.

## Perf

As of Day 11:

```
BenchmarkDotNet=v0.13.2, OS=macOS 13.0.1 (22A400) [Darwin 22.1.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD DEBUG
  DefaultJob : .NET 7.0.0 (7.0.22.51805), Arm64 RyuJIT AdvSIMD


|    Method | Day | IsPartOne |             Mean |          Error |         StdDev |
|---------- |---- |---------- |-----------------:|---------------:|---------------:|
| Benchmark |   1 |     False |        32.603 us |      0.1156 us |      0.1025 us |
| Benchmark |   1 |      True |        32.631 us |      0.0869 us |      0.0771 us |
| Benchmark |   2 |     False |        82.653 us |      0.1401 us |      0.1170 us |
| Benchmark |   2 |      True |        80.848 us |      0.1771 us |      0.1657 us |
| Benchmark |   3 |     False |        32.453 us |      0.3211 us |      0.3004 us |
| Benchmark |   3 |      True |        73.870 us |      0.4218 us |      0.3945 us |
| Benchmark |   4 |     False |        68.849 us |      0.2764 us |      0.2585 us |
| Benchmark |   4 |      True |        56.220 us |      0.4178 us |      0.3262 us |
| Benchmark |   5 |     False |        96.159 us |      0.8042 us |      0.7129 us |
| Benchmark |   5 |      True |        82.477 us |      0.3252 us |      0.3042 us |
| Benchmark |   6 |     False |        87.074 us |      0.4153 us |      0.3885 us |
| Benchmark |   6 |      True |       171.234 us |      0.6468 us |      0.6050 us |
| Benchmark |   7 |     False |       469.857 us |      9.0091 us |      8.8482 us |
| Benchmark |   7 |      True |       467.825 us |      1.4685 us |      1.2263 us |
| Benchmark |   8 |     False |       766.520 us |      2.0220 us |      1.8914 us |
| Benchmark |   8 |      True |       381.438 us |      7.2321 us |      6.7649 us |
| Benchmark |   9 |     False |       521.262 us |      2.5656 us |      2.3999 us |
| Benchmark |   9 |      True |       946.909 us |      2.6168 us |      2.4478 us |
| Benchmark |  10 |     False |         7.632 us |      0.0200 us |      0.0177 us |
| Benchmark |  10 |      True |         8.494 us |      0.0227 us |      0.0201 us |
| Benchmark |  11 |     False |         7.377 us |      0.0141 us |      0.0117 us |
| Benchmark |  11 |      True |     2,929.034 us |     20.6688 us |     17.2594 us |
| Benchmark |  12 |     False |    26,970.013 us |     43.1843 us |     36.0608 us |
| Benchmark |  12 |      True | 4,996,237.958 us | 30,838.7917 us | 28,846.6248 us |
```
