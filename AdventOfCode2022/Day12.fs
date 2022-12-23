namespace AdventOfCode2022

open System
open System.Runtime.CompilerServices

#if DEBUG
open Checked
#endif

#if DEBUG
#else
#nowarn "9"
#endif

[<Struct>]
[<IsReadOnly>]
type Coordinate =
    {
        X : int
        Y : int
    }

[<RequireQualifiedAccess>]
module Day12 =

    let private charToByte (c : char) = byte c - byte 'a'

    let parse (lines : StringSplitEnumerator) : array<byte> * int * Coordinate * Coordinate =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable startPos = Unchecked.defaultof<Coordinate>
        let mutable endPos = Unchecked.defaultof<Coordinate>
        let mutable row = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let current = enum.Current.TrimEnd ()
                let arr = Array.zeroCreate current.Length

                for i = 0 to arr.Length - 1 do
                    if current.[i] = 'S' then
                        startPos <-
                            {
                                X = i
                                Y = row
                            }

                        arr.[i] <- 0uy
                    elif current.[i] = 'E' then
                        endPos <-
                            {
                                X = i
                                Y = row
                            }

                        arr.[i] <- 25uy
                    else
                        arr.[i] <- charToByte current.[i]

                arr |> output.Add
                row <- row + 1

        let arr = Array.zeroCreate<byte> (output.Count * output.[0].Length)

        for x = 0 to output.[0].Length - 1 do
            for y = 0 to output.Count - 1 do
                arr.[y * output.[0].Length + x] <- output.[y].[x]

        arr, output.[0].Length, startPos, endPos

    let inline isEdge (nodes : Arr2D< ^a >) sourceX sourceY destX destY =
        Arr2D.get nodes sourceX sourceY
        <= LanguagePrimitives.GenericOne + Arr2D.get nodes destX destY

    /// The input arrays must all have the same dimensions.
    /// `nodes` will not be mutated; `distances` and `isVisited` will be mutated.
    /// (As a result of these arguments, `dijkstra` is allocation-free.)
    /// Returns the shortest path to the destination, or the min of the shortest path to all
    /// destinations.
    let dijkstra
        (distances : Arr2D<int>)
        (isVisited : Arr2D<bool>)
        (nodes : Arr2D<byte>)
        (start : Coordinate)
        (dest : Coordinate option)
        : int
        =
        let mutable currentX = start.X
        let mutable currentY = start.Y
        let mutable currentDistance = 0

        Arr2D.clear isVisited
        // A distance of 0 is interpreted as infinite.
        Arr2D.clear distances

        let mutable stillGoing = true

        while stillGoing && currentDistance < Int32.MaxValue do
            if currentX < distances.Width - 1 then
                if
                    not (Arr2D.get isVisited (currentX + 1) currentY)
                    && isEdge nodes currentX currentY (currentX + 1) currentY
                then
                    let newDistance = 1 + currentDistance

                    let distanceGuess = Arr2D.get distances (currentX + 1) currentY

                    if distanceGuess = 0 || newDistance < distanceGuess then
                        Arr2D.set distances (currentX + 1) currentY newDistance

            if currentX > 0 then
                if
                    not (Arr2D.get isVisited (currentX - 1) currentY)
                    && isEdge nodes currentX currentY (currentX - 1) currentY
                then
                    let newDistance = 1 + currentDistance

                    let distanceGuess = Arr2D.get distances (currentX - 1) currentY

                    if distanceGuess = 0 || newDistance < distanceGuess then
                        Arr2D.set distances (currentX - 1) currentY newDistance

            if currentY > 0 then
                if
                    not (Arr2D.get isVisited currentX (currentY - 1))
                    && isEdge nodes currentX currentY currentX (currentY - 1)
                then
                    let newDistance = 1 + currentDistance

                    let distanceGuess = Arr2D.get distances currentX (currentY - 1)

                    if distanceGuess = 0 || newDistance < distanceGuess then
                        Arr2D.set distances currentX (currentY - 1) newDistance

            if currentY < distances.Height - 1 then
                if
                    not (Arr2D.get isVisited currentX (currentY + 1))
                    && isEdge nodes currentX currentY currentX (currentY + 1)
                then
                    let newDistance = 1 + currentDistance

                    let distanceGuess = Arr2D.get distances currentX (currentY + 1)

                    if distanceGuess = 0 || newDistance < distanceGuess then
                        Arr2D.set distances currentX (currentY + 1) newDistance

            Arr2D.set isVisited currentX currentY true

            match dest with
            | Some dest when currentX = dest.X && currentY = dest.Y -> stillGoing <- false
            | _ ->
                let mutable smallestDistance = Int32.MaxValue

                for nextX = 0 to isVisited.Width - 1 do
                    for nextY = 0 to isVisited.Height - 1 do
                        if not (Arr2D.get isVisited nextX nextY) then
                            let distance = Arr2D.get distances nextX nextY

                            if distance > 0 && distance <= smallestDistance then
                                currentX <- nextX
                                currentY <- nextY
                                smallestDistance <- distance

                currentDistance <- smallestDistance

        match dest with
        | Some dest -> Arr2D.get distances dest.X dest.Y
        | None ->

        let mutable minValue = Int32.MaxValue

        for y = 0 to nodes.Height - 1 do
            for x = 0 to nodes.Width - 1 do
                if Arr2D.get nodes x y = 0uy then
                    let distance = Arr2D.get distances x y

                    if distance <> 0 then
                        minValue <- min minValue distance

        minValue

    let part1 (lines : StringSplitEnumerator) : int =
        let data, width, start, endPoint = parse lines
#if DEBUG
        let data =
            {
                Width = width
                Elements = data
            }

        let distances = Arr2D.create data.Width data.Height Int32.MaxValue
        let isVisited = Arr2D.zeroCreate<bool> data.Width data.Height

#else
        use ptr = fixed data

        let data =
            {
                Width = width
                Elements = ptr
                Length = data.Length
            }

        let distanceArr = Array.zeroCreate (data.Width * data.Height)
        use ptr = fixed distanceArr
        let distances = Arr2D.zeroCreate<int> ptr data.Width data.Height
        let visitedArr = Array.zeroCreate (data.Width * data.Height)
        use ptr = fixed visitedArr
        let isVisited = Arr2D.zeroCreate<bool> ptr data.Width data.Height
#endif

        dijkstra distances isVisited data endPoint (Some start)

    let part2 (lines : StringSplitEnumerator) : int =
        let data, width, _, endPoint = parse lines
#if DEBUG
        let data =
            {
                Width = width
                Elements = data
            }

        let distances = Arr2D.zeroCreate<int32> data.Width data.Height
        let isVisited = Arr2D.zeroCreate<bool> data.Width data.Height
#else
        use ptr = fixed data

        let data =
            {
                Width = width
                Elements = ptr
                Length = data.Length
            }

        let distanceArr = Array.zeroCreate (data.Width * data.Height)
        use ptr = fixed distanceArr
        let distances = Arr2D.zeroCreate<int32> ptr data.Width data.Height
        let visitedArr = Array.zeroCreate (data.Width * data.Height)
        use ptr = fixed visitedArr
        let isVisited = Arr2D.zeroCreate<bool> ptr data.Width data.Height
#endif

        dijkstra distances isVisited data endPoint None
