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

    let inline swap< ^T> (arr : 'T[]) (p1 : int) (p2 : int) =
        let tmp = arr.[p1]
        arr.[p1] <- arr.[p2]
        arr.[p2] <- tmp

    let inline performPart1Round< ^T>
        (modulo : 'T -> int -> int)
        (original : 'T[])
        (currentValues : 'T[])
        (currentLayout : int[])
        =
        for i in 0 .. original.Length - 1 do
            let currentLocation = Array.IndexOf<_> (currentLayout, i)

            let modulus : int = currentLayout.Length - 1

            let moveBy = ((modulo original.[i] modulus) + modulus) % modulus

            let newPos = (currentLocation + moveBy) % modulus

            if newPos > currentLocation then
                for j = currentLocation to newPos - 1 do
                    swap currentValues j (j + 1)
                    swap currentLayout j (j + 1)

            elif newPos <> currentLocation then
                for j = currentLocation downto newPos + 1 do
                    swap currentValues j (j - 1)
                    swap currentLayout j (j - 1)

    let part1 (lines : StringSplitEnumerator) : int =
        let original = parse lines
        let currentValues = Array.init original.Length (fun i -> original.[i])
        let currentLayout = Array.init original.Length id

        performPart1Round (%) original currentValues currentLayout

        let zeroIndex = Array.IndexOf<_> (currentValues, 0)

        currentValues.[(zeroIndex + 1000) % currentValues.Length]
        + currentValues.[(zeroIndex + 2000) % currentValues.Length]
        + currentValues.[(zeroIndex + 3000) % currentValues.Length]

    let part2 (lines : StringSplitEnumerator) : int64 =
        let original = parse lines |> Array.map (fun i -> 811589153L * int64 i)

        let currentValues = Array.init original.Length (fun i -> original.[i])
        let currentLayout = Array.init original.Length id

        for _ = 1 to 10 do
            performPart1Round (fun i m -> int (i % int64 m)) original currentValues currentLayout

        let zeroIndex = Array.IndexOf<_> (currentValues, 0)

        currentValues.[(zeroIndex + 1000) % currentValues.Length]
        + currentValues.[(zeroIndex + 2000) % currentValues.Length]
        + currentValues.[(zeroIndex + 3000) % currentValues.Length]
