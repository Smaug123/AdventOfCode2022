namespace AdventOfCode2022

open System
open System.Globalization

[<RequireQualifiedAccess>]
module Day4 =

    /// This only checks if the first arg fully contains the second arg.
    let inline fullyContains (a, b) (c, d) : bool = a <= c && b >= d

    let inline overlaps (a, b) (c, d) : bool = b >= c && a <= d

    let inline private parseInt (c : ReadOnlySpan<char>) : int =
        Int32.Parse (c, NumberStyles.AllowTrailingWhite)

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
        (parseInt firstElf1, parseInt firstElf2), (parseInt secondElf1, parseInt secondElf2)

    let part1 (lines : StringSplitEnumerator) : int =
        let mutable count = 0

        for line in lines do
            let firstElf, secondElf = parse line

            if fullyContains firstElf secondElf || fullyContains secondElf firstElf then
                count <- count + 1

        count

    let part2 (lines : StringSplitEnumerator) : int =
        let mutable count = 0

        for line in lines do
            let firstElf, secondElf = parse line

            if overlaps firstElf secondElf then
                count <- count + 1

        count
