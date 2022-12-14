namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay15 =

    let input =
        """
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day15.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 24

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day15.txt"

        Day15.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 768


    [<Test>]
    let ``Part 2, given`` () =
        Day15.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 93

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day15.txt"
        Day15.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 26686
