namespace AdventOfCode2022

open System
open Microsoft.FSharp.NativeInterop

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day25 =

    let toInt (s : ReadOnlySpan<char>) : int64 =
        let mutable i = 0L
        let mutable fivePow = 1L

        for c = s.Length - 1 downto 0 do
            match s.[c] with
            | '2' -> i <- i + 2L * fivePow
            | '1' -> i <- i + fivePow
            | '0' -> ()
            | '-' -> i <- i - fivePow
            | '=' -> i <- i - 2L * fivePow
            | c -> failwithf "unrecognised: %c" c

            fivePow <- fivePow * 5L

        i

    let rec ofInt (i : int64) : string =
        if i <= 2L then
            sprintf "%i" i
        else
            match i % 5L with
            | 0L -> sprintf "%s0" (ofInt (i / 5L))
            | 1L -> sprintf "%s1" (ofInt (i / 5L))
            | 2L -> sprintf "%s2" (ofInt (i / 5L))
            | 3L -> sprintf "%s=" (ofInt (i / 5L + 1L))
            | 4L -> sprintf "%s-" (ofInt (i / 5L + 1L))
            | _ -> failwith "maths doesn't work"

    /// Returns the width and the height too. The resulting array is suitable to become an Arr2D.
    let parse (line : StringSplitEnumerator) : int64 ResizeArray =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let line = enum.Current.Trim ()
                output.Add (toInt line)

        output

    let part1 (lines : StringSplitEnumerator) : string =
        let numbers = parse lines

        numbers |> Seq.map int64 |> Seq.sum |> ofInt
