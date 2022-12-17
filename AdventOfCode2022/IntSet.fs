namespace AdventOfCode2022

type IntSet = int64

[<RequireQualifiedAccess>]
module IntSet =

    let inline set (set : IntSet) (nodeId : int) : IntSet = set ||| (1L <<< nodeId)
    let inline contains (set : IntSet) (nodeId : int) : bool = set &&& (1L <<< nodeId) <> 0
    let ofSeq (nodes : int seq) : IntSet = (0L, nodes) ||> Seq.fold set

    let toSeq (nodes : IntSet) : int seq =
        seq {
            let mutable nodes = nodes
            let mutable count = 0

            while nodes > 0 do
                if nodes % 2L = 1L then
                    yield count

                nodes <- nodes >>> 1
                count <- count + 1
        }

    let count (nodes : IntSet) : int =
        let mutable nodes = nodes
        let mutable ans = 0

        while nodes > 0 do
            if nodes % 2L = 1L then
                ans <- ans + 1

            nodes <- nodes >>> 1

        ans


    let first (nodes : IntSet) : int =
        let mutable nodes = nodes
        let mutable count = 0
        let mutable ans = 0
        let mutable keepGoing = true

        while keepGoing && nodes > 0 do
            if nodes % 2L = 1L then
                ans <- count
                keepGoing <- false

            nodes <- nodes >>> 1
            count <- count + 1

        ans
