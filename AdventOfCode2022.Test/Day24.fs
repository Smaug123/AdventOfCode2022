namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay24 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day24.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day24.txt"

        Day24.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0


    [<Test>]
    let ``Part 2, given`` () =
        Day24.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day24.txt"

        Day24.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 0
