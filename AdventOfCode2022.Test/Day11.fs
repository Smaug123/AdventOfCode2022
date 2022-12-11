namespace AdventOfCode2022.Test

open System
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
        let allCounts = ResizeArray<int list list> ()
        let addToCounts (counts : int list seq) : unit = counts |> Seq.toList |> allCounts.Add

        Day11.part1 addToCounts (StringSplitEnumerator.make '\n' input)
        |> shouldEqual 10605

        let allCounts = Seq.toList allCounts

        allCounts.[0]
        |> shouldEqual [ [ 20 ; 23 ; 27 ; 26 ] ; [ 2080 ; 25 ; 167 ; 207 ; 401 ; 1046 ] ; [] ; [] ]

        allCounts.[1]
        |> shouldEqual [ [ 695 ; 10 ; 71 ; 135 ; 350 ] ; [ 43 ; 49 ; 58 ; 55 ; 362 ] ; [] ; [] ]

        allCounts.[2]
        |> shouldEqual [ [ 16 ; 18 ; 21 ; 20 ; 122 ] ; [ 1468 ; 22 ; 150 ; 286 ; 739 ] ; [] ; [] ]

        allCounts.[3]
        |> shouldEqual [ [ 491 ; 9 ; 52 ; 97 ; 248 ; 34 ] ; [ 39 ; 45 ; 43 ; 258 ] ; [] ; [] ]

        allCounts.[4]
        |> shouldEqual [ [ 15 ; 17 ; 16 ; 88 ; 1037 ] ; [ 20 ; 110 ; 205 ; 524 ; 72 ] ; [] ; [] ]

        allCounts.[5]
        |> shouldEqual [ [ 8 ; 70 ; 176 ; 26 ; 34 ] ; [ 481 ; 32 ; 36 ; 186 ; 2190 ] ; [] ; [] ]

        allCounts.[6]
        |> shouldEqual [ [ 162 ; 12 ; 14 ; 64 ; 732 ; 17 ] ; [ 148 ; 372 ; 55 ; 72 ] ; [] ; [] ]

        allCounts.[7]
        |> shouldEqual [ [ 51 ; 126 ; 20 ; 26 ; 136 ] ; [ 343 ; 26 ; 30 ; 1546 ; 36 ] ; [] ; [] ]

        allCounts.[8]
        |> shouldEqual [ [ 116 ; 10 ; 12 ; 517 ; 14 ] ; [ 108 ; 267 ; 43 ; 55 ; 288 ] ; [] ; [] ]

        allCounts.[9]
        |> shouldEqual [ [ 91 ; 16 ; 20 ; 98 ] ; [ 481 ; 245 ; 22 ; 26 ; 1092 ; 30 ] ; [] ; [] ]

        allCounts.[14]
        |> shouldEqual [ [ 83 ; 44 ; 8 ; 184 ; 9 ; 20 ; 26 ; 102 ] ; [ 110 ; 36 ] ; [] ; [] ]

        allCounts.[19]
        |> shouldEqual [ [ 10 ; 12 ; 14 ; 26 ; 34 ] ; [ 245 ; 93 ; 53 ; 199 ; 115 ] ; [] ; [] ]

    [<Test>]
    let ``Part 1, single round, given`` () =
        let monkeys = Day11.parse (StringSplitEnumerator.make '\n' input)
        let output = ResizeArray<string> ()
        let inspections = Array.zeroCreate<int> monkeys.Count

        Day11.oneRound output.Add monkeys inspections

        output
        |> String.concat Environment.NewLine
        |> shouldEqual
            """Monkey 0:
  Monkey inspects an item with a worry level of 79.
    Worry level is multiplied by 19 to 1501.
    Monkey gets bored with item. Worry level is divided by 3 to 500.
    Current worry level is not divisible by 23.
    Item with worry level 500 is thrown to monkey 3.
  Monkey inspects an item with a worry level of 98.
    Worry level is multiplied by 19 to 1862.
    Monkey gets bored with item. Worry level is divided by 3 to 620.
    Current worry level is not divisible by 23.
    Item with worry level 620 is thrown to monkey 3.
Monkey 1:
  Monkey inspects an item with a worry level of 54.
    Worry level increases by 6 to 60.
    Monkey gets bored with item. Worry level is divided by 3 to 20.
    Current worry level is not divisible by 19.
    Item with worry level 20 is thrown to monkey 0.
  Monkey inspects an item with a worry level of 65.
    Worry level increases by 6 to 71.
    Monkey gets bored with item. Worry level is divided by 3 to 23.
    Current worry level is not divisible by 19.
    Item with worry level 23 is thrown to monkey 0.
  Monkey inspects an item with a worry level of 75.
    Worry level increases by 6 to 81.
    Monkey gets bored with item. Worry level is divided by 3 to 27.
    Current worry level is not divisible by 19.
    Item with worry level 27 is thrown to monkey 0.
  Monkey inspects an item with a worry level of 74.
    Worry level increases by 6 to 80.
    Monkey gets bored with item. Worry level is divided by 3 to 26.
    Current worry level is not divisible by 19.
    Item with worry level 26 is thrown to monkey 0.
Monkey 2:
  Monkey inspects an item with a worry level of 79.
    Worry level is multiplied by itself to 6241.
    Monkey gets bored with item. Worry level is divided by 3 to 2080.
    Current worry level is divisible by 13.
    Item with worry level 2080 is thrown to monkey 1.
  Monkey inspects an item with a worry level of 60.
    Worry level is multiplied by itself to 3600.
    Monkey gets bored with item. Worry level is divided by 3 to 1200.
    Current worry level is not divisible by 13.
    Item with worry level 1200 is thrown to monkey 3.
  Monkey inspects an item with a worry level of 97.
    Worry level is multiplied by itself to 9409.
    Monkey gets bored with item. Worry level is divided by 3 to 3136.
    Current worry level is not divisible by 13.
    Item with worry level 3136 is thrown to monkey 3.
Monkey 3:
  Monkey inspects an item with a worry level of 74.
    Worry level increases by 3 to 77.
    Monkey gets bored with item. Worry level is divided by 3 to 25.
    Current worry level is not divisible by 17.
    Item with worry level 25 is thrown to monkey 1.
  Monkey inspects an item with a worry level of 500.
    Worry level increases by 3 to 503.
    Monkey gets bored with item. Worry level is divided by 3 to 167.
    Current worry level is not divisible by 17.
    Item with worry level 167 is thrown to monkey 1.
  Monkey inspects an item with a worry level of 620.
    Worry level increases by 3 to 623.
    Monkey gets bored with item. Worry level is divided by 3 to 207.
    Current worry level is not divisible by 17.
    Item with worry level 207 is thrown to monkey 1.
  Monkey inspects an item with a worry level of 1200.
    Worry level increases by 3 to 1203.
    Monkey gets bored with item. Worry level is divided by 3 to 401.
    Current worry level is not divisible by 17.
    Item with worry level 401 is thrown to monkey 1.
  Monkey inspects an item with a worry level of 3136.
    Worry level increases by 3 to 3139.
    Monkey gets bored with item. Worry level is divided by 3 to 1046.
    Current worry level is not divisible by 17.
    Item with worry level 1046 is thrown to monkey 1."""

        inspections |> shouldEqual [| 2 ; 4 ; 3 ; 5 |]

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day11.txt"

        // 120736 is too high
        Day11.part1 ignore (StringSplitEnumerator.make '\n' input) |> shouldEqual 6023

    [<Test>]
    let ``Part 2, given 1`` () =
        Day11.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 1

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day11.txt"
        Day11.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 2533
