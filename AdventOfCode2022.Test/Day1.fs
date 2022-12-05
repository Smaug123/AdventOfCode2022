namespace AdventOfCode2022.Test

open System
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
        input.Split '\n' |> Day1.part1 |> shouldEqual 66306

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day1.txt"
        input.Split '\n' |> Day1.part2 |> shouldEqual 195292

    [<Test>]
    let ``Part 1, given example`` () =
        testInput.Split Environment.NewLine |> Day1.part1 |> shouldEqual 24000


    [<Test>]
    let ``Part 2, given example`` () =
        testInput.Split Environment.NewLine |> Day1.part2 |> shouldEqual 45000
