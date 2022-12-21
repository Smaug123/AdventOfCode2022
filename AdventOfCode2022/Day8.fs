namespace AdventOfCode2022

open System

#if DEBUG
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day8 =

    let parse (lines : StringSplitEnumerator) : byte array * int =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable lineCount = 0

        for line in enum do
            let line = line.TrimEnd ()

            if not (line.IsWhiteSpace ()) then
                lineCount <- lineCount + 1

                for c in line do
                    output.Add (byte c - byte '0')

        output.ToArray (), lineCount

    let isVisible (board : Arr2D<byte>) (x : int) (y : int) : bool =
        // From the left?
        let mutable isVisible = true
        let mutable i = 0

        while i < x && isVisible do
            if Arr2D.get board i y >= Arr2D.get board x y then
                isVisible <- false

            i <- i + 1

        if isVisible then
            true
        else

        // From the right?
        let mutable isVisible = true
        let mutable i = board.Height - 1

        while i > x && isVisible do
            if Arr2D.get board i y >= Arr2D.get board x y then
                isVisible <- false

            i <- i - 1

        if isVisible then
            true
        else

        // From the top?
        let mutable isVisible = true
        let mutable i = 0

        while i < y && isVisible do
            if Arr2D.get board x i >= Arr2D.get board x y then
                isVisible <- false

            i <- i + 1

        if isVisible then
            true
        else

        // From the bottom?
        let mutable isVisible = true
        let mutable i = board.Width - 1

        while i > y && isVisible do
            if Arr2D.get board x i >= Arr2D.get board x y then
                isVisible <- false

            i <- i - 1

        isVisible

    let part1 (lines : StringSplitEnumerator) : int =
        let board, height = parse lines
#if DEBUG
        let board =
            {
                Arr2D.Elements = board
                Width = board.Length / height
            }
#else
        use p = fixed board

        let board =
            {
                Arr2D.Elements = p
                Length = board.Length
                Width = board.Length / height
            }
#endif

        let mutable visibleCount = 0

        for y = 0 to board.Height - 1 do
            for x = 0 to board.Width - 1 do
                if isVisible board x y then
                    visibleCount <- visibleCount + 1

        visibleCount

    let scenicScore (board : Arr2D<byte>) (x : int) (y : int) : int =
        let mutable scenicCount = 0

        do
            let mutable isVisible = true
            let mutable i = y - 1

            while i >= 0 && isVisible do
                if Arr2D.get board x i >= Arr2D.get board x y then
                    isVisible <- false

                scenicCount <- scenicCount + 1

                i <- i - 1

        do
            let mutable isVisible = true
            let mutable i = y + 1
            let mutable subCount = 0

            while i < board.Height && isVisible do
                if Arr2D.get board x i >= Arr2D.get board x y then
                    isVisible <- false

                subCount <- subCount + 1

                i <- i + 1

            scenicCount <- scenicCount * subCount

        do
            let mutable isVisible = true
            let mutable i = x - 1
            let mutable subCount = 0

            while i >= 0 && isVisible do
                if Arr2D.get board i y >= Arr2D.get board x y then
                    isVisible <- false

                subCount <- subCount + 1

                i <- i - 1

            scenicCount <- scenicCount * subCount

        do
            let mutable isVisible = true
            let mutable i = x + 1
            let mutable subCount = 0

            while i < board.Width && isVisible do
                if Arr2D.get board i y >= Arr2D.get board x y then
                    isVisible <- false

                subCount <- subCount + 1

                i <- i + 1

            scenicCount <- scenicCount * subCount

        scenicCount


    let part2 (lines : StringSplitEnumerator) : int =
        let board, height = parse lines
#if DEBUG
        let board =
            {
                Arr2D.Elements = board
                Width = board.Length / height
            }
#else
        use p = fixed board

        let board =
            {
                Arr2D.Elements = p
                Length = board.Length
                Width = board.Length / height
            }
#endif

        let mutable scenicMax = 0

        for y = 0 to board.Height - 1 do
            for x = 0 to board.Width - 1 do
                scenicMax <- max scenicMax (scenicScore board x y)

        scenicMax
