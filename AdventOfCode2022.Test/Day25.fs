namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay25 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day25.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day25.txt"

        Day25.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0
