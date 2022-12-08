namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay8 =

    let input =
        """30373
25512
65332
33549
35390
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day8.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 21

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day8.txt"

        Day8.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 1823

    [<Test>]
    let ``Part 2, given`` () =
        Day8.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 8

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day8.txt"
        Day8.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 211680
