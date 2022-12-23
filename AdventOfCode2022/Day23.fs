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

    let parse (line : StringSplitEnumerator) : struct (int * int) HashSet =
        use mutable enum = line.GetEnumerator ()
        let output = HashSet ()
        let mutable y = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable x = 0

                for c in enum.Current.TrimEnd () do
                    if c = '#' then
                        output.Add (struct (x, y)) |> ignore

                    x <- x + 1

                y <- y + 1

        output

    // proposedEndSteps will be cleared on input; it's just to save allocation.
    // All inputs will be mutated.
    // Returns true if an elf moved.
    let inline oneRound
        (board : HashSet<_>)
        (proposedEndSteps : Dictionary<_, _>)
        (proposedDirections : _ array)
        : bool
        =
        proposedEndSteps.Clear ()

        for struct (elfX, elfY) as elf in board do
            let mutable adjacentElvesCount = 0

            for xOffset = -1 to 1 do
                for yOffset = -1 to 1 do
                    if
                        (xOffset <> 0 || yOffset <> 0)
                        && board.Contains (struct (elfX + xOffset, elfY + yOffset))
                    then
                        adjacentElvesCount <- adjacentElvesCount + 1

            if adjacentElvesCount > 0 then
                let mutable proposedEndPlace = ValueNone

                for struct (proposedX, proposedY) in proposedDirections do
                    let mutable hasElfInDestination = false

                    if proposedEndPlace.IsNone then
                        if proposedX = 0 then
                            for proposedX' = -1 to 1 do
                                if board.Contains (struct (elfX + proposedX', elfY + proposedY)) then
                                    hasElfInDestination <- true

                            if not hasElfInDestination then
                                proposedEndPlace <- ValueSome (struct (elfX + proposedX, elfY + proposedY))
                        else
                            for proposedY' = -1 to 1 do
                                if board.Contains (struct (elfX + proposedX, elfY + proposedY')) then
                                    hasElfInDestination <- true

                            if not hasElfInDestination then
                                proposedEndPlace <- ValueSome (struct (elfX + proposedX, elfY + proposedY))

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

        for struct (x, y) in board do
            if x < minX then
                minX <- x

            if x > maxX then
                maxX <- x

            if y < minY then
                minY <- y

            if y > maxY then
                maxY <- y

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
