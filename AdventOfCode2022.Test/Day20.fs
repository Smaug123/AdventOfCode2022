namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay20 =

    let input =
        """1
2
-3
3
-2
0
4
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day20.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 3

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day20.txt"

        Day20.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 7225


    [<Test>]
    let ``Part 2, given`` () =
        Day20.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 1623178306L

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day20.txt"

        Day20.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 548634267428L
