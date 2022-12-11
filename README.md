# AdventOfCode2022

Advent of Code 2022, in F#.
Just `dotnet build` and `dotnet test`.

## Perf

As of Day 11:

```
|    Method | IsPartOne | Day |         Mean |      Error |     StdDev |
|---------- |---------- |---- |-------------:|-----------:|-----------:|
| Benchmark |     False |   1 |    32.123 us |  0.1045 us |  0.0873 us |
| Benchmark |     False |   2 |    81.840 us |  0.2376 us |  0.2222 us |
| Benchmark |     False |   3 |    31.403 us |  0.6213 us |  0.9488 us |
| Benchmark |     False |   4 |    67.172 us |  0.5272 us |  0.4403 us |
| Benchmark |     False |   5 |    92.836 us |  0.5355 us |  0.4472 us |
| Benchmark |     False |   6 |    84.263 us |  0.5106 us |  0.3987 us |
| Benchmark |     False |   7 |   453.359 us |  2.0308 us |  1.5855 us |
| Benchmark |     False |   8 | 1,463.500 us | 11.5194 us | 10.2117 us |
| Benchmark |     False |   9 |   524.929 us |  9.5808 us | 11.4053 us |
| Benchmark |     False |  10 |     7.600 us |  0.0697 us |  0.0652 us |
| Benchmark |     False |  11 |    12.655 us |  0.1730 us |  0.1619 us |
| Benchmark |      True |   1 |    33.431 us |  0.4478 us |  0.4189 us |
| Benchmark |      True |   2 |    81.818 us |  0.6992 us |  0.6198 us |
| Benchmark |      True |   3 |    70.466 us |  1.4060 us |  2.1472 us |
| Benchmark |      True |   4 |    55.925 us |  0.5135 us |  0.4804 us |
| Benchmark |      True |   5 |    83.952 us |  1.2843 us |  1.2013 us |
| Benchmark |      True |   6 |   169.857 us |  0.9308 us |  0.8251 us |
| Benchmark |      True |   7 |   473.761 us |  7.5477 us |  7.0601 us |
| Benchmark |      True |   8 |   649.943 us |  3.7094 us |  3.2883 us |
| Benchmark |      True |   9 |   940.372 us |  5.2204 us |  4.8832 us |
| Benchmark |      True |  10 |     8.432 us |  0.0565 us |  0.0529 us |
| Benchmark |      True |  11 | 3,815.519 us | 50.7779 us | 47.4977 us |
```
