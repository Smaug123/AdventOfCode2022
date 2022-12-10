namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay10 =

    let input =
        """addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day10.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 13140

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day10.txt"

        Day10.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 15140

    [<Test>]
    let ``Part 2, given`` () =
        let output = ResizeArray ()
        Day10.part2 (StringSplitEnumerator.make '\n' input) |> Day10.render output.Add

        output
        |> List.ofSeq
        |> shouldEqual
            [
                "##..##..##..##..##..##..##..##..##..##.."
                "###...###...###...###...###...###...###."
                "####....####....####....####....####...."
                "#####.....#####.....#####.....#####....."
                "######......######......######......####"
                "#######.......#######.......#######....."
            ]

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day10.txt"
        let output = ResizeArray ()
        Day10.part2 (StringSplitEnumerator.make '\n' input) |> Day10.render output.Add

        output
        |> List.ofSeq
        |> shouldEqual
            [
                "###..###....##..##..####..##...##..###.."
                "#..#.#..#....#.#..#....#.#..#.#..#.#..#."
                "###..#..#....#.#..#...#..#....#..#.#..#."
                "#..#.###.....#.####..#...#.##.####.###.."
                "#..#.#....#..#.#..#.#....#..#.#..#.#...."
                "###..#.....##..#..#.####..###.#..#.#...."
            ]
