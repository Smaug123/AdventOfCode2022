namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay15 =

    let input =
        """Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day15.part1 10 (StringSplitEnumerator.make '\n' input) |> shouldEqual 26

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day15.txt"

        Day15.part1 2000000 (StringSplitEnumerator.make '\n' input)
        |> shouldEqual 6275922


    [<Test>]
    let ``Part 2, given`` () =
        Day15.part2 20 (StringSplitEnumerator.make '\n' input) |> shouldEqual 56000011

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day15.txt"

        Day15.part2 4000000 (StringSplitEnumerator.make '\n' input)
        |> shouldEqual 11747175442119L
