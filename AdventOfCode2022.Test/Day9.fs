namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay9 =

    let input =
        """R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day9.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 13

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day9.txt"

        Day9.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 6023

    [<Test>]
    let ``Part 2, given 1`` () =
        Day9.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 1

    let example2 =
        """R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20
"""

    [<Test>]
    let ``Part 2, given 2`` () =
        Day9.part2 (StringSplitEnumerator.make '\n' example2) |> shouldEqual 36

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day9.txt"
        Day9.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 2533
