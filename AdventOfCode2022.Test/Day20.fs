namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay20 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day20.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 33

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day20.txt"

        Day20.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 2341


    [<Test>]
    let ``Part 2, given`` () =
        Day20.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 10

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day20.txt"

        Day20.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 3689
