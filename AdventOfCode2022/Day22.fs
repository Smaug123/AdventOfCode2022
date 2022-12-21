namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day22 =

    let parse (line : StringSplitEnumerator) : int ResizeArray =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                output.Add 0

        output

    let part1 (lines : StringSplitEnumerator) : int =
        let original = parse lines

        -1

    let part2 (lines : StringSplitEnumerator) : int =
        let original = parse lines

        -1
