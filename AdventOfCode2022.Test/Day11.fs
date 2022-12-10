namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay11 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day11.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 13

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day11.txt"

        Day11.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 6023

    [<Test>]
    let ``Part 2, given 1`` () =
        Day11.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 1

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day11.txt"
        Day11.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 2533
