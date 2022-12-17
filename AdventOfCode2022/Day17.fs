namespace AdventOfCode2022

open System
open System.Collections.Generic
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#else
#nowarn "9"
#endif


[<RequireQualifiedAccess>]
module Day17 =

    /// Returns the nodes, and also the "AA" node.
    let parse (line : string) : Direction array =
        Array.init
            line.Length
            (fun i ->
                match line.[i] with
                | '<' -> Direction.Left
                | '>' -> Direction.Right
                | c -> failwithf "unexpected char %c" c
            )

    let printGrid (arr : Arr2D<int>) (currentTop : int) =
        for row in currentTop - 4 .. arr.Height - 1 do
            for col in 0..6 do
                match Arr2D.get arr col row with
                | 0 -> printf "."
                | 1 -> printf "@"
                | 2 -> printf "#"
                | _ -> failwith "oh no"

            printfn ""

        printfn "--------------------"

    let towerHeight (maxPossibleHeight : int) (grid : Arr2D<_>) =
        let mutable towerHeight = 0
        let mutable stillLooking = true

        while stillLooking do
            let row = maxPossibleHeight - towerHeight - 1
            let mutable anyOn = false

            for col in 0..6 do
                if Arr2D.get grid col row <> 0 then
                    anyOn <- true

            if not anyOn then
                stillLooking <- false
            else
                towerHeight <- towerHeight + 1

        towerHeight

    let introduceRock (shape : bool[][]) startGrid currentBase =
        for row in shape.Length - 1 .. -1 .. 0 do
            for col in 0 .. shape.[0].Length - 1 do
                if shape.[row].[col] then
                    let x = 2 + col
                    let y = currentBase + row - shape.Length + 1
                    Arr2D.set startGrid x y 1

    let moveJet (direction : Direction) (currentBase : int) (startGrid : Arr2D<int>) : unit =
        match direction with
        | Direction.Left ->
            let mutable canMove = true

            for row in currentBase .. -1 .. currentBase - 3 do
                if Arr2D.get startGrid 0 row = 1 then
                    canMove <- false
                else
                    for col in 1..6 do
                        if Arr2D.get startGrid col row = 1 && Arr2D.get startGrid (col - 1) row = 2 then
                            canMove <- false

            if canMove then
                for row in currentBase .. -1 .. currentBase - 3 do
                    for col in 0..5 do
                        if Arr2D.get startGrid (col + 1) row = 1 then
                            Arr2D.set startGrid col row 1
                            Arr2D.set startGrid (col + 1) row 0

                    if Arr2D.get startGrid 6 row = 1 then
                        Arr2D.set startGrid 6 row 0
        | Direction.Right ->
            let mutable canMove = true

            for row in currentBase .. -1 .. currentBase - 3 do
                if Arr2D.get startGrid 6 row = 1 then
                    canMove <- false
                else
                    for col in 0..5 do
                        if Arr2D.get startGrid col row = 1 && Arr2D.get startGrid (col + 1) row = 2 then
                            canMove <- false

            if canMove then
                for row in currentBase .. -1 .. currentBase - 3 do
                    for col in 6..-1..1 do
                        if Arr2D.get startGrid (col - 1) row = 1 then
                            Arr2D.set startGrid col row 1
                            Arr2D.set startGrid (col - 1) row 0

                    if Arr2D.get startGrid 1 row = 1 then
                        Arr2D.set startGrid 0 row 0
        | _ -> failwith "Unexpected direction"

    /// Returns the new currentBase if we're still falling, or None if we're not
    /// still falling.
    let fallOnce (currentBase : int) (startGrid : Arr2D<int>) : int option =
        let mutable isFalling = true

        // Fall one place. Can we fall?
        if currentBase = startGrid.Height - 1 then
            isFalling <- false
        else
            for row in currentBase .. -1 .. currentBase - 3 do
                for col in 0..6 do
                    if Arr2D.get startGrid col row = 1 && Arr2D.get startGrid col (row + 1) = 2 then
                        isFalling <- false

        if isFalling then
            for row in currentBase .. -1 .. currentBase - 3 do
                for col in 0..6 do
                    if Arr2D.get startGrid col row = 1 then
                        Arr2D.set startGrid col (row + 1) 1
                        Arr2D.set startGrid col row 0

            Some (currentBase + 1)
        else
            for row in currentBase .. -1 .. currentBase - 3 do
                for col in 0..6 do
                    if Arr2D.get startGrid col row = 1 then
                        // Freeze in place
                        Arr2D.set startGrid col row 2

            None

    let findCurrentTop (currentTop : int) (startGrid : Arr2D<int>) : int =
        let mutable currentTop = currentTop

        for row in currentTop - 1 .. -1 .. currentTop - 4 do
            for col in 0..6 do
                if Arr2D.get startGrid col row = 2 then
                    currentTop <- row

        currentTop

    let shapes =
        [|
            [| [| true ; true ; true ; true |] |]
            [|
                [| false ; true ; false |]
                [| true ; true ; true |]
                [| false ; true ; false |]
            |]
            [|
                [| false ; false ; true |]
                [| false ; false ; true |]
                [| true ; true ; true |]
            |]
            Array.init 4 (fun _ -> [| true |])
            Array.init 2 (fun _ -> [| true ; true |])
        |]

    let part1 (line : string) : int =
        let directions = parse line

        let maxPossibleHeight =
            shapes
            |> Array.map Array.length // if each shape stacked perfectly on top
            |> Array.sum
            |> fun i -> i * (2022 / 5 + 1)

#if DEBUG
        let startGrid = Arr2D.zeroCreate<int> 7 maxPossibleHeight
#else
        let startGridBacking = Array.zeroCreate (7 * maxPossibleHeight)
        use ptr = fixed startGridBacking
        let startGrid = Arr2D.zeroCreate<int> ptr 7 maxPossibleHeight
#endif

        let mutable currentTop = maxPossibleHeight
        let mutable jetCount = 0

        for count in 0 .. 2022 - 1 do
            let shape = shapes.[count % shapes.Length]

            let mutable currentBase = currentTop - 4

            introduceRock shape startGrid currentBase

            // Set it falling
            let mutable isFalling = true

            while isFalling do
                // Move by jet.
                moveJet directions.[jetCount % directions.Length] currentBase startGrid
                jetCount <- (jetCount + 1) % directions.Length

                match fallOnce currentBase startGrid with
                | Some newCurrentBase -> currentBase <- newCurrentBase
                | None -> isFalling <- false

                // Set new currentTop
                currentTop <- findCurrentTop currentTop startGrid

        towerHeight maxPossibleHeight startGrid

    let part2 (line : string) : int64 =
        let directions = parse line

        let maxPossibleHeight =
            shapes
            |> Array.map Array.length // if each shape stacked perfectly on top
            |> Array.sum
            |> fun i -> i * (100000000 / 5 + 1)

#if DEBUG
        let startGrid = Arr2D.zeroCreate<int> 7 maxPossibleHeight
#else
        let startGridBacking = Array.zeroCreate (7 * maxPossibleHeight)
        use ptr = fixed startGridBacking
        let startGrid = Arr2D.zeroCreate<int> ptr 7 maxPossibleHeight
#endif

        let mutable currentTop = maxPossibleHeight

        let mutable shapeCount = 0
        let mutable jetCount = directions.Length
        let seenJetCounts = HashSet ()
        let mutable fromLastCycle = ValueNone
        let mutable skippedFromCycle = -1L

        let limit = 1000000000000L
        let mutable remainingStones = limit

        while remainingStones > 0 do
            for count in 0 .. shapes.Length - 1 do
                shapeCount <- shapeCount + 1
                remainingStones <- remainingStones - 1L
                let shape = shapes.[count]

                let mutable currentBase = currentTop - 4

                introduceRock shape startGrid currentBase

                // Set it falling
                let mutable isFalling = true

                while isFalling do
                    // Move by jet.
                    moveJet directions.[jetCount % directions.Length] currentBase startGrid
                    jetCount <- (jetCount + 1) % directions.Length

                    match fallOnce currentBase startGrid with
                    | Some newCurrentBase -> currentBase <- newCurrentBase
                    | None -> isFalling <- false

                    // Set new currentTop
                    currentTop <- findCurrentTop currentTop startGrid

            // Try and find a duplicate.
            if not (seenJetCounts.Add jetCount) then
                match fromLastCycle with
                | ValueNone ->
                    let towerHeight = towerHeight maxPossibleHeight startGrid
                    seenJetCounts.Clear ()
                    seenJetCounts.Add jetCount |> ignore
                    fromLastCycle <- ValueSome (shapeCount, towerHeight)
                | ValueSome (prevShapeCount, prevTowerHeight) ->
                    let towerHeight = towerHeight maxPossibleHeight startGrid
                    let heightGainedPerCycle = towerHeight - prevTowerHeight
                    let piecesPerCycle = shapeCount - prevShapeCount

                    let remainingCycles = (limit - int64 shapeCount) / int64 piecesPerCycle
                    skippedFromCycle <- remainingCycles * int64 heightGainedPerCycle

                    remainingStones <- (limit - int64 shapeCount) % int64 piecesPerCycle
                    seenJetCounts.Clear ()

        let towerHeight = towerHeight maxPossibleHeight startGrid

        int64 towerHeight + skippedFromCycle
