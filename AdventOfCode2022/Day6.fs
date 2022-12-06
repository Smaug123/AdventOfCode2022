namespace AdventOfCode2022

open System

[<RequireQualifiedAccess>]
module Day6 =

    let findDuplicateSorted (a : 'a array) : int ValueOption =
        let mutable i = 0
        let mutable result = ValueNone

        while result.IsNone && i + 1 < a.Length do
            if a.[i] = a.[i + 1] then
                result <- ValueSome i

            i <- i + 1

        result

    let rec private go (count : int) (s : ReadOnlySpan<char>) (i : int) =
        let fourChars = s.Slice (i - count + 1, count)
        let arr = fourChars.ToArray ()
        Array.sortInPlace arr

        match findDuplicateSorted arr with
        | ValueSome _ -> go count s (i + 1)
        | ValueNone -> i + 1

    let part1 (line : string) : int = go 4 (line.AsSpan ()) 3

    let part2 (line : string) : int = go 14 (line.AsSpan ()) 13
