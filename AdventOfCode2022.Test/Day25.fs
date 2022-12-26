namespace AdventOfCode2022.Test

open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022
open System

[<TestFixture>]
module TestDay25 =

    let input =
        """1=-0-2
12111
2=0=
21
2=01
111
20012
112
1=-1=
1-12
12
1=
122
"""

    [<Test>]
    let ``Part 1, given`` () =
        Day25.part1 (StringSplitEnumerator.make '\n' input) |> shouldEqual "2=-1=0"

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day25.txt"

        Day25.part1 (StringSplitEnumerator.make '\n' input)
        |> shouldEqual "2-0=11=-0-2-1==1=-22"

    let testCases =
        [
            1, "1"
            2, "2"
            3, "1="
            4, "1-"
            5, "10"
            6, "11"
            7, "12"
            8, "2="
            9, "2-"
            10, "20"
            15, "1=0"
            20, "1-0"
            2022, "1=11-2"
            12345, "1-0---0"
            314159265, "1121-1110-1=0"
            1747, "1=-0-2"
            906, "12111"
            198, "2=0="
            11, "21"
            201, "2=01"
            31, "111"
            1257, "20012"
            32, "112"
            353, "1=-1="
            107, "1-12"
            7, "12"
            3, "1="
            37, "122"
        ]
        |> List.map TestCaseData

    [<TestCaseSource(nameof testCases)>]
    let ``toInt works`` (int : int, str : string) =
        Day25.toInt (str.AsSpan ()) |> shouldEqual int

    [<TestCaseSource(nameof testCases)>]
    let ``ofInt works`` (int : int, str : string) = Day25.ofInt int |> shouldEqual str
