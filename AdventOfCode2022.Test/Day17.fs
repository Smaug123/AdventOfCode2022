namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay17 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day17.part1 (input.Split '\n') |> shouldEqual 1651

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day17.txt"

        Day17.part1 (input.Split '\n') |> shouldEqual 1751


    [<Test>]
    let ``Part 2, given`` () =
        Day17.part2 (input.Split '\n') |> shouldEqual 1707

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day17.txt"

        Day17.part2 (input.Split '\n') |> shouldEqual 2207
