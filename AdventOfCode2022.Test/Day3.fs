namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay3 =

    [<Test>]
    let ``Part 1, given`` () =
        """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"""
        |> fun s -> s.Split System.Environment.NewLine
        |> Day3.part1
        |> shouldEqual 157

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day3.txt"

        input.Split '\n'
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Day3.part1
        |> shouldEqual 9651


    [<Test>]
    let ``Part 2, given`` () =
        """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"""
        |> fun s -> s.Split System.Environment.NewLine
        |> Day3.part2
        |> shouldEqual 8018

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day3.txt"

        input.Split '\n'
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Day3.part2
        |> shouldEqual 2518
