namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay21 =

    let input =
        """root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day21.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 152L

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day21.txt"

        Day21.part1 (StringSplitEnumerator.make '\n' input)
        |> shouldEqual 54703080378102L


    [<Test>]
    let ``Part 2, given`` () =
        Day21.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 301L

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day21.txt"

        Day21.part2 (StringSplitEnumerator.make '\n' input)
        |> shouldEqual 3952673930912L
