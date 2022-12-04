namespace AdventOfCode2022

open System

[<RequireQualifiedAccess>]
module Day4 =

    /// This only checks if the first arg fully contains the second arg.
    let inline fullyContains (a, b) (c, d) : bool = a <= c && b >= d

    let inline overlaps (a, b) (c, d) : bool = b >= c && a <= d

    let parse (s : ReadOnlySpan<char>) : (int * int) * (int * int) =
        let commaIndex = s.IndexOf ','
        let firstElf = s.Slice (0, commaIndex)
        let secondElf = s.Slice (commaIndex + 1)
        let firstDashIndex = firstElf.IndexOf '-'
        let firstElf1 = firstElf.Slice (0, firstDashIndex)
        let firstElf2 = firstElf.Slice (firstDashIndex + 1)
        let secondDashIndex = secondElf.IndexOf '-'
        let secondElf1 = secondElf.Slice (0, secondDashIndex)
        let secondElf2 = secondElf.Slice (secondDashIndex + 1)
        (Int32.Parse firstElf1, Int32.Parse firstElf2), (Int32.Parse secondElf1, Int32.Parse secondElf2)

    let part1 (lines : string seq) : int =
        lines
        |> Seq.map (fun s -> parse (s.AsSpan ()))
        |> Seq.filter (fun (firstElf, secondElf) -> fullyContains firstElf secondElf || fullyContains secondElf firstElf
        )
        |> Seq.length

    let part2 (lines : string seq) : int =
        lines
        |> Seq.map (fun s -> parse (s.AsSpan ()))
        |> Seq.filter (fun (firstElf, secondElf) -> overlaps firstElf secondElf)
        |> Seq.length
