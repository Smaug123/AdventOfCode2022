namespace AdventOfCode2022.Test

open System
open NUnit.Framework
open FsUnitTyped
open AdventOfCode2022

[<TestFixture>]
module TestDay7 =

    let input =
        """$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k
"""

    [<Test>]
    let ``Part 1, given`` () =
        input.Split '\n' |> Day7.part1 |> shouldEqual 95437

    [<Test>]
    let ``Part 1`` () =
        let input = Assembly.readResource "Day7.txt"

        Day7.part1 (input.Split '\n') |> shouldEqual 1886043

    [<Test>]
    let ``Part 2, given`` () =
        Day7.part2 (input.Split '\n') |> shouldEqual 24933642

    [<Test>]
    let ``Part 2`` () =
        let input = Assembly.readResource "Day7.txt"
        Day7.part2 (input.Split '\n') |> shouldEqual 2145
