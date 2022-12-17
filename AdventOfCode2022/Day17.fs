namespace AdventOfCode2022

open System
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

    let part1 (line : string) : int =
        let directions = parse line

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

        for count in 1..2022 do
            let shape = shapes.[(count - 1) % shapes.Length]

            let mutable currentBase = currentTop - 4

            // Write shape to grid
            for row in shape.Length - 1 .. -1 .. 0 do
                for col in 0 .. shape.[0].Length - 1 do
                    if shape.[row].[col] then
                        let x = 2 + col
                        let y = currentBase + row - shape.Length + 1
                        Arr2D.set startGrid x y 1

            // Set it falling
            let mutable isFalling = true

            while isFalling do
                // Move by jet.
                match directions.[jetCount] with
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

                jetCount <- (jetCount + 1) % directions.Length

                // Fall one place. Can we fall?
                if currentBase = startGrid.Height - 1 then
                    isFalling <- false
                else
                    for row in currentBase .. -1 .. currentBase - 3 do
                        for col in 0..6 do
                            if
                                Arr2D.get startGrid col row = 1
                                && Arr2D.get startGrid col (row + 1) = 2
                            then
                                isFalling <- false

                if isFalling then
                    for row in currentBase .. -1 .. currentBase - 3 do
                        for col in 0..6 do
                            if Arr2D.get startGrid col row = 1 then
                                Arr2D.set startGrid col (row + 1) 1
                                Arr2D.set startGrid col row 0

                    currentBase <- currentBase + 1
                else
                    for row in currentBase .. -1 .. currentBase - 3 do
                        for col in 0..6 do
                            if Arr2D.get startGrid col row = 1 then
                                // Freeze in place
                                Arr2D.set startGrid col row 2

                // Set new currentTop

                for row in currentTop - 1 .. -1 .. currentTop - 4 do
                    for col in 0..6 do
                        if Arr2D.get startGrid col row = 2 then
                            currentTop <- row

                ()

        let mutable towerHeight = 0
        let mutable stillLooking = true
        while stillLooking do
            let row = maxPossibleHeight - towerHeight - 1
            let mutable anyOn = false
            for col in 0..6 do
                if Arr2D.get startGrid col row <> 0 then
                    anyOn <- true
            if not anyOn then
                stillLooking <- false
            else
                towerHeight <- towerHeight + 1
        towerHeight

    let part2 (line : string) : int =
        let directions = parse line
        0
