namespace AdventOfCode2022

open System
open System.Collections.Generic
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#else
#nowarn "9"
#endif


[<RequireQualifiedAccess>]
module Day18 =

    /// Returns the nodes, and also the "AA" node.
    let parse (line : string) : Direction array =
        Array.init
            line.Length
            (fun i ->
                match line.[i] with
                | '<' -> Direction.Left
                | '>' -> Direction.Right
                | c -> failwithf "unexpected char %c" c
            )

    let part1 (line : string) : int =
        let directions = parse line

        1

    let part2 (line : string) : int64 = -1L
