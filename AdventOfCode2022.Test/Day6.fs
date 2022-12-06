namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay6 =

    let testInput1 =
        [
            "mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7
            "bvwbjplbgvbhsrlpgdmjqwftvncz", 5
            "nppdvjthqldpwncqszvftbrmjlhg", 6
            "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10
            "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11
        ]
        |> List.map TestCaseData

    [<TestCaseSource(nameof (testInput1))>]
    let ``Part 1, given`` (input : string, output : int) = Day6.part1 input |> shouldEqual output

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day6.txt"

        Day6.part1 (input.TrimEnd ()) |> shouldEqual 1544

    let part2Data =
        [
            "bvwbjplbgvbhsrlpgdmjqwftvncz", 23
            "nppdvjthqldpwncqszvftbrmjlhg", 23
            "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29
            "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26
        ]
        |> List.map TestCaseData

    [<TestCaseSource(nameof (part2Data))>]
    let ``Part 2, given`` (input : string, output : int) = Day6.part2 input |> shouldEqual output

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day6.txt"
        Day6.part2 input |> shouldEqual 2145
