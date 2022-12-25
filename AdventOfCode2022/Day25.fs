namespace AdventOfCode2022

open System
open System.Text
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

    let ofInt (i : int64) : string =
        let rec go (i : int64) (sb : ResizeArray<char>) =
            if i = 0 then
                sb.Add '0'
            elif i = 1 then
                sb.Add '1'
            elif i = 2 then
                sb.Add '2'
            else

            match i % 5L with
            | 0L ->
                sb.Add '0'
                go (i / 5L) sb
            | 1L ->
                sb.Add '1'
                go (i / 5L) sb
            | 2L ->
                sb.Add '2'
                go (i / 5L) sb
            | 3L ->
                sb.Add '='
                go (i / 5L + 1L) sb
            | 4L ->
                sb.Add '-'
                go (i / 5L + 1L) sb
            | _ -> failwith "maths doesn't work"

        let sb = ResizeArray 27
        go i sb
        sb.Reverse ()
        String (sb.ToArray ())

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
