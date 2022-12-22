namespace AdventOfCode2022

open System

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day23 =

    let parse (line : StringSplitEnumerator) : ResizeArray<int> =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        while enum.MoveNext () do
            output.Add 0

        output

    let part1 (lines : StringSplitEnumerator) : int =
        let board = parse lines

        -1

    let part2 (lines : StringSplitEnumerator) : int =
        let board = parse lines

        -1
