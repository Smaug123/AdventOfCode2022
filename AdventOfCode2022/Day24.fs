namespace AdventOfCode2022

open System
open Microsoft.FSharp.NativeInterop

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

    let moveBlizzards (width : int) (height : int) (board : Arr2D<Byte>) : unit =
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

        // TODO do this by modifying in place without allocation instead
#if DEBUG
        Array.Copy (resultArr, board.Elements)
#else
        NativePtr.copyBlock board.Elements ptr board.Length
#endif

    let inline coordToInt' (width : int) (x : int) (y : int) : int = x + y * width
    let inline coordToInt (width : int) (coord : Coordinate) : int = coordToInt' width coord.X coord.Y

    let inline intToCoord (width : int) (coord : int) : Coordinate =
        let x = coord % width

        {
            X = x
            Y = (coord - x) / width
        }

    /// The buffer is an array of at least 5 Coordinates, except it's had coordToInt called on it.
    let inline populateAvailableMoves
        (buffer : int[])
        (width : int)
        (height : int)
        (current : Coordinate)
        (board : Arr2D<byte>)
        : int
        =
        let mutable bufLen = 0

        if current.X > 1 && current.Y <> 0 && current.Y <> height - 1 then
            if Arr2D.get board (current.X - 1) current.Y = 0uy then
                buffer.[bufLen] <- coordToInt' width (current.X - 1) current.Y
                bufLen <- bufLen + 1

        if Arr2D.get board current.X current.Y = 0uy then
            buffer.[bufLen] <- coordToInt width current
            bufLen <- bufLen + 1

        if current.X < width - 2 && current.Y <> 0 then
            if Arr2D.get board (current.X + 1) current.Y = 0uy then
                buffer.[bufLen] <- coordToInt' width (current.X + 1) current.Y
                bufLen <- bufLen + 1

        if current.Y > 1 && current.X <> 0 then

            if Arr2D.get board current.X (current.Y - 1) = 0uy then
                buffer.[bufLen] <- coordToInt' width current.X (current.Y - 1)
                bufLen <- bufLen + 1

        if current.Y < height - 2 && current.X <> 0 then
            if Arr2D.get board current.X (current.Y + 1) = 0uy then
                buffer.[bufLen] <- coordToInt' width current.X (current.Y + 1)
                bufLen <- bufLen + 1

        bufLen

    let inline goFrom
        (start : Coordinate)
        (dest : Coordinate)
        (width : int)
        (height : int)
        (board : Arr2D<byte>)
        (timeStep : int)
        =
        let mutable buffer = ResizeArray ()
        let movesBuffer = Array.zeroCreate 5

        let dest = coordToInt width dest

        let rec go (timeStep : int) (toExplore : int ResizeArray) =
            moveBlizzards width height board

            if toExplore.Contains dest then
                timeStep + 1
            else

            buffer.Clear ()

            for currPos in toExplore do
                let bufLen =
                    populateAvailableMoves movesBuffer width height (intToCoord width currPos) board

                for move = 0 to bufLen - 1 do
                    let move = movesBuffer.[move]

                    if not (buffer.Contains move) then
                        buffer.Add move

            let continueWith = buffer
            buffer <- toExplore

            go (timeStep + 1) continueWith

        let set = ResizeArray ()

        coordToInt width start |> set.Add

        go timeStep set

    let goToEnd width height =
        goFrom
            {
                X = 1
                Y = 0
            }
            {
                X = width - 2
                Y = height - 2
            }
            width
            height

    let goToStart width height =
        goFrom
            {
                X = width - 2
                Y = height - 1
            }
            {
                X = 1
                Y = 1
            }
            width
            height

    let part1 (lines : StringSplitEnumerator) : int =
        let board, width, height = parse lines
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

        goToEnd width height board 0

    let part2 (lines : StringSplitEnumerator) : int =
        let board, width, height = parse lines
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

        let toEnd = goToEnd width height board 0
        let backToStart = goToStart width height board toEnd
        goToEnd width height board backToStart
