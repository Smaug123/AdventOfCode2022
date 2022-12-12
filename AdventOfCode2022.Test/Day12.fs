namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay12 =

    let input =
        """Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day12.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 31

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day12.txt"

        Day12.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 456


    [<Test>]
    let ``Part 2, given`` () =
        Day12.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 29

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day12.txt"
        Day12.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 454
