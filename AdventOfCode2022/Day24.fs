namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day24 =

    let parse (line : StringSplitEnumerator) : Coordinate HashSet =
        use mutable enum = line.GetEnumerator ()
        let output = HashSet ()
        let mutable y = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable x = 0

                for c in enum.Current.TrimEnd () do
                    if c = '#' then
                        output.Add
                            {
                                X = x
                                Y = y
                            }
                        |> ignore

                    x <- x + 1

                y <- y + 1

        output

    let part1 (lines : StringSplitEnumerator) : int =
        let board = parse lines
        -1

    let part2 (lines : StringSplitEnumerator) : int =
        let board = parse lines
        -1
