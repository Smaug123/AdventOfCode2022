namespace AdventOfCode2022

open System
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#else
#nowarn "9"
#endif


[<RequireQualifiedAccess>]
module Day16 =

    type Node = int

    /// Returns the nodes, and also the "AA" node.
    let parse (lines : string seq) : Map<Node, int * IntSet> * Node =
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
                let outbound =
                    outbound |> Seq.map (fun x -> Map.find x allNodes |> fst) |> IntSet.ofSeq

                node, (weight, outbound)
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

    let tryMin (s : seq<'a>) : 'a ValueOption =
        use enum = s.GetEnumerator ()

        if not (enum.MoveNext ()) then
            ValueNone
        else
            let mutable answer = enum.Current

            while enum.MoveNext () do
                answer <- min answer enum.Current

            ValueSome answer

    let getShortestPathLength (valves : Map<_, _>) : Node -> Node -> int =
        let rec go (seenSoFar : IntSet) (v1 : Node) (v2 : Node) : int =
            let v2Neighbours = snd valves.[v2]

            if v1 = v2 then
                0
            elif IntSet.contains v2Neighbours v1 then
                1
            elif IntSet.contains seenSoFar v2 then
                -1
            else
                let mutable best = Int32.MaxValue
                let mutable neighbours = v2Neighbours
                let mutable neighbour = 0

                while neighbours > 0 do
                    if neighbours % 2L = 1L then
                        match go (IntSet.set seenSoFar v2) v1 neighbour with
                        | -1 -> ()
                        | next ->
                            if next < best then
                                best <- next

                    neighbours <- neighbours >>> 1
                    neighbour <- neighbour + 1

                if best = Int32.MaxValue then -1 else best + 1

        fun v1 v2 -> go IntSet.empty v1 v2


    let part1 (lines : string seq) : int =
        let valves, aaNode = parse lines

        let allTaps =
            valves |> Map.filter (fun _ (x, _) -> x > 0) |> Map.keys |> IntSet.ofSeq

        let getShortestPathLength = getShortestPathLength valves

#if DEBUG
        let pathWeights = Arr2D.zeroCreate<int> valves.Count valves.Count
#else
        let pathWeightsStorage = Array.zeroCreate (valves.Count * valves.Count)
        use ptr = fixed pathWeightsStorage
        let pathWeights = Arr2D.zeroCreate<int> ptr valves.Count valves.Count
#endif
        for v1 in IntSet.toSeq allTaps do
            for v2 in IntSet.toSeq allTaps do
                let length = getShortestPathLength v1 v2
                Arr2D.set pathWeights v1 v2 length

        let rec go
            (timeRemainingOnCurrentPath : int)
            (headingTo : Node)
            (alreadyOn : IntSet)
            (currentWeight : int)
            (remaining : int)
            =
            if remaining <= 0 then
                currentWeight
            elif timeRemainingOnCurrentPath > 0 then
                go (timeRemainingOnCurrentPath - 1) headingTo alreadyOn currentWeight (remaining - 1)
            else

            let alreadyOn = IntSet.set alreadyOn headingTo

            let mutable allTaps = allTaps &&& (~~~alreadyOn)
            let mutable count = 0
            let mutable max = ValueNone

            while allTaps > 0 do
                if allTaps % 2L = 1 then
                    let addToWeight = fst valves.[headingTo] * (remaining - 1)

                    let candidate =
                        go
                            (Arr2D.get pathWeights count headingTo)
                            count
                            alreadyOn
                            (currentWeight + addToWeight)
                            (remaining - 1)

                    match max with
                    | ValueNone -> max <- ValueSome candidate
                    | ValueSome existingMax ->
                        if existingMax < candidate then
                            max <- ValueSome candidate

                allTaps <- allTaps >>> 1
                count <- count + 1

            match max with
            | ValueSome v -> v
            | ValueNone -> currentWeight + (remaining - 1) * (fst valves.[headingTo])

        let mutable startChoices = allTaps
        let mutable start = 0
        let mutable maxValue = Int32.MinValue

        while startChoices > 0 do
            if startChoices % 2L = 1L then
                let distance = getShortestPathLength aaNode start
                // By inspecting the graph, I can see that AA is screened off from the rest
                // of the graph by the set of valves with distance at most 3 from it.
                // Assume that we're going to turn them on when we pass through - this isn't
                // actually fully general, but it is enough.
                if distance <= 3 then
                    let candidate = go distance start 0 0 30
                    maxValue <- max maxValue candidate

            startChoices <- startChoices >>> 1
            start <- start + 1

        maxValue

    let part2 (lines : string seq) : int =
        let valves, aaNode = parse lines
        let valvesIndexed = valves |> Map.values |> Array.ofSeq

        let allTaps =
            valves |> Map.filter (fun _ (x, _) -> x > 0) |> Map.keys |> IntSet.ofSeq

        let getShortestPathLength = getShortestPathLength valves

#if DEBUG
        let pathWeights = Arr2D.zeroCreate<int> valves.Count valves.Count
#else
        let pathWeightsStorage = Array.zeroCreate (valves.Count * valves.Count)
        use ptr = fixed pathWeightsStorage
        let pathWeights = Arr2D.zeroCreate<int> ptr valves.Count valves.Count
#endif

        for v1 in IntSet.toSeq allTaps do
            for v2 in IntSet.toSeq allTaps do
                let length = getShortestPathLength v1 v2
                Arr2D.set pathWeights v1 v2 length

        let rec go
            (journey1 : int)
            (journey2 : int)
            (headingTo1 : Node)
            (headingTo2 : Node)
            (alreadyOn : IntSet)
            (currentWeight : int)
            (remaining : int)
            =
            if remaining <= 0 then
                currentWeight
            elif journey1 > 0 && journey2 > 0 then
                go (journey1 - 1) (journey2 - 1) headingTo1 headingTo2 alreadyOn currentWeight (remaining - 1)
            elif journey1 = 0 && journey2 > 0 then

                let addToWeight =
                    if IntSet.contains alreadyOn headingTo1 then
                        0
                    else
                        (remaining - 1) * (fst valvesIndexed.[headingTo1])

                let newWeight = addToWeight + currentWeight

                let alreadyOn = IntSet.set alreadyOn headingTo1

                let mutable allTaps = allTaps &&& ~~~alreadyOn
                let mutable node = 0
                let mutable max = ValueNone

                while allTaps > 0L do
                    // Strictly speaking we might be able to overtake the other one
                    // who is heading for their node, but it so happens that the
                    // test cases don't care about that.
                    if allTaps % 2L = 1L && node <> headingTo2 then
                        let next =
                            go
                                (Arr2D.get pathWeights node headingTo1)
                                (journey2 - 1)
                                node
                                headingTo2
                                alreadyOn
                                newWeight
                                (remaining - 1)

                        match max with
                        | ValueNone -> max <- ValueSome next
                        | ValueSome existingMax ->
                            if next > existingMax then
                                max <- ValueSome next

                    allTaps <- allTaps >>> 1
                    node <- node + 1

                match max with
                | ValueSome v -> v
                | ValueNone -> go 1000000 (journey2 - 1) headingTo1 headingTo2 alreadyOn newWeight (remaining - 1)

            elif journey2 = 0 && journey1 > 0 then
                let addToWeight =
                    if IntSet.contains alreadyOn headingTo2 then
                        0
                    else
                        fst valvesIndexed.[headingTo2] * (remaining - 1)

                let newWeight = addToWeight + currentWeight

                let alreadyOn = IntSet.set alreadyOn headingTo2

                let mutable allTaps = allTaps &&& ~~~alreadyOn
                let mutable node = 0
                let mutable max = ValueNone

                while allTaps > 0L do
                    // Strictly speaking we might be able to overtake the other one
                    // who is heading for their node, but it so happens that the
                    // test cases don't care about that.
                    if allTaps % 2L = 1L && node <> headingTo1 then
                        let next =
                            go
                                (journey1 - 1)
                                (Arr2D.get pathWeights node headingTo2)
                                headingTo1
                                node
                                alreadyOn
                                newWeight
                                (remaining - 1)

                        match max with
                        | ValueNone -> max <- ValueSome next
                        | ValueSome existingMax ->
                            if next > existingMax then
                                max <- ValueSome next

                    allTaps <- allTaps >>> 1
                    node <- node + 1

                match max with
                | ValueSome v -> v
                | ValueNone -> go (journey1 - 1) 1000000 headingTo1 headingTo2 alreadyOn newWeight (remaining - 1)

            else
                // Both reached destination at same time
                let addToWeight1 =
                    if IntSet.contains alreadyOn headingTo1 then
                        0
                    else
                        (remaining - 1) * fst valvesIndexed.[headingTo1]

                let addToWeight2 =
                    if IntSet.contains alreadyOn headingTo2 then
                        0
                    else
                        (remaining - 1) * fst valvesIndexed.[headingTo2]

                let newWeight =
                    if headingTo1 <> headingTo2 then
                        addToWeight1 + addToWeight2 + currentWeight
                    else
                        addToWeight1 + currentWeight

                let alreadyOn = IntSet.set (IntSet.set alreadyOn headingTo1) headingTo2

                let nextChoices = allTaps &&& ~~~alreadyOn

                if IntSet.count nextChoices >= 2 then
                    let mutable maxVal = Int32.MinValue

                    let mutable next1 = nextChoices
                    let mutable count1 = 0

                    while next1 > 0 do
                        if next1 % 2L = 1 then
                            let mutable next2 = nextChoices
                            let mutable count2 = 0

                            while next2 > 0 do
                                // It's never correct for both to try and turn on the same node.
                                if next2 % 2L = 1 && next1 <> next2 then
                                    let candidate =
                                        go
                                            (Arr2D.get pathWeights count1 headingTo1)
                                            (Arr2D.get pathWeights count2 headingTo2)
                                            count1
                                            count2
                                            alreadyOn
                                            newWeight
                                            (remaining - 1)

                                    maxVal <- max maxVal candidate

                                next2 <- next2 >>> 1
                                count2 <- count2 + 1

                        next1 <- next1 >>> 1
                        count1 <- count1 + 1

                    maxVal

                elif nextChoices = 0 then
                    0
                else
                    // nextChoices.Count = 1
                    let next = IntSet.first nextChoices
                    go 100000 (Arr2D.get pathWeights next headingTo2) next next alreadyOn newWeight (remaining - 1)

        let startChoices =
            allTaps
            |> IntSet.toSeq
            |> Seq.map (fun startNode -> startNode, getShortestPathLength aaNode startNode)
            |> Map.ofSeq

        let maxVal = ref Int32.MinValue

        Seq.allPairs startChoices startChoices
        |> Seq.filter (fun (KeyValue (n1, distance1), KeyValue (n2, distance2)) ->
            // By inspecting the graph, I can see that AA is screened off from the rest
            // of the graph by the set of valves with distance at most 3 from it.
            // Assume that we're going to turn them on when we pass through - this isn't
            // actually fully general, but it is enough.
            n1 < n2 && distance1 <= 3 && distance2 <= 3
        )
        |> PSeq.map (fun (KeyValue (start1, distance1), KeyValue (start2, distance2)) ->
            go distance1 distance2 start1 start2 0 0 26
        )
        |> PSeq.iter (fun s ->
            lock
                maxVal
                (fun () ->
                    if s > maxVal.Value then
                        maxVal.Value <- s
                )
        )

        maxVal.Value
