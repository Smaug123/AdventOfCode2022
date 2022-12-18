namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay18 =

    let input1 =
        """1,1,1
2,1,1
"""

    let input2 =
        """2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day18.part1 (StringSplitEnumerator.make '\n' input1) |> shouldEqual 10
        Day18.part1 (StringSplitEnumerator.make '\n' input2) |> shouldEqual 64

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day18.txt"

        Day18.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 3542


    [<Test>]
    let ``Part 2, given`` () =
        Day18.part2 (StringSplitEnumerator.make '\n' input2) |> shouldEqual 58

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day18.txt"

        Day18.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 2080
