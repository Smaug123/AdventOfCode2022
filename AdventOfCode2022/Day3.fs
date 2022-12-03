namespace AdventOfCode2022

[<Measure>]
type Priority

[<RequireQualifiedAccess>]
module Day3 =

    let toPriority (c : char) : int<Priority> =
        if 'a' <= c && c <= 'z' then int c - int 'a' + 1
        elif 'A' <= c && c <= 'Z' then int c - int 'A' + 27
        else failwithf "oh no: %c" c
        |> LanguagePrimitives.Int32WithMeasure

    let part1 (lines : string seq) : int<Priority> =
        lines
        |> Seq.map (fun s ->
            let s = s.Trim ()
            let s1 = s.[0 .. s.Length / 2 - 1].ToCharArray () |> Set.ofSeq
            let s2 = s.[s.Length / 2 ..].ToCharArray () |> Set.ofSeq
            Set.intersect s1 s2 |> Seq.exactlyOne |> toPriority
        )
        |> Seq.sum

    let part2 (lines : string seq) : int<Priority> =
        lines
        |> Seq.chunkBySize 3
        |> Seq.map (fun strArr ->

            strArr
            |> Array.map (fun s -> s.Trim().ToCharArray () |> Set.ofSeq)
            |> Set.intersectMany
            |> Seq.exactlyOne
            |> toPriority
        )
        |> Seq.sum
