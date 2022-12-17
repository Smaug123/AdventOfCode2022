namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay16 =

    let input =
        """Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II
"""

    [<Test>]
    let ``seq behaviour`` () =
        IntSet.ofSeq [ 1 ; 2 ; 3 ; 16 ]
        |> IntSet.toSeq
        |> List.ofSeq
        |> shouldEqual [ 1 ; 2 ; 3 ; 16 ]

    [<Test>]
    let ``Part 1, given`` () =
        Day16.part1 (input.Split '\n') |> shouldEqual 1651

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day16.txt"

        Day16.part1 (input.Split '\n') |> shouldEqual 1751


    [<Test>]
    let ``Part 2, given`` () =
        Day16.part2 (input.Split '\n') |> shouldEqual 1707

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day16.txt"

        Day16.part2 (input.Split '\n') |> shouldEqual 2207
