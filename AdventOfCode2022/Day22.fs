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

    /// Returns the board (where 0 means "nothing here", 1 means "space", 2 means "wall"),
    /// the set of instructions (move * change-direction), and the final trailing instruction to move.
    let parse (line : StringSplitEnumerator) : int[][] * ResizeArray<struct(int * Direction)> * int =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        let row = ResizeArray ()
        // 2 here in case there's a trailing \r
        while enum.MoveNext () && enum.Current.Length >= 2 do
            for i = 0 to enum.Current.Length - 1 do
                match enum.Current.[i] with
                | ' ' -> row.Add 0
                | '.' -> row.Add 1
                | '#' -> row.Add 2
                | '\r' -> ()
                | c -> failwithf "unexpected char: %c" c
            output.Add (row.ToArray ())
            row.Clear ()

        if not (enum.MoveNext ()) then failwith "expected instruction line"
        let directions = ResizeArray ()
        let line = enum.Current.TrimEnd ()
        let mutable i = 0
        for count = 0 to line.Length - 1 do
            if '0' <= line.[count] && line.[count] <= '9' then
                i <- i * 10 + (int line.[count] - int '0')
            else
                let dir =
                    match line.[count] with
                    | 'L' -> Direction.Left
                    | 'R' -> Direction.Right
                    | c -> failwithf "Unexpected: %c" c
                directions.Add (struct (i, dir))
                i <- 0

        output.ToArray (), directions, i

    let part1 (lines : StringSplitEnumerator) : int =
        let board, instructions, finalDistance = parse lines

        -1

    let part2 (lines : StringSplitEnumerator) : int =
        let original = parse lines

        -1
