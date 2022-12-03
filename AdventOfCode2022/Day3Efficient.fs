namespace AdventOfCode2022

open System

type private CharSet =
    {
        Counts : int array
    }

[<RequireQualifiedAccess>]
module private CharSet =

    let empty () =
        {
            Counts = Array.zeroCreate 52
        }

    /// Not thread-safe.
    /// Returns true if the element was already in the set.
    let add<'v> (charMap : CharSet) (key : char) : bool =
        let key : int = Day3.toPriority key |> int<int<_>>
        let previous = charMap.Counts.[key - 1]

        if previous = 0 then
            charMap.Counts.[key - 1] <- previous + 1
            false
        else
            true

    let ofSpan (keys : ReadOnlySpan<char>) : CharSet =
        let charSet = empty ()

        for key in keys do
            charSet.Counts.[(Day3.toPriority key |> int<int<_>>) - 1] <- 1

        charSet

    let contains (charSet : CharSet) (key : char) : bool =
        charSet.Counts.[int<int<_>> (Day3.toPriority key) - 1] > 0

    let intersectMany (sets : CharSet array) : int<Priority> =
        let rec compareEntry (entry : int) (arrCount : int) =
            if arrCount >= sets.Length then
                true
            else if sets.[arrCount].Counts.[entry] > 0 then
                compareEntry entry (arrCount + 1)
            else
                false

        let rec go (i : int) : int<_> =
            if i >= 52 then
                failwith "failed to find intersection"
            else if compareEntry i 0 then
                LanguagePrimitives.Int32WithMeasure (i + 1)
            else
                go (i + 1)

        go 0

[<RequireQualifiedAccess>]
module Day3Efficient =

    let rec private go (set : CharSet) (s : ReadOnlySpan<char>) (i : int) =
        let char = s.[i]

        if CharSet.contains set char then
            Day3.toPriority char
        else
            go set s (i + 1)

    let part1 (lines : string seq) : int<Priority> =
        lines
        |> Seq.map (fun s ->
            let s = s.AsSpan().Trim ()
            let set = CharSet.ofSpan (s.Slice (0, s.Length / 2))

            go set (s.Slice (s.Length / 2)) 0
        )
        |> Seq.sum

    let part2 (lines : string seq) : int<Priority> =
        lines
        |> Seq.chunkBySize 3
        |> Seq.map (fun strArr ->
            strArr
            |> Array.map (fun s -> CharSet.ofSpan (s.AsSpan().Trim ()))
            |> CharSet.intersectMany
        )
        |> Seq.sum
