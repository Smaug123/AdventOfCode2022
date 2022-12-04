namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay4 =

    [<Test>]
    let ``Part 1, given`` () =
        """2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"""
        |> fun s -> s.Split System.Environment.NewLine
        |> Day4.part1
        |> shouldEqual 2

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day4.txt"

        input.Split '\n'
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Day4.part1
        |> shouldEqual 433


    [<Test>]
    let ``Part 2, given`` () =
        """2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"""
        |> fun s -> s.Split System.Environment.NewLine
        |> Day4.part2
        |> shouldEqual 4

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day4.txt"

        input.Split '\n'
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Day4.part2
        |> shouldEqual 852
