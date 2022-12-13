namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay13 =

    let input =
        """[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day13.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 13

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day13.txt"

        Day13.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 5185


    [<Test>]
    let ``Part 2, given`` () =
        Day13.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 140

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day13.txt"
        Day13.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 23751
