namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay16 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day16.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 26

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day16.txt"

        Day16.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 6275922


    [<Test>]
    let ``Part 2, given`` () =
        Day16.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 56000011

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day16.txt"

        Day16.part2 (StringSplitEnumerator.make '\n' input)
        |> shouldEqual 11747175442119L
