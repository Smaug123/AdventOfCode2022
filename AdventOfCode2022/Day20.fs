namespace AdventOfCode2022

open System
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day20 =

    //Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 4 ore. Each obsidian robot costs 4 ore and 8 clay. Each geode robot costs 2 ore and 18 obsidian.

    let parse (line : StringSplitEnumerator) : int ResizeArray =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                ()

        output

    let part1 (line : StringSplitEnumerator) : int =
        let blueprints = parse line

        -1

    let part2 (line : StringSplitEnumerator) : int =
        let blueprints = parse line

        -1
