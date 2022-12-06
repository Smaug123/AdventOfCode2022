namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture false>]
[<TestFixture true>]
type TestDay3 (efficient : bool) =

    let part1 (s : string) =
        if efficient then
            Day3Efficient.part1 (StringSplitEnumerator.make '\n' s)
        else
            s.Split '\n' |> Seq.filter (not << String.IsNullOrEmpty) |> Day3.part1

    let part2 (s : string seq) =
        if efficient then Day3Efficient.part2 s else Day3.part2 s

    [<Test>]
    member _.``Part 1, given`` () =
        """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"""
        |> part1
        |> shouldEqual 157<Priority>

    [<Test>]
    member _.``Part 1`` () =
        let input = Assembly.readResource "Day3.txt"

        input |> part1 |> shouldEqual 8018<Priority>


    [<Test>]
    member _.``Part 2, given`` () =
        """vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"""
        |> fun s -> s.Split System.Environment.NewLine
        |> part2
        |> shouldEqual 70<Priority>

    [<Test>]
    member _.``Part 2`` () =
        let input = Assembly.readResource "Day3.txt"

        input.Split '\n'
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> part2
        |> shouldEqual 2518<Priority>
