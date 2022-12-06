namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay4 =

    let testInput =
        """2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"""

    [<Test>]
    let ``Part 1, given`` () =
        Day4.part1 (StringSplitEnumerator.make '\n' testInput) |> shouldEqual 2

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day4.txt"

        Day4.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 433

    [<Test>]
    let ``Part 2, given`` () =
        Day4.part2 (StringSplitEnumerator.make '\n' testInput) |> shouldEqual 4

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day4.txt"

        Day4.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 852
