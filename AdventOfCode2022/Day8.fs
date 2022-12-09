namespace AdventOfCode2022

open System.Collections.Generic
open System

[<RequireQualifiedAccess>]
module Day8 =

    let parse (lines : StringSplitEnumerator) : byte[] IReadOnlyList =
        use mutable enum = lines
        let output = ResizeArray ()

        for line in enum do
            let line = line.TrimEnd ()

            if not (line.IsWhiteSpace ()) then
                let arr = Array.zeroCreate line.Length
                let mutable i = 0

                for c in line do
                    arr.[i] <- byte c - byte '0'
                    i <- i + 1

                output.Add arr

        output :> _

    let isVisible (board : byte[] IReadOnlyList) (x : int) (y : int) : bool =
        // From the left?
        let mutable isVisible = true
        let mutable i = 0

        while i < x && isVisible do
            if board.[y].[i] >= board.[y].[x] then
                isVisible <- false

            i <- i + 1

        if isVisible then
            true
        else

        // From the right?
        let mutable isVisible = true
        let mutable i = board.[0].Length - 1

        while i > x && isVisible do
            if board.[y].[i] >= board.[y].[x] then
                isVisible <- false

            i <- i - 1

        if isVisible then true else

        // From the top?
        let mutable isVisible = true
        let mutable i = 0

        while i < y && isVisible do
            if board.[i].[x] >= board.[y].[x] then
                isVisible <- false

            i <- i + 1

        if isVisible then
            true
        else

        // From the bottom?
        let mutable isVisible = true
        let mutable i = board.Count - 1

        while i > y && isVisible do
            if board.[i].[x] >= board.[y].[x] then
                isVisible <- false

            i <- i - 1

        isVisible

    let part1 (lines : StringSplitEnumerator) : int =
        let board = parse lines

        let mutable visibleCount = 0

        for y in 0 .. board.Count - 1 do
            for x in 0 .. board.[0].Length - 1 do
                if isVisible board x y then
                    visibleCount <- visibleCount + 1

        visibleCount

    let scenicScore (board : byte[] IReadOnlyList) (x : int) (y : int) : int =
        let mutable scenicCount = 0

        do
            let mutable isVisible = true
            let mutable i = y - 1

            while i >= 0 && isVisible do
                if board.[i].[x] >= board.[y].[x] then
                    isVisible <- false

                scenicCount <- scenicCount + 1

                i <- i - 1

        do
            let mutable isVisible = true
            let mutable i = y + 1
            let mutable subCount = 0

            while i < board.Count && isVisible do
                if board.[i].[x] >= board.[y].[x] then
                    isVisible <- false

                subCount <- subCount + 1

                i <- i + 1

            scenicCount <- scenicCount * subCount

        do
            let mutable isVisible = true
            let mutable i = x - 1
            let mutable subCount = 0

            while i >= 0 && isVisible do
                if board.[y].[i] >= board.[y].[x] then
                    isVisible <- false

                subCount <- subCount + 1

                i <- i - 1

            scenicCount <- scenicCount * subCount

        do
            let mutable isVisible = true
            let mutable i = x + 1
            let mutable subCount = 0

            while i < board.[0].Length && isVisible do
                if board.[y].[i] >= board.[y].[x] then
                    isVisible <- false

                subCount <- subCount + 1

                i <- i + 1

            scenicCount <- scenicCount * subCount

        scenicCount


    let part2 (lines : StringSplitEnumerator) : int =
        let board = parse lines
        let mutable scenicMax = 0

        for y in 0 .. board.Count - 1 do
            for x in 0 .. board.[0].Length - 1 do
                scenicMax <- max scenicMax (scenicScore board x y)

        scenicMax
