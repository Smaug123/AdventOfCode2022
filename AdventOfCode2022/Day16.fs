namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#endif


[<RequireQualifiedAccess>]
module Day16 =

    type Node = string

    let parse (lines : string seq) : Map<Node, int * Node Set> =
        lines
        |> Seq.filter (not << String.IsNullOrEmpty)
        |> Seq.map (fun line ->
            let split = line.Split "tunne" |> Array.last |> (fun s -> s.Trim ())
            let split = split.Split(' ').[4..] |> Array.map (fun s -> s.Trim ',') |> Set.ofArray
            line.[6..7], (Int32.Parse line.[line.IndexOf '=' + 1 .. line.IndexOf ';' - 1], split)
        )
        |> Map.ofSeq

    let tryMax (s : seq<'a>) : 'a option =
        use enum = s.GetEnumerator ()

        if not (enum.MoveNext ()) then
            None
        else
            let mutable answer = enum.Current

            while enum.MoveNext () do
                answer <- max answer enum.Current

            Some answer

    let tryMin (s : seq<'a>) : 'a option =
        use enum = s.GetEnumerator ()

        if not (enum.MoveNext ()) then
            None
        else
            let mutable answer = enum.Current

            while enum.MoveNext () do
                answer <- min answer enum.Current

            Some answer

    let part1 (lines : string seq) : int =
        let valves = parse lines
        let allTaps = valves |> Map.filter (fun _ (x, _) -> x > 0) |> Map.keys |> Set.ofSeq

        let rec getShortestPathLength (seenSoFar : Node Set) (v1 : Node) (v2 : Node) =
            let v2Neighbours = snd valves.[v2]

            if v1 = v2 then
                Some 0
            elif Set.contains v1 v2Neighbours then
                Some 1
            elif Set.contains v2 seenSoFar then
                None
            else
                v2Neighbours
                |> Seq.choose (getShortestPathLength (Set.add v2 seenSoFar) v1)
                |> tryMin
                |> Option.map ((+) 1)

        let pathWeights =
            Seq.allPairs allTaps allTaps
            |> Seq.map (fun (v1, v2) ->
                let length = Option.get (getShortestPathLength Set.empty v1 v2)
                (v1, v2), length
            )
            |> Map.ofSeq

        let startChoices =
            allTaps
            |> Seq.map (fun startNode -> startNode, Option.get (getShortestPathLength Set.empty "AA" startNode))
            |> Map.ofSeq

        let rec go
            (timeRemainingOnCurrentPath : int)
            (headingTo : Node)
            (alreadyOn : Node Set)
            (currentWeight : int)
            (remaining : int)
            =
            if remaining <= 0 then currentWeight
            elif timeRemainingOnCurrentPath > 0 then
                go (timeRemainingOnCurrentPath - 1) headingTo alreadyOn currentWeight (remaining - 1)
            else

            let nextChoices = allTaps |> Set.filter (fun t -> t <> headingTo && not (Set.contains t alreadyOn))

            nextChoices
            |> Seq.map (fun nextVertex ->
                let addToWeight = fst valves.[headingTo] * (remaining - 1)
                go pathWeights.[nextVertex, headingTo] nextVertex (Set.add headingTo alreadyOn) (currentWeight + addToWeight) (remaining - 1)
            )
            |> tryMax
            |> Option.defaultValue (currentWeight + (remaining - 1) * (fst valves.[headingTo]))

        startChoices
        |> Map.map (fun start distance ->
            go distance start Set.empty 0 30
        )
        |> Map.values
        |> Seq.max

    let part2 (lines : StringSplitEnumerator) : int64 =
        //let sensors = parse lines

        0L
