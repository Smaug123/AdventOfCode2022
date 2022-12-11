namespace AdventOfCode2022.Test

open AdventOfCode2022
open NUnit.Framework
open FsUnitTyped

[<TestFixture>]
module TestDay1 =

    let testInput =
        """1000
2000
3000

4000

5000
6000

7000
8000
9000

10000
"""

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day1.txt"
        Day1.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 66306

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day1.txt"
        Day1.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 195292

    [<Test>]
    let ``Part 1, given example`` () =
        Day1.part1 (StringSplitEnumerator.make '\n' testInput) |> shouldEqual 24000

    [<Test>]
    let ``Part 2, given example`` () =
        Day1.part2 (StringSplitEnumerator.make '\n' testInput) |> shouldEqual 45000
