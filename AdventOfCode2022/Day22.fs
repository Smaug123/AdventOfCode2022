namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day22 =

    /// Returns the board (where 0 means "nothing here", 1 means "space", 2 means "wall"),
    /// the set of instructions (move * change-direction), and the final trailing instruction to move.
    let parse (line : StringSplitEnumerator) : int[][] * ResizeArray<struct (int * Direction)> * int =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        let mutable maxBoardLength = 0
        let row = ResizeArray ()
        // 2 here in case there's a trailing \r
        while enum.MoveNext () && enum.Current.Length >= 2 do
            for i = 0 to enum.Current.Length - 1 do
                match enum.Current.[i] with
                | ' ' -> row.Add 0
                | '.' -> row.Add 1
                | '#' -> row.Add 2
                | '\r' -> ()
                | c -> failwithf "unexpected char: %c" c

            output.Add (row.ToArray ())
            maxBoardLength <- max maxBoardLength row.Count
            row.Clear ()

        if not (enum.MoveNext ()) then
            failwith "expected instruction line"

        let directions = ResizeArray ()
        let line = enum.Current.TrimEnd ()
        let mutable i = 0

        for count = 0 to line.Length - 1 do
            if '0' <= line.[count] && line.[count] <= '9' then
                i <- i * 10 + (int line.[count] - int '0')
            else
                let dir =
                    match line.[count] with
                    | 'L' -> Direction.Left
                    | 'R' -> Direction.Right
                    | c -> failwithf "Unexpected: %c" c

                directions.Add (struct (i, dir))
                i <- 0

        let finalOutput =
            Array.init
                output.Count
                (fun i -> Array.append output.[i] (Array.zeroCreate (maxBoardLength - output.[i].Length)))

        finalOutput, directions, i

    let inline rotateRight (dir : Direction) =
        match dir with
        | Direction.Up -> Direction.Right
        | Direction.Right -> Direction.Down
        | Direction.Down -> Direction.Left
        | Direction.Left -> Direction.Up
        | _ -> failwith "bad direction"

    let inline rotateLeft (dir : Direction) =
        match dir with
        | Direction.Up -> Direction.Left
        | Direction.Right -> Direction.Up
        | Direction.Down -> Direction.Right
        | Direction.Left -> Direction.Down
        | _ -> failwith "bad direction"

    /// Returns false if we got stuck due to a wall.
    let private moveOneStep
        (currPos : MutableCoordinate)
        (direction : Direction)
        (board : int[][])
        : bool
        =
        let answer =
            {
                MutableCoordinate.X = currPos.X
                MutableCoordinate.Y = currPos.Y
            }

        match direction with
        | Direction.Up ->
            if answer.Y = 0 then
                answer.Y <- board.Length - 1
            else
                answer.Y <- answer.Y - 1

            while board.[answer.Y].[answer.X] = 0 do
                if answer.Y = 0 then
                    answer.Y <- board.Length - 1
                else
                    answer.Y <- answer.Y - 1
        | Direction.Down ->
            if answer.Y = board.Length - 1 then
                answer.Y <- 0
            else
                answer.Y <- answer.Y + 1

            while board.[answer.Y].[answer.X] = 0 do
                if answer.Y = board.Length - 1 then
                    answer.Y <- 0
                else
                    answer.Y <- answer.Y + 1
        | Direction.Left ->
            if answer.X = 0 then
                answer.X <- board.[0].Length - 1
            else
                answer.X <- answer.X - 1

            while board.[answer.Y].[answer.X] = 0 do
                if answer.X = 0 then
                    answer.X <- board.[0].Length - 1
                else
                    answer.X <- answer.X - 1
        | Direction.Right ->
            if answer.X = board.[0].Length - 1 then
                answer.X <- 0
            else
                answer.X <- answer.X + 1

            while board.[answer.Y].[answer.X] = 0 do
                if answer.X = board.[0].Length - 1 then
                    answer.X <- 0
                else
                    answer.X <- answer.X + 1
        | _ -> failwith "noooo"

        if board.[answer.Y].[answer.X] <> 2 then
            currPos.X <- answer.X
            currPos.Y <- answer.Y
            true
        else false

    let moveDistance
        (currPos : MutableCoordinate)
        (direction : Direction)
        (distance : int)
        (board : int[][])
        : unit
        =
        let mutable i = 0
        let mutable keepGoing = true
        while keepGoing && i < distance do
            keepGoing <- moveOneStep currPos direction board
            i <- i + 1

    let part1 (lines : StringSplitEnumerator) : int =
        let board, instructions, finalDistance = parse lines

        let position =
            {
                MutableCoordinate.Y = 0
                MutableCoordinate.X = Array.IndexOf (board.[0], 1)
            }

        let mutable direction = Direction.Right

        for struct (distance, rotation) in instructions do
            moveDistance position direction distance board

            direction <-
                match rotation with
                | Direction.Right -> rotateRight direction
                | Direction.Left -> rotateLeft direction
                | _ -> failwith "bad rotation"

        moveDistance position direction finalDistance board

        let finalFacing =
            match direction with
            | Direction.Right -> 0
            | Direction.Down -> 1
            | Direction.Left -> 2
            | Direction.Up -> 3
            | _ -> failwith "oh no"

        1000 * (position.Y + 1) + 4 * (position.X + 1) + finalFacing

    let part2 (lines : StringSplitEnumerator) : int =
        let original = parse lines

        -1
