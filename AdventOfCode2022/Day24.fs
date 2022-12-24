namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day24 =

    // byte % 2 is whether Up is in;
    // byte % 4 is whether Down is in;
    // byte % 8 is whether Left is in;
    // byte % 16 is whether Right is in.
    // This is logically a 2D array, but without having to give up ownership.
    type Day24Board = byte[]

    /// Returns the width and the height too. The resulting array is suitable to become an Arr2D.
    let parse (line : StringSplitEnumerator) : byte[] * int * int =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()
        let mutable y = 0
        let mutable width = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable x = 0

                for c in enum.Current.TrimEnd () do
                    match c with
                    | '>' -> output.Add 8uy
                    | '^' -> output.Add 1uy
                    | 'v' -> output.Add 2uy
                    | '<' -> output.Add 4uy
                    | '.'
                    | '#' -> output.Add 0uy
                    | _ -> failwithf "unexpected char: %c" c

                    x <- x + 1

                width <- x

                y <- y + 1

        output.ToArray (), width, y

    let moveBlizzards (width : int) (height : int) (board : Day24Board) : Day24Board =
#if DEBUG
        let board =
            {
                Elements = board
                Width = width
            }
#else
        use boardPtr = fixed board

        let board =
            {
                Elements = boardPtr
                Width = width
                Length = width * height
            }
#endif

        let resultArr = Array.zeroCreate<byte> (width * height)
#if DEBUG
        let result =
            {
                Elements = resultArr
                Width = width
            }
#else
        use ptr = fixed resultArr

        let result =
            {
                Elements = ptr
                Width = width
                Length = resultArr.Length
            }
#endif

        for y = 1 to height - 2 do
            for x = 1 to width - 2 do
                let directions = Arr2D.get board x y

                if directions % 2uy = 1uy then
                    let y = if y = 1 then height - 2 else y - 1
                    let prev = Arr2D.get result x y
                    Arr2D.set result x y (prev + 1uy)

                if (directions / 2uy) % 2uy = 1uy then
                    let y = if y = height - 2 then 1 else y + 1
                    let prev = Arr2D.get result x y
                    Arr2D.set result x y (prev + 2uy)

                if (directions / 4uy) % 2uy = 1uy then
                    let x = if x = 1 then width - 2 else x - 1
                    let prev = Arr2D.get result x y
                    Arr2D.set result x y (prev + 4uy)

                if (directions / 8uy) % 2uy = 1uy then
                    let x = if x = width - 2 then 1 else x + 1
                    let prev = Arr2D.get result x y
                    Arr2D.set result x y (prev + 8uy)

        resultArr

    let inline boardAtTime (width : int) (height : int) (startBoard : Day24Board) : int -> Day24Board =
        let store = ResizeArray ()
        store.Add startBoard

        fun day ->
            if store.Count > day then
                store.[day]
            else
                for i = store.Count to day do
                    store.Add (moveBlizzards width height store.[i - 1])

                store.[day]

    let availableIndividualMoves
        (buffer : Coordinate[])
        (width : int)
        (height : int)
        (current : Coordinate)
        (board : Arr2D<byte>)
        : int
        =
        let mutable bufLen = 0

        if current.X > 1 && current.Y <> 0 && current.Y <> height - 1 then
            if Arr2D.get board (current.X - 1) current.Y = 0uy then
                let trial =
                    { current with
                        X = current.X - 1
                    }

                buffer.[bufLen] <- trial
                bufLen <- bufLen + 1

        if Arr2D.get board current.X current.Y = 0uy then
            buffer.[bufLen] <- current
            bufLen <- bufLen + 1

        if current.X < width - 2 && current.Y <> 0 then
            if Arr2D.get board (current.X + 1) current.Y = 0uy then
                let trial =
                    { current with
                        X = current.X + 1
                    }

                buffer.[bufLen] <- trial
                bufLen <- bufLen + 1

        if current.Y > 1 && current.X <> 0 then

            if Arr2D.get board current.X (current.Y - 1) = 0uy then
                let trial =
                    { current with
                        Y = current.Y - 1
                    }

                buffer.[bufLen] <- trial
                bufLen <- bufLen + 1

        if current.Y < height - 2 && current.X <> 0 then
            if Arr2D.get board current.X (current.Y + 1) = 0uy then
                let trial =
                    { current with
                        Y = current.Y + 1
                    }

                buffer.[bufLen] <- trial
                bufLen <- bufLen + 1

        bufLen

    let inline populateAvailableMoves
        (width : int)
        (height : int)
        ([<InlineIfLambda>] boardAtTime : int -> Day24Board)
        (buffer : Coordinate[])
        (timeStep : int)
        (currPos : Coordinate)
        : int
        =
        let board = boardAtTime (timeStep + 1)

#if DEBUG
        let board =
            {
                Elements = board
                Width = width
            }
#else
        use ptr = fixed board

        let board =
            {
                Elements = ptr
                Width = width
                Length = width * height
            }
#endif

        availableIndividualMoves buffer width height currPos board

    let inline coordToInt (width : int) (coord : Coordinate) : int = coord.X + coord.Y * width

    let inline intToCoord (width : int) (coord : int) : Coordinate =
        let x = coord % width

        {
            X = x
            Y = (coord - x) / width
        }

    let inline goToEnd
        (width : int)
        (height : int)
        ([<InlineIfLambda>] populateAvailableMoves : Coordinate[] -> int -> Coordinate -> int)
        (timeStep : int)
        =
        let mutable buffer = ResizeArray ()
        let movesBuffer = Array.zeroCreate<Coordinate> 5

        let dest =
            {
                X = width - 2
                Y = height - 2
            }
            |> coordToInt width

        let rec go (timeStep : int) (toExplore : int ResizeArray) =
            if toExplore.Contains dest then
                timeStep + 1
            else

            buffer.Clear ()

            for currPos in toExplore do
                let bufLen = populateAvailableMoves movesBuffer timeStep (intToCoord width currPos)

                for move = 0 to bufLen - 1 do
                    let move = coordToInt width movesBuffer.[move]

                    if not (buffer.Contains move) then
                        buffer.Add move

            let continueWith = buffer
            buffer <- toExplore

            go (timeStep + 1) continueWith

        let set = ResizeArray ()

        {
            X = 1
            Y = 0
        }
        |> coordToInt width
        |> set.Add

        go timeStep set

    let inline goToStart
        (width : int)
        (height : int)
        ([<InlineIfLambda>] populateAvailableMoves : Coordinate[] -> int -> Coordinate -> int)
        (timeStep : int)
        =
        let mutable buffer = ResizeArray ()
        let availableMovesBuffer = Array.zeroCreate<Coordinate> 5

        let dest =
            {
                X = 1
                Y = 1
            }
            |> coordToInt width

        let rec go (timeStep : int) (toExplore : int ResizeArray) =
            if toExplore.Contains dest then
                timeStep + 1
            else

            buffer.Clear ()

            for currPos in toExplore do
                let bufLen =
                    populateAvailableMoves availableMovesBuffer timeStep (intToCoord width currPos)

                for move = 0 to bufLen - 1 do
                    let move = coordToInt width availableMovesBuffer.[move]

                    if not (buffer.Contains move) then
                        buffer.Add move

            let continueWith = buffer
            buffer <- toExplore

            go (timeStep + 1) continueWith

        let arr = ResizeArray ()

        {
            X = width - 2
            Y = height - 1
        }
        |> coordToInt width
        |> arr.Add

        go timeStep arr

    let part1 (lines : StringSplitEnumerator) : int =
        let board, width, height = parse lines

        let boardAtTime = boardAtTime width height board
        let availableMoves = populateAvailableMoves width height boardAtTime

        goToEnd width height availableMoves 0

    let part2 (lines : StringSplitEnumerator) : int =
        let board, width, height = parse lines

        let boardAtTime = boardAtTime width height board
        let availableMoves = populateAvailableMoves width height boardAtTime

        let toEnd = goToEnd width height availableMoves 0
        let backToStart = goToStart width height availableMoves toEnd
        goToEnd width height availableMoves backToStart
