namespace AdventOfCode2022

open System
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#else
#nowarn "9"
#endif


[<RequireQualifiedAccess>]
module Day17 =

    /// Returns the nodes, and also the "AA" node.
    let parse (line : string) : Direction array =
        Array.init line.Length (fun i ->
            match line.[i] with
            | '<' -> Direction.Left
            | '>' -> Direction.Right
            | c -> failwithf "unexpected char %c" c
        )

    let part1 (line : string) : int =
        let directions = parse line
        0

    let part2 (line : string) : int =
        let directions = parse line
        0
