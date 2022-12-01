namespace AdventOfCode2022

open System

[<RequireQualifiedAccess>]
module Day1 =

    let counts (lines : string seq) : int list =
        ((0, []), lines)
        ||> Seq.fold (fun (acc, counts) line ->
            if String.IsNullOrWhiteSpace line then
                0, (acc :: counts)
            else
                match Int32.TryParse line with
                | false, _ -> failwithf "not an int: %s" line
                | true, v -> acc + v, counts
        )
        |> snd

    /// Expects a trailing newline (as is present in the given input data).
    let part1 (lines : string seq) : int = lines |> counts |> List.max

    /// I wanted to save cloning the entire seq, so here is some bonus efficiency.
    let maxThree (inputs : int list) : struct (int * int * int) =
        ((struct (Int32.MinValue, Int32.MinValue, Int32.MinValue)), inputs)
        ||> List.fold (fun (struct (max1, max2, max3) as maxes) input ->
            if input <= max3 then maxes
            elif input <= max2 then struct (max1, max2, input)
            elif input <= max1 then struct (max1, input, max2)
            else struct (input, max1, max2)
        )

    let part2 (lines : string seq) : int =
        let struct (a, b, c) = lines |> counts |> maxThree

        a + b + c
