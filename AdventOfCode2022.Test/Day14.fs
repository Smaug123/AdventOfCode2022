namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay14 =

    let input =
        """498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day14.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 24

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day14.txt"

        Day14.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 768


    [<Test>]
    let ``Part 2, given`` () =
        Day14.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 93

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day14.txt"
        Day14.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 26686
