namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay23 =

    let input =
        """....#..
..###.#
#...#.#
.#...##
#.###..
##.#.##
.#..#..
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day23.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 110

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day23.txt"

        Day23.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 4025


    [<Test>]
    let ``Part 2, given`` () =
        Day23.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 20

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day23.txt"

        Day23.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 935
