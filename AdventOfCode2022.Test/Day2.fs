namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay2 =

    [<Test>]
    let ``Part 1, given`` () =
        """A Y
B X
C Z"""
        |> fun s -> s.Split System.Environment.NewLine
        |> Day2.part1
        |> shouldEqual 15

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day2.txt"
        input.Split '\n' |> Day2.part1 |> shouldEqual 9651


    [<Test>]
    let ``Part 2, given`` () =
        """A Y
B X
C Z"""
        |> fun s -> s.Split System.Environment.NewLine
        |> Day2.part2
        |> shouldEqual 12

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day2.txt"
        input.Split '\n' |> Day2.part2 |> shouldEqual 10560
