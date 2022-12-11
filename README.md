# AdventOfCode2022

Advent of Code 2022, in F#.
Just `dotnet build` and `dotnet test`.

## Perf

As of Day 11:

```
|    Method | IsPartOne | Day |         Mean |      Error |     StdDev |
|---------- |---------- |---- |-------------:|-----------:|-----------:|
| Benchmark |     False |   1 |    32.663 us |  0.4192 us |  0.3921 us |
| Benchmark |     False |   2 |    82.502 us |  0.4576 us |  0.4281 us |
| Benchmark |     False |   3 |    33.273 us |  0.5546 us |  0.5188 us |
| Benchmark |     False |   4 |    66.689 us |  0.3763 us |  0.2938 us |
| Benchmark |     False |   5 |    96.317 us |  1.4445 us |  1.3511 us |
| Benchmark |     False |   6 |    90.087 us |  0.7930 us |  0.6622 us |
| Benchmark |     False |   7 |   466.332 us |  5.0471 us |  4.7211 us |
| Benchmark |     False |   8 |   759.873 us |  4.3523 us |  4.0712 us |
| Benchmark |     False |   9 |   507.427 us |  5.8451 us |  5.4675 us |
| Benchmark |     False |  10 |     7.615 us |  0.0300 us |  0.0280 us |
| Benchmark |     False |  11 |    12.617 us |  0.0716 us |  0.0670 us |
| Benchmark |      True |   1 |    33.206 us |  0.1680 us |  0.1572 us |
| Benchmark |      True |   2 |    80.874 us |  0.3673 us |  0.3436 us |
| Benchmark |      True |   3 |    72.505 us |  0.8570 us |  0.8016 us |
| Benchmark |      True |   4 |    56.584 us |  0.5950 us |  0.5565 us |
| Benchmark |      True |   5 |    84.942 us |  0.4420 us |  0.4135 us |
| Benchmark |      True |   6 |   167.142 us |  1.0515 us |  0.9836 us |
| Benchmark |      True |   7 |   454.487 us |  3.4531 us |  2.8835 us |
| Benchmark |      True |   8 |   370.147 us |  2.0985 us |  1.9630 us |
| Benchmark |      True |   9 |   938.836 us | 10.7999 us |  9.0184 us |
| Benchmark |      True |  10 |     8.446 us |  0.0685 us |  0.0641 us |
| Benchmark |      True |  11 | 3,768.481 us | 15.3375 us | 14.3467 us |
```
