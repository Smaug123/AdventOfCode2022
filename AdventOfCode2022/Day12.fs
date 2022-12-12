namespace AdventOfCode2022

open System.Collections.Generic
open System

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day12 =

    let private charToByte (c : char) = byte c - byte 'a'

    let parse (lines : StringSplitEnumerator) : byte[] ResizeArray * struct (byte * byte) * struct (byte * byte) =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable startPos = struct (0uy, 0uy)
        let mutable endPos = struct (0uy, 0uy)
        let mutable row = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let current = enum.Current.TrimEnd ()
                let arr = Array.zeroCreate current.Length

                for i in 0 .. arr.Length - 1 do
                    if current.[i] = 'S' then
                        startPos <- struct (byte i, byte row)
                        arr.[i] <- 0uy
                    elif current.[i] = 'E' then
                        endPos <- struct (byte i, byte row)
                        arr.[i] <- 25uy
                    else
                        arr.[i] <- charToByte current.[i]

                arr |> output.Add
                row <- row + 1

        output, startPos, endPos

    let dijkstra
        (nodes : byte[] ResizeArray)
        (struct (startY : byte, startX : byte))
        (struct (destY : byte, destX : byte))
        =
        let startY, startX = int startX, int startY
        let destY, destX = int destX, int destY

        let mutable currentY = startY
        let mutable currentX = startX
        let mutable currentDistance = 0

        let distances : int[][] =
            Array.init nodes.Count (fun _ -> Array.create nodes.[0].Length Int32.MaxValue)

        let isVisited : bool[][] =
            Array.init nodes.Count (fun _ -> Array.zeroCreate nodes.[0].Length)

        distances.[startY].[startX] <- 0
        let mutable stillGoing = true

        while stillGoing && currentDistance < Int32.MaxValue do
            if currentX > 0 then
                if not isVisited.[currentY].[currentX - 1] && nodes.[currentY].[currentX - 1] <= 1uy + nodes.[currentY].[currentX] then
                    let newDistance = 1 + currentDistance

                    if newDistance < distances.[currentY].[currentX - 1] then
                        distances.[currentY].[currentX - 1] <- newDistance

            if currentX < distances.[currentY].Length - 1 then
                if not isVisited.[currentY].[currentX + 1] && nodes.[currentY].[currentX + 1] <= 1uy + nodes.[currentY].[currentX] then
                    let newDistance = 1 + currentDistance

                    if newDistance < distances.[currentY].[currentX + 1] then
                        distances.[currentY].[currentX + 1] <- newDistance

            if currentY < distances.Length - 1 then
                if not isVisited.[currentY + 1].[currentX] && nodes.[currentY + 1].[currentX] <= 1uy + nodes.[currentY].[currentX] then
                    let newDistance = 1 + currentDistance

                    if newDistance < distances.[currentY + 1].[currentX] then
                        distances.[currentY + 1].[currentX] <- newDistance


            if currentY > 0 then
                if not isVisited.[currentY - 1].[currentX] && nodes.[currentY - 1].[currentX] <= 1uy + nodes.[currentY].[currentX] then
                    let newDistance = 1 + currentDistance

                    if newDistance < distances.[currentY - 1].[currentX] then
                        distances.[currentY - 1].[currentX] <- newDistance

            isVisited.[currentY].[currentX] <- true

            if currentY = destY && currentX = destX then
                stillGoing <- false
            else
                let mutable smallestDistance = Int32.MaxValue

                for nextY in 0 .. isVisited.Length - 1 do
                    for nextX in 0 .. isVisited.[0].Length - 1 do
                        if not isVisited.[nextY].[nextX] && distances.[nextY].[nextX] <= smallestDistance then
                            currentY <- nextY
                            currentX <- nextX
                            smallestDistance <- distances.[nextY].[nextX]
                currentDistance <- smallestDistance

        distances.[destY].[destX]

    let part1 (lines : StringSplitEnumerator) : int64 =
        let data, start, endPoint = parse lines
        dijkstra data start endPoint

    let part2 (lines : StringSplitEnumerator) : int =
        let data, _, endPoint = parse lines
        let mutable best = Int32.MaxValue
        for i in 0..data.Count - 1 do
            for j in 0..data.[0].Length - 1 do
                if data.[i].[j] = 0uy then
                    let d = dijkstra data (struct(byte j, byte i)) endPoint
                    if d < best then
                        best <- d
        best
