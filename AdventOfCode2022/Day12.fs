namespace AdventOfCode2022

open System

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day12 =

    let private charToByte (c : char) = byte c - byte 'a'

    [<Struct>]
    type Coordinate =
        {
            X : int
            Y : int
        }

    let parse (lines : StringSplitEnumerator) : Arr2D<byte> * Coordinate * Coordinate =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable startPos = Unchecked.defaultof<Coordinate>
        let mutable endPos = Unchecked.defaultof<Coordinate>
        let mutable row = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let current = enum.Current.TrimEnd ()
                let arr = Array.zeroCreate current.Length

                for i in 0 .. arr.Length - 1 do
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

        let output = Arr2D.init output.[0].Length output.Count (fun x y -> output.[y].[x])
        output, startPos, endPos

    /// The input arrays must all have the same dimensions.
    /// `nodes` will not be mutated; `distances` and `isVisited` will be mutated.
    /// (As a result of these arguments, `dijkstra` is intended to be allocation-free.)
    let dijkstra
        (distances : Arr2D<int>)
        (isVisited : Arr2D<bool>)
        (nodes : Arr2D<byte>)
        (start : Coordinate)
        (dest : Coordinate)
        =
        let mutable currentX = start.X
        let mutable currentY = start.Y
        let mutable currentDistance = 0

        Arr2D.clear isVisited
        Arr2D.setAll distances Int32.MaxValue
        Arr2D.set distances start.X start.Y 0
        let mutable stillGoing = true

        while stillGoing && currentDistance < Int32.MaxValue do
            let currentNode = Arr2D.get nodes currentX currentY

            if currentX < distances.Width - 1 then
                if
                    not (Arr2D.get isVisited (currentX + 1) currentY)
                    && Arr2D.get nodes (currentX + 1) currentY <= 1uy + currentNode
                then
                    let newDistance = 1 + currentDistance

                    if newDistance < Arr2D.get distances (currentX + 1) currentY then
                        Arr2D.set distances (currentX + 1) currentY newDistance

            if currentX > 0 then
                if
                    not (Arr2D.get isVisited (currentX - 1) currentY)
                    && Arr2D.get nodes (currentX - 1) currentY <= 1uy + currentNode
                then
                    let newDistance = 1 + currentDistance

                    if newDistance < Arr2D.get distances (currentX - 1) currentY then
                        Arr2D.set distances (currentX - 1) currentY newDistance

            if currentY > 0 then
                if
                    not (Arr2D.get isVisited currentX (currentY - 1))
                    && Arr2D.get nodes currentX (currentY - 1) <= 1uy + currentNode
                then
                    let newDistance = 1 + currentDistance

                    if newDistance < Arr2D.get distances currentX (currentY - 1) then
                        Arr2D.set distances currentX (currentY - 1) newDistance

            if currentY < distances.Height - 1 then
                if
                    not (Arr2D.get isVisited currentX (currentY + 1))
                    && Arr2D.get nodes currentX (currentY + 1) <= 1uy + currentNode
                then
                    let newDistance = 1 + currentDistance

                    if newDistance < Arr2D.get distances currentX (currentY + 1) then
                        Arr2D.set distances currentX (currentY + 1) newDistance

            Arr2D.set isVisited currentX currentY true

            if currentX = dest.X && currentY = dest.Y then
                stillGoing <- false
            else
                let mutable smallestDistance = Int32.MaxValue

                for nextX in 0 .. isVisited.Width - 1 do
                    for nextY in 0 .. isVisited.Height - 1 do
                        if
                            not (Arr2D.get isVisited nextX nextY)
                            && Arr2D.get distances nextX nextY <= smallestDistance
                        then
                            currentX <- nextX
                            currentY <- nextY
                            smallestDistance <- Arr2D.get distances nextX nextY

                currentDistance <- smallestDistance

        Arr2D.get distances dest.X dest.Y

    let part1 (lines : StringSplitEnumerator) : int64 =
        let data, start, endPoint = parse lines
        let distances = Arr2D.create data.Width data.Height Int32.MaxValue
        let isVisited = Arr2D.zeroCreate<bool> data.Width data.Height

        dijkstra distances isVisited data start endPoint

    let part2 (lines : StringSplitEnumerator) : int =
        let data, _, endPoint = parse lines
        let mutable best = Int32.MaxValue
        let distances = Arr2D.zeroCreate<int32> data.Width data.Height
        let isVisited = Arr2D.zeroCreate<bool> data.Width data.Height

        for y in 0 .. data.Height - 1 do
            for x in 0 .. data.Width - 1 do
                if Arr2D.get data x y = 0uy then
                    let coord =
                        {
                            X = x
                            Y = y
                        }

                    let d = dijkstra distances isVisited data coord endPoint

                    if d < best then
                        best <- d

        best
