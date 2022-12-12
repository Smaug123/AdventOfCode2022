namespace AdventOfCode2022

open System.Collections.Generic
open System

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day12 =

    let private charToByte (c : char) = byte c - byte 'a'

    let parse (lines : StringSplitEnumerator) : byte[] ResizeArray * struct (byte * byte) * struct (byte * byte) =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable startPos = struct (0uy, 0uy)
        let mutable endPos = struct (0uy, 0uy)
        let mutable row = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let current = enum.Current.TrimEnd ()
                let arr = Array.zeroCreate current.Length

                for i in 0 .. arr.Length - 1 do
                    if current.[i] = 'S' then
                        startPos <- struct (byte i, byte row)
                        arr.[i] <- 0uy
                    elif current.[i] = 'E' then
                        endPos <- struct (byte i, byte row)
                        arr.[i] <- 25uy
                    else
                        arr.[i] <- charToByte current.[i]

                arr |> output.Add
                row <- row + 1

        output, startPos, endPos

    let part1 (lines : StringSplitEnumerator) : int64 =
        let data, start, endPoint = parse lines
        0L

    let part2 (lines : StringSplitEnumerator) : int64 =
        let data = parse lines
        0L
