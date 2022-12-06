namespace AdventOfCode2022

open System
open System.Collections.Generic

[<RequireQualifiedAccess>]
module Day1 =

    let counts (lines : byref<StringSplitEnumerator>) : int IReadOnlyList =
        let counts = ResizeArray ()
        let mutable acc = 0

        for line in lines do
            if line.IsWhiteSpace () then
                counts.Add acc
                acc <- 0
            else
                acc <- acc + Int32.Parse line

        if acc <> 0 then
            counts.Add acc

        counts

    /// Expects a trailing newline (as is present in the given input data).
    let part1 (lines : StringSplitEnumerator) : int =
        let mutable lines = lines
        counts &lines |> Seq.max

    /// I wanted to save cloning the entire seq, so here is some bonus efficiency.
    let maxThree (inputs : int IReadOnlyList) : struct (int * int * int) =
        ((struct (Int32.MinValue, Int32.MinValue, Int32.MinValue)), inputs)
        ||> Seq.fold (fun (struct (max1, max2, max3) as maxes) input ->
            if input <= max3 then maxes
            elif input <= max2 then struct (max1, max2, input)
            elif input <= max1 then struct (max1, input, max2)
            else struct (input, max1, max2)
        )

    let part2 (lines : StringSplitEnumerator) : int =
        let mutable lines = lines
        let struct (a, b, c) = counts &lines |> maxThree

        a + b + c
