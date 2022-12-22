namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay22 =

    let input =
        """        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day22.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 6032

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day22.txt"

        Day22.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0


    [<Test>]
    let ``Part 2, given`` () =
        Day22.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day22.txt"

        Day22.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0
