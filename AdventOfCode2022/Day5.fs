namespace AdventOfCode2022

open System

[<RequireQualifiedAccess>]
module Day5 =

    let parse (s : ReadOnlySpan<char>) : (int * int) * (int * int) =
        failwith "TODO"

    let part1 (lines : string seq) : int =
        lines
        |> Seq.map (fun s -> parse (s.AsSpan ()))
        |> Seq.map (fun x ->
            failwith "TODO"
        )
        |> Seq.length

    let part2 (lines : string seq) : int =
        lines
        |> Seq.map (fun s -> parse (s.AsSpan ()))
        |> Seq.map (fun x ->
            failwith "TODO"
        )
        |> Seq.length
