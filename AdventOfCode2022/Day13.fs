namespace AdventOfCode2022

open System

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day13 =

    let parse (lines : StringSplitEnumerator) : int =
        use mutable enum = lines
        let output = ResizeArray ()

        while enum.MoveNext () do
            output.Add ()

        0

    let part1 (lines : StringSplitEnumerator) : int =
        let data = parse lines
        -1

    let part2 (lines : StringSplitEnumerator) : int =
        let data = parse lines
        -1
