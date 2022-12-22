namespace AdventOfCode2022

open System

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
    let private moveOneStep (currPos : MutableCoordinate) (direction : Direction) (board : int[][]) : bool =
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
        else
            false

    let moveDistance (currPos : MutableCoordinate) (direction : Direction) (distance : int) (board : int[][]) : unit =
        let mutable i = 0
        let mutable keepGoing = true

        while keepGoing && i < distance do
            keepGoing <- moveOneStep currPos direction board
            i <- i + 1

    let inline answer (position : Coordinate) (direction : Direction) =
        let finalFacing =
            match direction with
            | Direction.Right -> 0
            | Direction.Down -> 1
            | Direction.Left -> 2
            | Direction.Up -> 3
            | _ -> failwith "oh no"

        1000 * (position.Y + 1) + 4 * (position.X + 1) + finalFacing

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

        answer
            {
                X = position.X
                Y = position.Y
            }
            direction

    /// If we walk off the given number of face in the given direction, where do we end up?
    /// Returns an additional "true" if we need to interchange Y and X.
    let newFaceOnExample
        (cubeSize : int)
        (face : int)
        (direction : Direction)
        : int * Direction * (struct (int * int) -> struct (int * int))
        =
        match face, direction with
        | 1, Direction.Up -> 2, Direction.Down, (fun (struct (x, y)) -> struct (cubeSize - x - 1, 0))
        | 1, Direction.Down -> 4, Direction.Down, (fun (struct (x, _)) -> struct (x, 0))
        | 1, Direction.Left -> 3, Direction.Down, (fun (struct (_, y)) -> struct (y, 0))
        | 1, Direction.Right -> 6, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, cubeSize - y - 1))
        | 2, Direction.Up -> 1, Direction.Down, (fun (struct (x, y)) -> struct (cubeSize - x - 1, 0))
        | 2, Direction.Down -> 5, Direction.Up, (fun (struct (x, y)) -> struct (cubeSize - x - 1, cubeSize - 1))
        | 2, Direction.Left -> 6, Direction.Up, (fun (struct (_, y)) -> struct (cubeSize - y - 1, cubeSize - 1))
        | 2, Direction.Right -> 3, Direction.Right, (fun (struct (x, y)) -> struct (0, y))
        | 3, Direction.Up -> 1, Direction.Right, (fun (struct (x, y)) -> struct (0, x))
        | 3, Direction.Down -> 5, Direction.Right, (fun (struct (x, y)) -> struct (0, cubeSize - 1 - y))
        | 3, Direction.Left -> 2, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, y))
        | 3, Direction.Right -> 4, Direction.Right, (fun (struct (x, y)) -> struct (0, y))
        | 4, Direction.Up -> 1, Direction.Up, (fun (struct (x, _)) -> struct (x, cubeSize - 1))
        | 4, Direction.Down -> 5, Direction.Down, (fun (struct (x, _)) -> struct (x, 0))
        | 4, Direction.Left -> 3, Direction.Left, (fun (struct (x, _)) -> struct (x, cubeSize - 1))
        | 4, Direction.Right -> 6, Direction.Down, (fun (struct (_, y)) -> struct (cubeSize - 1 - y, 0))
        | 5, Direction.Up -> 4, Direction.Up, (fun (struct (x, y)) -> struct (x, cubeSize - 1))
        | 5, Direction.Down -> 2, Direction.Up, (fun (struct (x, y)) -> struct (cubeSize - x - 1, cubeSize - 1))
        | 5, Direction.Left -> 3, Direction.Up, (fun (struct (x, y)) -> struct (cubeSize - y - 1, cubeSize - 1))
        | 5, Direction.Right -> 6, Direction.Right, (fun (struct (x, y)) -> struct (0, y))
        | 6, Direction.Up -> 4, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, cubeSize - x - 1))
        | 6, Direction.Down -> 2, Direction.Right, (fun (struct (x, y)) -> failwith "TODO")
        | 6, Direction.Left -> 5, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, y))
        | 6, Direction.Right -> 1, Direction.Left, (fun (struct (x, y)) -> failwith "TODO")
        | _ -> failwith "bad face"

    let inline toArrayElementOnExample (faceSize : int) (face : int) (position : Coordinate) : Coordinate =
        match face with
        | 1 ->
            {
                X = position.X + 2 * faceSize
                Y = position.Y
            }
        | 2 ->
            {
                X = position.X
                Y = position.Y + faceSize
            }
        | 3 ->
            {
                X = position.X + faceSize
                Y = position.Y + faceSize
            }
        | 4 ->
            {
                X = position.X + 2 * faceSize
                Y = position.Y + faceSize
            }
        | 5 ->
            {
                X = position.X + 2 * faceSize
                Y = position.Y + 2 * faceSize
            }
        | 6 ->
            {
                X = position.X + 3 * faceSize
                Y = position.Y + 2 * faceSize
            }
        | _ -> failwith "bad face"

    /// Returns false if we got stuck due to a wall.
    /// The position is referring to the position within the given face.
    let inline private moveOneStepCube
        (cubeSize : int)
        ([<InlineIfLambda>] toArrayElement : int -> Coordinate -> Coordinate)
        ([<InlineIfLambda>] newFace : int -> Direction -> int * Direction * (struct (int * int) -> struct (int * int)))
        (currFace : byref<int>)
        (currPos : MutableCoordinate)
        (direction : byref<Direction>)
        (board : int[][])
        : bool
        =
        // If we do walk off this face, where do we end up?
        let faceAfterSpill, directionAfterSpill, transformPosition =
            newFace currFace direction

        let intendedDest, face, newDirection =
            match direction with
            | Direction.Up ->
                if currPos.Y = 0 then
                    let struct (x, y) = transformPosition (struct (currPos.X, currPos.Y))

                    {
                        X = x
                        Y = y
                    },
                    faceAfterSpill,
                    directionAfterSpill
                else
                    {
                        X = currPos.X
                        Y = currPos.Y - 1
                    },
                    currFace,
                    direction
            | Direction.Down ->
                if currPos.Y = cubeSize - 1 then
                    let struct (x, y) = transformPosition (struct (currPos.X, currPos.Y))

                    {
                        X = x
                        Y = y
                    },
                    faceAfterSpill,
                    directionAfterSpill
                else
                    {
                        X = currPos.X
                        Y = currPos.Y + 1
                    },
                    currFace,
                    direction
            | Direction.Left ->
                if currPos.X = 0 then
                    let struct (x, y) = transformPosition (struct (currPos.X, currPos.Y))

                    {
                        X = x
                        Y = y
                    },
                    faceAfterSpill,
                    directionAfterSpill
                else
                    {
                        X = currPos.X - 1
                        Y = currPos.Y
                    },
                    currFace,
                    direction
            | Direction.Right ->
                if currPos.X = cubeSize - 1 then
                    let struct (x, y) = transformPosition (struct (currPos.X, currPos.Y))

                    {
                        X = x
                        Y = y
                    },
                    faceAfterSpill,
                    directionAfterSpill
                else
                    {
                        X = currPos.X + 1
                        Y = currPos.Y
                    },
                    currFace,
                    direction
            | _ -> failwith "noooo"

        let pos = toArrayElement face intendedDest

        if board.[pos.Y].[pos.X] <> 2 then
            currPos.X <- intendedDest.X
            currPos.Y <- intendedDest.Y
            currFace <- face
            direction <- newDirection
            true
        else
            false

    let inline moveCubeDistance
        (cubeSize : int)
        ([<InlineIfLambda>] toArrayElement : int -> Coordinate -> Coordinate)
        ([<InlineIfLambda>] newFace : int -> Direction -> int * Direction * (struct (int * int) -> struct (int * int)))
        (currFace : byref<int>)
        (currPos : MutableCoordinate)
        (direction : byref<Direction>)
        (distance : int)
        (board : int[][])
        : unit
        =
        let mutable i = 0
        let mutable keepGoing = true

        while keepGoing && i < distance do
            keepGoing <- moveOneStepCube cubeSize toArrayElement newFace &currFace currPos &direction board
            i <- i + 1

    let part2Example (lines : StringSplitEnumerator) : int =
        let board, instructions, finalDistance = parse lines
        let faceSize = 4

        let position =
            {
                MutableCoordinate.X = 0
                MutableCoordinate.Y = 0
            }

        let mutable direction = Direction.Right
        let mutable face = 1

        for struct (distance, rotation) in instructions do
            moveCubeDistance
                faceSize
                (toArrayElementOnExample faceSize)
                (newFaceOnExample faceSize)
                &face
                position
                &direction
                distance
                board

            direction <-
                match rotation with
                | Direction.Right -> rotateRight direction
                | Direction.Left -> rotateLeft direction
                | _ -> failwith "bad rotation"

        moveCubeDistance
            faceSize
            (toArrayElementOnExample faceSize)
            (newFaceOnExample faceSize)
            &face
            position
            &direction
            finalDistance
            board

        let faceDimension = board.Length / 3

        let position =
            toArrayElementOnExample
                faceDimension
                face
                {
                    X = position.X
                    Y = position.Y
                }

        answer position direction

    // The real thing has shape:
    // _12
    // _3_
    // 45_
    // 6__

    let inline toArrayElement (faceSize : int) (face : int) (position : Coordinate) : Coordinate =
        match face with
        | 1 ->
            {
                X = position.X + faceSize
                Y = position.Y
            }
        | 2 ->
            {
                X = position.X + 2 * faceSize
                Y = position.Y
            }
        | 3 ->
            {
                X = position.X + faceSize
                Y = position.Y + faceSize
            }
        | 4 ->
            {
                X = position.X
                Y = position.Y + 2 * faceSize
            }
        | 5 ->
            {
                X = position.X + faceSize
                Y = position.Y + 2 * faceSize
            }
        | 6 ->
            {
                X = position.X
                Y = position.Y + 3 * faceSize
            }
        | _ -> failwith "bad face"

    let inline newFace
        (cubeSize : int)
        (face : int)
        (direction : Direction)
        : int * Direction * (struct (int * int) -> struct (int * int))
        =
        match face, direction with
        | 1, Direction.Up -> 6, Direction.Right, (fun (struct (x, y)) -> struct (0, x))
        | 1, Direction.Down -> 3, Direction.Down, (fun (struct (x, _)) -> struct (x, 0))
        | 1, Direction.Left -> 4, Direction.Right, (fun (struct (x, y)) -> struct (0, cubeSize - 1 - y))
        | 1, Direction.Right -> 2, Direction.Right, (fun (struct (x, y)) -> struct (0, y))
        | 2, Direction.Up -> 6, Direction.Up, (fun (struct (x, y)) -> (x, cubeSize - 1))
        | 2, Direction.Down -> 3, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, x))
        | 2, Direction.Left -> 1, Direction.Left, (fun (struct (_, y)) -> struct (cubeSize - 1, y))
        | 2, Direction.Right -> 5, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, cubeSize - 1 - y))
        | 3, Direction.Up -> 1, Direction.Up, (fun (struct (x, y)) -> struct (x, cubeSize - 1))
        | 3, Direction.Down -> 5, Direction.Down, (fun (struct (x, y)) -> struct (x, 0))
        | 3, Direction.Left -> 4, Direction.Down, (fun (struct (x, y)) -> struct (y, 0))
        | 3, Direction.Right -> 2, Direction.Up, (fun (struct (x, y)) -> struct (y, cubeSize - 1))
        | 4, Direction.Up -> 3, Direction.Right, (fun (struct (x, _)) -> struct (0, x))
        | 4, Direction.Down -> 6, Direction.Down, (fun (struct (x, _)) -> struct (x, 0))
        | 4, Direction.Left -> 1, Direction.Right, (fun (struct (x, y)) -> struct (0, cubeSize - 1 - y))
        | 4, Direction.Right -> 5, Direction.Right, (fun (struct (_, y)) -> struct (0, y))
        | 5, Direction.Up -> 3, Direction.Up, (fun (struct (x, y)) -> struct (x, cubeSize - 1))
        | 5, Direction.Down -> 6, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, x))
        | 5, Direction.Left -> 4, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, y))
        | 5, Direction.Right -> 2, Direction.Left, (fun (struct (x, y)) -> struct (cubeSize - 1, cubeSize - 1 - y))
        | 6, Direction.Up -> 4, Direction.Up, (fun (struct (x, y)) -> struct (x, cubeSize - 1))
        | 6, Direction.Down -> 2, Direction.Down, (fun (struct (x, y)) -> struct (x, 0))
        | 6, Direction.Left -> 1, Direction.Down, (fun (struct (x, y)) -> struct (y, 0))
        | 6, Direction.Right -> 5, Direction.Up, (fun (struct (x, y)) -> struct (y, cubeSize - 1))
        | _ -> failwith "bad face"

    let part2 (lines : StringSplitEnumerator) : int =
        let board, instructions, finalDistance = parse lines
        let faceSize = board.[0].Length / 3

        let position =
            {
                MutableCoordinate.X = 0
                MutableCoordinate.Y = 0
            }

        let mutable direction = Direction.Right
        let mutable face = 1

        for struct (distance, rotation) in instructions do
            moveCubeDistance
                faceSize
                (toArrayElement faceSize)
                (newFace faceSize)
                &face
                position
                &direction
                distance
                board

            direction <-
                match rotation with
                | Direction.Right -> rotateRight direction
                | Direction.Left -> rotateLeft direction
                | _ -> failwith "bad rotation"

        moveCubeDistance
            faceSize
            (toArrayElement faceSize)
            (newFace faceSize)
            &face
            position
            &direction
            finalDistance
            board

        let position =
            toArrayElement
                faceSize
                face
                {
                    X = position.X
                    Y = position.Y
                }

        answer position direction
