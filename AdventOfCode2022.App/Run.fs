namespace AdventOfCode2022.App

open AdventOfCode2022

[<RequireQualifiedAccess>]
module Run =
    let mutable shouldWrite = true

    let day1 (partTwo : bool) (input : string) =
        let lines = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day1.part1 lines

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day1.part2 lines

            if shouldWrite then
                printfn "%i" output

    let day2 (partTwo : bool) (input : string) =
        let lines = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day2.part1 lines

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day2.part2 lines

            if shouldWrite then
                printfn "%i" output

    let day3 (partTwo : bool) (input : string) =
        let lines = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day3Efficient.part1 lines

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day3Efficient.part2 (input.Split '\n')

            if shouldWrite then
                printfn "%i" output

    let day4 (partTwo : bool) (input : string) =
        let lines = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day4.part1 lines

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day4.part2 lines

            if shouldWrite then
                printfn "%i" output

    let day5 (partTwo : bool) (input : string) =
        let lines = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day5.part1 lines

            if shouldWrite then
                printfn "%s" output
        else
            let output = Day5.part2 lines

            if shouldWrite then
                printfn "%s" output

    let day6 (partTwo : bool) (input : string) =
        if not partTwo then
            let output = Day6.part1 input

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day6.part2 input

            if shouldWrite then
                printfn "%i" output

    let day7 (partTwo : bool) (input : string) =
        let day7 = input.Split '\n'

        if not partTwo then
            let output = Day7.part1 day7

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day7.part2 day7

            if shouldWrite then
                printfn "%i" output

    let day8 (partTwo : bool) (input : string) =
        let day8 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day8.part1 day8

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day8.part2 day8

            if shouldWrite then
                printfn "%i" output

    let day9 (partTwo : bool) (input : string) =
        let day9 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day9.part1 day9

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day9.part2 day9

            if shouldWrite then
                printfn "%i" output

    let day10 (partTwo : bool) (input : string) =
        let day10 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day10.part1 day10

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day10.part2 day10

            if shouldWrite then
                Day10.render (printfn "%s") output

    let day11 (partTwo : bool) (input : string) =
        let day11 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day11.part1 day11

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day11.part2 day11

            if shouldWrite then
                printfn "%i" output


    let day12 (partTwo : bool) (input : string) =
        let day12 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day12.part1 day12

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day12.part2 day12

            if shouldWrite then
                printfn "%i" output


    let day13 (partTwo : bool) (input : string) =
        let day13 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day13.part1 day13

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day13.part2 day13

            if shouldWrite then
                printfn "%i" output


    let day14 (partTwo : bool) (input : string) =
        let day14 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day14.part1 day14

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day14.part2 day14

            if shouldWrite then
                printfn "%i" output


    let day15 (partTwo : bool) (input : string) =
        let day15 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day15.part1 2000000 day15

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day15.part2 4000000 day15

            if shouldWrite then
                printfn "%i" output

    let day16 (partTwo : bool) (input : string) =
        let day16 = input.Split '\n'

        if not partTwo then
            let output = Day16.part1 day16

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day16.part2 day16

            if shouldWrite then
                printfn "%i" output

    let day17 (partTwo : bool) (input : string) =
        let day17 = input.TrimEnd ()

        if not partTwo then
            let output = Day17.part1 day17

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day17.part2 day17

            if shouldWrite then
                printfn "%i" output

    let day18 (partTwo : bool) (input : string) =
        let day18 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day18.part1 day18

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day18.part2 day18

            if shouldWrite then
                printfn "%i" output

    let day19 (partTwo : bool) (input : string) =
        let day19 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day19.part1 day19

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day19.part2 day19

            if shouldWrite then
                printfn "%i" output

    let day20 (partTwo : bool) (input : string) =
        let day20 = StringSplitEnumerator.make '\n' input

        if not partTwo then
            let output = Day20.part1 day20

            if shouldWrite then
                printfn "%i" output
        else
            let output = Day20.part2 day20

            if shouldWrite then
                printfn "%i" output

    let allRuns =
        [|
            day1
            day2
            day3
            day4
            day5
            day6
            day7
            day8
            day9
            day10
            day11
            day12
            day13
            day14
            day15
            day16
            day17
            day18
            day19
            day20
        |]
