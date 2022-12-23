namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day23 =

    let parse (line : StringSplitEnumerator) : Coordinate HashSet =
        use mutable enum = line.GetEnumerator ()
        let output = HashSet ()
        let mutable y = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable x = 0

                for c in enum.Current.TrimEnd () do
                    if c = '#' then
                        output.Add
                            {
                                X = x
                                Y = y
                            }
                        |> ignore

                    x <- x + 1

                y <- y + 1

        output

    // proposedEndSteps will be cleared on input; it's just to save allocation.
    // All inputs will be mutated.
    // Returns true if an elf moved.
    let inline oneRound
        (board : HashSet<Coordinate>)
        (proposedEndSteps : Dictionary<_, _>)
        (proposedDirections : _ array)
        : bool
        =
        proposedEndSteps.Clear ()

        for elf in board do
            let mutable hasAdjacentElf = false

            for xOffset = -1 to 1 do
                for yOffset = -1 to 1 do
                    if not hasAdjacentElf && (xOffset <> 0 || yOffset <> 0) then
                        let adjacentElf =
                            {
                                X = elf.X + xOffset
                                Y = elf.Y + yOffset
                            }

                        if board.Contains adjacentElf then
                            hasAdjacentElf <- true

            if hasAdjacentElf then
                let mutable proposedEndPlace = ValueNone

                for struct (proposedX, proposedY) in proposedDirections do
                    let mutable hasElfInDestination = false

                    if proposedEndPlace.IsNone then
                        if proposedX = 0 then
                            for proposedX' = -1 to 1 do
                                if not hasElfInDestination then
                                    let proposal =
                                        {
                                            X = elf.X + proposedX'
                                            Y = elf.Y + proposedY
                                        }

                                    if board.Contains proposal then
                                        hasElfInDestination <- true

                        else
                            for proposedY' = -1 to 1 do
                                if not hasElfInDestination then
                                    let proposal =
                                        {
                                            X = elf.X + proposedX
                                            Y = elf.Y + proposedY'
                                        }

                                    if board.Contains proposal then
                                        hasElfInDestination <- true

                        if not hasElfInDestination then
                            proposedEndPlace <-
                                ValueSome
                                    {
                                        X = elf.X + proposedX
                                        Y = elf.Y + proposedY
                                    }

                match proposedEndPlace with
                | ValueNone -> ()
                | ValueSome loc ->
                    if not (proposedEndSteps.TryAdd (loc, ValueSome elf)) then
                        proposedEndSteps.[loc] <- ValueNone

        for KeyValue (dest, source) in proposedEndSteps do
            match source with
            | ValueNone -> ()
            | ValueSome source ->
                board.Remove source |> ignore
                board.Add dest |> ignore

        let tmp = proposedDirections.[0]

        for i = 0 to proposedDirections.Length - 2 do
            proposedDirections.[i] <- proposedDirections.[i + 1]

        proposedDirections.[proposedDirections.Length - 1] <- tmp
        proposedEndSteps.Count > 0

    let part1 (lines : StringSplitEnumerator) : int =
        let board = parse lines

        let proposedEndSteps = Dictionary board.Count

        let proposedDirections =
            [| struct (0, -1) ; struct (0, 1) ; struct (-1, 0) ; struct (1, 0) |]

        for _round = 0 to 9 do
            oneRound board proposedEndSteps proposedDirections |> ignore

        let mutable minX = Int32.MaxValue
        let mutable maxX = Int32.MinValue
        let mutable minY = Int32.MaxValue
        let mutable maxY = Int32.MinValue
        let mutable count = 0

        for elf in board do
            if elf.X < minX then
                minX <- elf.X

            if elf.X > maxX then
                maxX <- elf.X

            if elf.Y < minY then
                minY <- elf.Y

            if elf.Y > maxY then
                maxY <- elf.Y

            count <- count + 1

        (maxX - minX + 1) * (maxY - minY + 1) - count

    let part2 (lines : StringSplitEnumerator) : int =
        let board = parse lines

        let proposedEndSteps = Dictionary board.Count

        let proposedDirections =
            [| struct (0, -1) ; struct (0, 1) ; struct (-1, 0) ; struct (1, 0) |]

        let mutable count = 1

        while oneRound board proposedEndSteps proposedDirections do
            count <- count + 1

        count
