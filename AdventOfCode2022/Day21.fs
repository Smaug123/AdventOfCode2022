namespace AdventOfCode2022

open System

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day21 =

    let parse (line : StringSplitEnumerator) : int[] =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                output.Add (Int32.Parse enum.Current)

        output.ToArray ()

    let part1 (lines : StringSplitEnumerator) : int =
        let original = parse lines

        -1

    let part2 (lines : StringSplitEnumerator) : int =
        let original = parse lines

        -1
