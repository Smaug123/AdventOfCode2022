namespace AdventOfCode2022

open System.Collections.Generic
open System

[<RequireQualifiedAccess>]
module Day11 =

    let parse (lines : StringSplitEnumerator) : (Direction * byte) IReadOnlyList =
        use mutable enum = lines
        let output = ResizeArray ()

        for line in enum do
            let line = line.TrimEnd ()

            if not (line.IsWhiteSpace ()) then
                let dir =
                    match Char.ToUpperInvariant line.[0] with
                    | 'U' -> Direction.Up
                    | 'D' -> Direction.Down
                    | 'L' -> Direction.Left
                    | 'R' -> Direction.Right
                    | _ -> failwith "Unexpected direction"

                let distance = Byte.Parse (line.Slice 2)

                output.Add (dir, distance)

        output :> _

    let part1 (lines : StringSplitEnumerator) : int =
        let directions = parse lines

        go 2 directions

    let part2 (lines : StringSplitEnumerator) : int =
        let directions = parse lines

        go 10 directions
