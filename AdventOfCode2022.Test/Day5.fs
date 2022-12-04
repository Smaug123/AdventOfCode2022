namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay5 =

    let testInput = """

"""

    [<Test>]
    let ``Part 1, given`` () =
        testInput.Split Environment.NewLine
        |> Day5.part1
        |> shouldEqual 2

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day5.txt"

        input.Split '\n'
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Day5.part1
        |> shouldEqual 433


    [<Test>]
    let ``Part 2, given`` () =
        testInput.Split Environment.NewLine
        |> Day4.part2
        |> shouldEqual 4

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day5.txt"

        input.Split '\n'
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Day5.part2
        |> shouldEqual 852
