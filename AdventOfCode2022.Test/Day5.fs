namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay5 =

    let testInput =
        """    [D]
[N] [C]
[Z] [M] [P]
 1   2   3

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2
"""

    [<Test>]
    let ``Part 1, given`` () =
        testInput.Split Environment.NewLine |> Day5.part1 |> shouldEqual "CMZ"

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day5.txt"

        input.Split '\n' |> Day5.part1 |> shouldEqual "BSDMQFLSP"


    [<Test>]
    let ``Part 2, given`` () =
        testInput.Split Environment.NewLine |> Day5.part2 |> shouldEqual "MCD"

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day5.txt"

        input.Split '\n' |> Day5.part2 |> shouldEqual "PGSQBFLDP"
