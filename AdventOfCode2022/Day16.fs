namespace AdventOfCode2022

open System
open System.Collections.Generic
open System.Collections.Immutable
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#endif


[<RequireQualifiedAccess>]
module Day16 =

    type Node = int

    /// Returns the nodes, and also the "AA" node.
    let parse (lines : string seq) : Map<Node, int * Node Set> * Node =
        let allNodes =
            lines
            |> Seq.filter (not << String.IsNullOrEmpty)
            |> Seq.mapi (fun i line ->
                let split = line.Split "tunne" |> Array.last |> (fun s -> s.Trim ())
                let split = split.Split(' ').[4..] |> Array.map (fun s -> s.Trim ',') |> Set.ofArray
                line.[6..7], (i, (Int32.Parse line.[line.IndexOf '=' + 1 .. line.IndexOf ';' - 1], split))
            )
            |> Map.ofSeq

        let result =
            allNodes
            |> Map.toSeq
            |> Seq.map (fun (key, (node, (weight, outbound))) ->
                node, (weight, (Set.map (fun x -> fst (Map.find x allNodes)) outbound))
            )
            |> Map.ofSeq

        result, fst allNodes.["AA"]

    let inline tryMax< ^a when ^a : comparison> (s : seq<'a>) : 'a option =
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

    let getShortestPathLength (valves : Map<_, _>) : Node -> Node -> int =
        let rec go (seenSoFar : Node Set) (v1 : Node) (v2 : Node) =
            let v2Neighbours = snd valves.[v2]

            if v1 = v2 then
                Some 0
            elif Set.contains v1 v2Neighbours then
                Some 1
            elif Set.contains v2 seenSoFar then
                None
            else
                v2Neighbours
                |> Seq.choose (go (Set.add v2 seenSoFar) v1)
                |> tryMin
                |> Option.map ((+) 1)

        fun v1 v2 -> go Set.empty v1 v2 |> Option.get


    type NodeSet = int

    let inline setNode (set : NodeSet) (nodeId : int) : int = set ||| (1 <<< nodeId)
    let inline getNode (set : NodeSet) (nodeId : int) : bool = set &&& (1 <<< nodeId) <> 0
    let ofSeq (nodes : Node seq) : NodeSet =
        (0, nodes)
        ||> Seq.fold setNode

    let toSeq (nodes : NodeSet) : Node seq =
        seq {
            let mutable nodes = nodes
            let mutable count = 0
            while nodes > 0 do
                if nodes % 2 = 0 then yield count
                nodes <- nodes / 2
                count <- count + 1
        }

    let part1 (lines : string seq) : int =
        let valves, aaNode = parse lines
        let allTaps = valves |> Map.filter (fun _ (x, _) -> x > 0) |> Map.keys |> ofSeq

        let getShortestPathLength = getShortestPathLength valves

        let pathWeights =
            Seq.allPairs allTaps allTaps
            |> Seq.map (fun (v1, v2) ->
                let length = getShortestPathLength v1 v2
                (v1, v2), length
            )
            |> Map.ofSeq

        let startChoices =
            allTaps
            |> Seq.map (fun startNode -> startNode, getShortestPathLength aaNode startNode)
            |> Map.ofSeq

        let rec go
            (timeRemainingOnCurrentPath : int)
            (headingTo : Node)
            (alreadyOn : NodeSet)
            (currentWeight : int)
            (remaining : int)
            =
            if remaining <= 0 then
                currentWeight
            elif timeRemainingOnCurrentPath > 0 then
                go (timeRemainingOnCurrentPath - 1) headingTo alreadyOn currentWeight (remaining - 1)
            else

            let nextChoices =
                allTaps
                |> Set.filter (fun t -> t <> headingTo && not (getNode alreadyOn t))

            nextChoices
            |> Seq.map (fun nextVertex ->
                let addToWeight = fst valves.[headingTo] * (remaining - 1)

                go
                    pathWeights.[nextVertex, headingTo]
                    nextVertex
                    (setNode alreadyOn headingTo)
                    (currentWeight + addToWeight)
                    (remaining - 1)
            )
            |> tryMax
            |> Option.defaultValue (currentWeight + (remaining - 1) * (fst valves.[headingTo]))

        startChoices
        |> Map.map (fun start distance -> go distance start 0 0 30)
        |> Map.values
        |> Seq.max

    let part2 (lines : string seq) : int =
        let valves, aaNode = parse lines

        let allTaps = valves |> Map.filter (fun _ (x, _) -> x > 0) |> Map.keys |> Set.ofSeq

        let getShortestPathLength = getShortestPathLength valves

        let pathWeights =
            Seq.allPairs allTaps allTaps
            |> Seq.map (fun (v1, v2) ->
                let length = getShortestPathLength v1 v2
                (v1, v2), length
            )
            |> readOnlyDict

        let rec go
            (journey1 : int)
            (journey2 : int)
            (headingTo1 : Node)
            (headingTo2 : Node)
            (alreadyOn : NodeSet)
            (currentWeight : int)
            (remaining : int)
            =
            if remaining <= 0 then
                currentWeight
            elif journey1 > 0 && journey2 > 0 then
                go (journey1 - 1) (journey2 - 1) headingTo1 headingTo2 alreadyOn currentWeight (remaining - 1)
            elif journey1 = 0 && journey2 > 0 then

                let addToWeight =
                    if getNode alreadyOn headingTo1 then
                        0
                    else
                        (remaining - 1) * (fst valves.[headingTo1])

                let alreadyOn = setNode alreadyOn headingTo1

                allTaps
                |> Seq.filter (fun t -> not <| getNode alreadyOn t)
                |> Seq.map (fun nextVertex ->
                    go
                        pathWeights.[nextVertex, headingTo1]
                        (journey2 - 1)
                        nextVertex
                        headingTo2
                        alreadyOn
                        (currentWeight + addToWeight)
                        (remaining - 1)
                )
                |> tryMax
                |> Option.defaultWith (fun () ->
                    go
                        1000000
                        (journey2 - 1)
                        headingTo1
                        headingTo2
                        alreadyOn
                        (currentWeight + addToWeight)
                        (remaining - 1)
                )
            elif journey2 = 0 && journey1 > 0 then
                let addToWeight =
                    if getNode alreadyOn headingTo2 then
                        0
                    else
                        fst valves.[headingTo2] * (remaining - 1)

                let alreadyOn = setNode alreadyOn headingTo2

                allTaps
                |> Seq.filter (fun t -> not (getNode alreadyOn t))
                |> Seq.map (fun nextVertex ->
                    go
                        (journey1 - 1)
                        pathWeights.[nextVertex, headingTo2]
                        headingTo1
                        nextVertex
                        alreadyOn
                        (currentWeight + addToWeight)
                        (remaining - 1)
                )
                |> tryMax
                |> Option.defaultWith (fun () ->
                    go
                        (journey1 - 1)
                        1000000
                        headingTo1
                        headingTo2
                        alreadyOn
                        (currentWeight + addToWeight)
                        (remaining - 1)
                )
            else
                // Both reached destination at same time
                let addToWeight1 =
                    if getNode alreadyOn headingTo1 then
                        0
                    else
                        (remaining - 1) * fst valves.[headingTo1]

                let addToWeight2 =
                    if getNode alreadyOn headingTo2 then
                        0
                    else
                        (remaining - 1) * fst valves.[headingTo2]

                let addToWeight =
                    if headingTo1 <> headingTo2 then
                        addToWeight1 + addToWeight2
                    else
                        addToWeight1

                let alreadyOn = setNode (setNode alreadyOn headingTo1) headingTo2

                let nextChoices = allTaps |> Set.filter (fun v -> not (getNode alreadyOn v))

                if nextChoices.Count >= 2 then
                    Seq.allPairs nextChoices nextChoices
                    |> Seq.map (fun (next1, next2) ->
                        go
                            pathWeights.[next1, headingTo1]
                            pathWeights.[next2, headingTo2]
                            next1
                            next2
                            alreadyOn
                            (currentWeight + addToWeight)
                            (remaining - 1)
                    )
                    |> Seq.max
                elif nextChoices.Count = 0 then
                    0
                else
                    let next = Seq.exactlyOne nextChoices
                    // nextChoices.Count = 1
                    go
                        100000
                        pathWeights.[next, headingTo2]
                        next
                        next
                        alreadyOn
                        (currentWeight + addToWeight)
                        (remaining - 1)

        let startChoices =
            allTaps
            |> Seq.map (fun startNode -> startNode, getShortestPathLength aaNode startNode)
            |> Map.ofSeq

        let maxVal = ref Int32.MinValue

        let s =
            Seq.allPairs startChoices startChoices
            |> Seq.filter (fun (KeyValue (n1, _), KeyValue (n2, _)) -> n1 < n2)
            |> PSeq.map (fun (KeyValue (start1, distance1), KeyValue (start2, distance2)) ->
                go distance1 distance2 start1 start2 0 0 26
            )
            |> PSeq.iter (fun s ->
                lock
                    maxVal
                    (fun () ->
                        if s > maxVal.Value then
                            maxVal.Value <- s
                            Console.WriteLine (sprintf "%i" s)
                        else
                            Console.WriteLine "(did not win)"
                    )
            )

        maxVal.Value
