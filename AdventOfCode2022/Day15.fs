namespace AdventOfCode2022

open System

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day15 =

    let parse (lines : StringSplitEnumerator) : int ResizeArray =
        use mutable enum = lines
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let line = enum.Current.TrimEnd ()
                output.Add line

        output

    let part1 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        0

    let part2 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        0
