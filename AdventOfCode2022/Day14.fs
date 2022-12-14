namespace AdventOfCode2022

open System

#if DEBUG
open System.Runtime.InteropServices
open Checked
#endif

#if DEBUG
#else
#nowarn "9"
#endif

type Day14Shape = Coordinate ResizeArray

[<RequireQualifiedAccess>]
type MutableCoordinate =
    {
        mutable X : int
        mutable Y : int
    }

[<RequireQualifiedAccess>]
module Day14 =

    let parse (lines : StringSplitEnumerator) : Day14Shape ResizeArray =
        use mutable enum = lines
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let line = enum.Current.TrimEnd ()
                let thisLine = ResizeArray ()
                let mutable components = StringSplitEnumerator.make' ' ' line

                while components.MoveNext () do
                    let mutable coords = StringSplitEnumerator.make' ',' components.Current
                    let x = StringSplitEnumerator.consumeInt &coords
                    let y = StringSplitEnumerator.consumeInt &coords

                    {
                        Coordinate.X = x
                        Y = y
                    }
                    |> thisLine.Add

                    // the component '->'
                    components.MoveNext () |> ignore

                output.Add thisLine

        output

    let setLine (point1 : Coordinate) (point2 : Coordinate) (arr : Arr2D<bool>) : unit =
        if point1.X = point2.X then
            for y = min point1.Y point2.Y to max point1.Y point2.Y do
                Arr2D.set arr point1.X y true
        else
            assert (point1.Y = point2.Y)

            for x = min point1.X point2.X to max point1.X point2.X do
                Arr2D.set arr x point1.Y true

    let inline fallOnce (sandPos : MutableCoordinate) (arr : Arr2D<bool>) : bool =
        if not (Arr2D.get arr sandPos.X (sandPos.Y + 1)) then
            sandPos.Y <- sandPos.Y + 1
            true
        elif not (Arr2D.get arr (sandPos.X - 1) (sandPos.Y + 1)) then
            sandPos.X <- sandPos.X - 1
            sandPos.Y <- sandPos.Y + 1
            true
        elif not (Arr2D.get arr (sandPos.X + 1) (sandPos.Y + 1)) then
            sandPos.X <- sandPos.X + 1
            sandPos.Y <- sandPos.Y + 1
            true
        else
            false

    let part1 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        let minX = data |> Seq.concat |> Seq.map (fun s -> s.X) |> Seq.min
        let maxX = data |> Seq.concat |> Seq.map (fun s -> s.X) |> Seq.max
        let maxY = data |> Seq.concat |> Seq.map (fun s -> s.Y) |> Seq.max

#if DEBUG
        let arr = Arr2D.zeroCreate<bool> (maxX + 1) (maxY + 1)
#else
        let board = Array.zeroCreate ((maxX + 1) * (maxY + 1))
        use ptr = fixed board
        let arr = Arr2D.zeroCreate<bool> ptr (maxX + 1) (maxY + 1)
#endif
        for line in data do
            for i = 0 to line.Count - 2 do
                setLine line.[i] line.[i + 1] arr

        let mutable sand = 0
        let mutable keepGoing = true

        while keepGoing do
            let sandPos =
                {
                    MutableCoordinate.X = 500
                    MutableCoordinate.Y = 0
                }

            let mutable stillFalling = true

            while stillFalling && sandPos.X >= minX && sandPos.X <= maxX && sandPos.Y < maxY do
                let movedDown = fallOnce sandPos arr

                if not movedDown then
                    stillFalling <- false
                    sand <- sand + 1
                    Arr2D.set arr sandPos.X sandPos.Y true

            if stillFalling then
                keepGoing <- false

        sand

    let part2 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        let maxX = data |> Seq.concat |> Seq.map (fun s -> s.X) |> Seq.max
        let maxY = data |> Seq.concat |> Seq.map (fun s -> s.Y) |> Seq.max |> ((+) 2)

#if DEBUG
        let arr = Arr2D.zeroCreate<bool> (maxX + 1001) (maxY + 1)
#else
        let board = Array.zeroCreate ((maxX + 1001) * (maxY + 1))
        use ptr = fixed board
        let arr = Arr2D.zeroCreate<bool> ptr (maxX + 1001) (maxY + 1)
#endif
        for line in data do
            for i = 0 to line.Count - 2 do
                let point1 =
                    {
                        Coordinate.X = line.[i].X + 500
                        Y = line.[i].Y
                    }

                let point2 =
                    {
                        Coordinate.X = line.[i + 1].X + 500
                        Y = line.[i + 1].Y
                    }

                setLine point1 point2 arr

        setLine
            {
                X = 0
                Y = maxY
            }
            {
                X = maxX + 1000
                Y = maxY
            }
            arr

        let mutable sand = 0
        let mutable keepGoing = true

        while keepGoing do
            let sandPos =
                {
                    MutableCoordinate.X = 1000
                    MutableCoordinate.Y = 0
                }

            let mutable stillFalling = true

            while stillFalling && sandPos.Y < maxY do
                let movedDown = fallOnce sandPos arr

                if not movedDown then
                    sand <- sand + 1
                    Arr2D.set arr sandPos.X sandPos.Y true

                    if sandPos.X = 1000 && sandPos.Y = 0 then
                        keepGoing <- false

                    stillFalling <- false

        sand
