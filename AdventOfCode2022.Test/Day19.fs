namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay19 =

    let input =
        """Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day19.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 33

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day19.txt"

        Day19.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual 2341


    [<Test>]
    let ``Part 2, given`` () =
        Day19.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual (62 * 56)

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day19.txt"

        Day19.part2 (StringSplitEnumerator.make '\n' input) |> shouldEqual 3689
