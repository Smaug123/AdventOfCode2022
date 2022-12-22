namespace AdventOfCode2022

open System

[<RequireQualifiedAccess>]
module Day6 =

    let inline findDuplicateSorted< ^a when ^a : equality> (a : 'a array) : int ValueOption =
        let mutable i = 0
        let mutable result = ValueNone

        while result.IsNone && i + 1 < a.Length do
            if a.[i] = a.[i + 1] then
                result <- ValueSome i

            i <- i + 1

        result

    let private swapFor (arr : char[]) (toRemove : char) (toInsert : char) : unit =
        let index = Array.IndexOf (arr, toRemove)

        if
            (index = 0 || arr.[index - 1] <= toInsert)
            && (index = arr.Length - 1 || toInsert <= arr.[index + 1])
        then
            arr.[index] <- toInsert
        else

        let mutable i = index
        let mutable notInserted = true

        if (index = 0 || arr.[index - 1] <= toInsert) then
            // Inserting above
            if toInsert > toRemove then
                while notInserted && i < arr.Length - 1 do
                    arr.[i] <- arr.[i + 1]
                    i <- i + 1

                    if i = arr.Length - 1 || toInsert <= arr.[i + 1] then
                        arr.[i] <- toInsert
                        notInserted <- false

                if notInserted then
                    arr.[arr.Length - 1] <- toInsert

        else
            // Inserting below
            while notInserted && i > 0 do
                arr.[i] <- arr.[i - 1]
                i <- i - 1

                if i = 0 || toInsert >= arr.[i - 1] then
                    arr.[i] <- toInsert
                    notInserted <- false

            if notInserted then
                arr.[0] <- toInsert

    let rec private go (arr : char[]) (count : int) (s : ReadOnlySpan<char>) (i : int) =
        if s.[i - count + 1] = s.[i + 1] then
            go arr count s (i + 1)
        else

        swapFor arr s.[i - count + 1] s.[i + 1]

        match findDuplicateSorted arr with
        | ValueSome _ -> go arr count s (i + 1)
        | ValueNone -> i + 2

    let part1 (line : string) : int =
        let length = 4
        let line = line.AsSpan ()
        let arr = line.Slice(0, length).ToArray ()
        Array.sortInPlace arr
        go arr length line (length - 1)

    let part2 (line : string) : int =
        let length = 14
        let line = line.AsSpan ()
        let arr = line.Slice(0, length).ToArray ()
        Array.sortInPlace arr
        go arr length line (length - 1)
