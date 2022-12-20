namespace AdventOfCode2022

open System

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day20 =

    let parse (line : StringSplitEnumerator) : int[] =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                output.Add (Int32.Parse enum.Current)

        output.ToArray ()

    [<Struct>]
    type Day20Entry =
        {
            OriginalPos : int
            Value : int
        }

    let inline clone< ^T when ^T : struct> (arr : ^T[]) : ^T[] =
        let newArr = Array.zeroCreate arr.Length
        Buffer.BlockCopy (arr, 0, newArr, 0, arr.Length * sizeof< ^T>)
        newArr

    let inline swapDown< ^T> (arr : 'T[]) (smaller : int) (bigger : int) : unit =
        let tmp = arr.[smaller]

        for j = smaller to bigger - 1 do
            arr.[j] <- arr.[j + 1]

        arr.[bigger] <- tmp

    let inline swapUp< ^T> (arr : 'T[]) (bigger : int) (smaller : int) : unit =
        let tmp = arr.[bigger]

        for j = bigger downto smaller + 1 do
            arr.[j] <- arr.[j - 1]

        arr.[smaller] <- tmp

    let inline performPart1Round (original : int[]) (currentValues : int[]) (currentLayout : int[]) =
        for i in 0 .. original.Length - 1 do
            let currentLocation = Array.IndexOf<_> (currentLayout, i, 0, currentLayout.Length)

            let modulus = currentLayout.Length - 1

            let moveBy = ((original.[i] % modulus) + modulus) % modulus

            let newPos = (currentLocation + moveBy) % modulus

            if newPos > currentLocation then
                swapDown currentValues currentLocation newPos
                swapDown currentLayout currentLocation newPos

            elif newPos <> currentLocation then
                swapUp currentValues currentLocation newPos
                swapUp currentLayout currentLocation newPos

    let part1 (lines : StringSplitEnumerator) : int =
        let original = parse lines
        let currentValues = clone original
        let currentLayout = Array.init original.Length id

        performPart1Round original currentValues currentLayout

        let zeroIndex = Array.IndexOf<_> (currentValues, 0)

        currentValues.[(zeroIndex + 1000) % currentValues.Length]
        + currentValues.[(zeroIndex + 2000) % currentValues.Length]
        + currentValues.[(zeroIndex + 3000) % currentValues.Length]

    let part2 (lines : StringSplitEnumerator) : int64 =
        let key = 811589153
        let original = parse lines
        let modded = original |> Array.map (fun i -> (key % (original.Length - 1)) * i)

        let currentValues = clone modded
        let currentLayout = Array.init modded.Length id

        for _ = 1 to 10 do
            performPart1Round modded currentValues currentLayout

        let zeroIndex = Array.IndexOf<_> (currentValues, 0)

        let result =
            let first = currentLayout.[(zeroIndex + 1000) % currentLayout.Length]
            let second = currentLayout.[(zeroIndex + 2000) % currentLayout.Length]
            let third = currentLayout.[(zeroIndex + 3000) % currentLayout.Length]

            original.[first] + original.[second] + original.[third]

        int64 result * int64 key
