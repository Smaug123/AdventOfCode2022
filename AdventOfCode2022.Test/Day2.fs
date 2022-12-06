namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay2 =

    let testInput =
        """A Y
B X
C Z"""

    [<Test>]
    let ``Part 1, given`` () =
        Day2.part1 (StringSplitEnumerator.make '\n' testInput) |> shouldEqual 15

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day2.txt"
        Day2.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 9651


    [<Test>]
    let ``Part 2, given`` () =
        Day2.part2 (StringSplitEnumerator.make '\n' testInput) |> shouldEqual 12

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day2.txt"
        Day2.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 10560
