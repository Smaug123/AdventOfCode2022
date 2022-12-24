namespace AdventOfCode2022

open System
open Microsoft.FSharp.NativeInterop

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day25 =

    /// Returns the width and the height too. The resulting array is suitable to become an Arr2D.
    let parse (line : StringSplitEnumerator) : byte[] * int * int =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()
        let mutable y = 0
        let mutable width = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable x = 0

                for c in enum.Current.TrimEnd () do
                    match c with
                    | '>' -> output.Add 8uy
                    | '^' -> output.Add 1uy
                    | 'v' -> output.Add 2uy
                    | '<' -> output.Add 4uy
                    | '.'
                    | '#' -> output.Add 0uy
                    | _ -> failwithf "unexpected char: %c" c

                    x <- x + 1

                width <- x

                y <- y + 1

        output.ToArray (), width, y

    let part1 (lines : StringSplitEnumerator) : int =
        let board, width, height = parse lines
        -1
