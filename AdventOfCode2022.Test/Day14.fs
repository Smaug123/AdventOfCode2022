namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay14 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day14.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 13

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day14.txt"

        Day14.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 5185


    [<Test>]
    let ``Part 2, given`` () =
        Day14.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 140

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day14.txt"
        Day14.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 23751
