namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay17 =

    let input = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>"

    [<Test>]
    let ``Part 1, given`` () =
        Day17.part1 (input.TrimEnd ()) |> shouldEqual 3068

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day17.txt"

        Day17.part1 (input.TrimEnd ()) |> shouldEqual 3127


    [<Test>]
    let ``Part 2, given`` () =
        Day17.part2 (input.TrimEnd ()) |> shouldEqual 1514285714288L

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day17.txt"

        Day17.part2 (input.TrimEnd ()) |> shouldEqual 1542941176480L
