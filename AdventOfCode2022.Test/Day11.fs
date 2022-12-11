namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay11 =

    let input =
        """Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day11.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 10605

    [<Test>]
    let ``Part 1, single round, given`` () =
        let monkeys = Day11.parse (StringSplitEnumerator.make '\n' input)
        let inspections = Array.zeroCreate monkeys.Length

        Day11.oneRoundDivThree monkeys inspections

        inspections |> shouldEqual [| 2 ; 4 ; 3 ; 5 |]

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day11.txt"

        Day11.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 120384


    [<Test>]
    let ``Part 2, given 1`` () =
        Day11.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 2713310158L

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day11.txt"
        Day11.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 32059801242L
