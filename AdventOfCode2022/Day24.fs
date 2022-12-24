namespace AdventOfCode2022

open System
open System.Collections.Generic
open System.Collections.Immutable

#if DEBUG
open System.Net.Security
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
    type Day24Board = ImmutableDictionary<Coordinate, byte>

    /// Returns the width and the height too.
    let parse (line : StringSplitEnumerator) : Day24Board * int * int =
        use mutable enum = line.GetEnumerator ()
        let output = Dictionary ()
        let mutable y = 0
        let mutable width = 0

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable x = 0

                for c in enum.Current.TrimEnd () do
                    let coord =
                        {
                            X = x
                            Y = y
                        }

                    match c with
                    | '>' -> output.Add (coord, 8uy)
                    | '^' -> output.Add (coord, 1uy)
                    | 'v' -> output.Add (coord, 2uy)
                    | '<' -> output.Add (coord, 4uy)
                    | '.'
                    | '#' -> ()
                    | _ -> failwithf "unexpected char: %c" c

                    x <- x + 1

                width <- x

                y <- y + 1

        output.ToImmutableDictionary (), width, y

    let moveBlizzards (width : int) (height : int) (board : Day24Board) : Day24Board =
        board
        |> Seq.collect (fun (KeyValue (position, directions)) ->
            seq {
                if directions % 2uy = 1uy then
                    yield
                        { position with
                            Y = if position.Y = 1 then height - 2 else position.Y - 1
                        },
                        1uy

                if (directions / 2uy) % 2uy = 1uy then
                    yield
                        { position with
                            Y = if position.Y = height - 2 then 1 else position.Y + 1
                        },
                        2uy

                if (directions / 4uy) % 2uy = 1uy then
                    yield
                        { position with
                            X = if position.X = 1 then width - 2 else position.X - 1
                        },
                        4uy

                if (directions / 8uy) % 2uy = 1uy then
                    yield
                        { position with
                            X = if position.X = width - 2 then 1 else position.X + 1
                        },
                        8uy
            }
        )
        |> Seq.groupBy fst
        |> Seq.map (fun (coord, directions) -> KeyValuePair (coord, Seq.sum (Seq.map snd directions)))
        |> ImmutableDictionary.CreateRange

    let boardAtTime (width : int) (height : int) (startBoard : Day24Board) : int -> Day24Board =
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
        (width : int)
        (height : int)
        (current : Coordinate)
        (board : Day24Board)
        : Coordinate list
        =
        [
            if current.X > 1 && current.Y <> 0 && current.Y <> height - 1 then
                let trial =
                    { current with
                        X = current.X - 1
                    }

                if not (board.ContainsKey trial) then
                    yield trial
            if current.X < width - 2 && current.Y <> 0 then
                let trial =
                    { current with
                        X = current.X + 1
                    }

                if not (board.ContainsKey trial) then
                    yield trial
            if current.Y > 1 && current.X <> 0 then
                let trial =
                    { current with
                        Y = current.Y - 1
                    }

                if not (board.ContainsKey trial) then
                    yield trial
            if current.Y < height - 2 && current.X <> 0 then
                let trial =
                    { current with
                        Y = current.Y + 1
                    }

                if not (board.ContainsKey trial) then
                    yield trial
            if not (board.ContainsKey current) then
                yield current
        ]

    let availableMoves
        (width : int)
        (height : int)
        (boardAtTime : int -> Day24Board)
        : int * Coordinate -> Coordinate list
        =
        let store = Dictionary ()

        fun ((timeStep, currPos) as args) ->
            match store.TryGetValue args with
            | false, _ ->
                let board = boardAtTime (timeStep + 1)

                let afterMoving = availableIndividualMoves width height currPos board

                store.[args] <- afterMoving
                afterMoving
            | true, v -> v

    let goToEnd (width : int) (height : int) availableMoves (timeStep : int) =
        let mutable buffer = HashSet ()

        let rec go (timeStep : int) (toExplore : Coordinate HashSet) =
            if
                toExplore.Contains
                    {
                        X = width - 2
                        Y = height - 2
                    }
            then
                timeStep + 1
            else

            buffer.Clear ()

            for currPos in toExplore do
                for move in availableMoves (timeStep, currPos) do
                    buffer.Add move |> ignore

            let continueWith = buffer
            buffer <- toExplore

            go (timeStep + 1) continueWith

        let set = HashSet ()

        {
            X = 1
            Y = 0
        }
        |> set.Add
        |> ignore

        go timeStep set

    let goToStart (width : int) (height : int) availableMoves (timeStep : int) =
        let mutable buffer = HashSet ()

        let rec go (timeStep : int) (toExplore : Coordinate HashSet) =
            if
                toExplore.Contains
                    {
                        X = 1
                        Y = 1
                    }
            then
                timeStep + 1
            else

            buffer.Clear ()

            for currPos in toExplore do
                for move in availableMoves (timeStep, currPos) do
                    buffer.Add move |> ignore

            let continueWith = buffer
            buffer <- toExplore

            go (timeStep + 1) continueWith

        let arr = HashSet ()

        {
            X = width - 2
            Y = height - 1
        }
        |> arr.Add
        |> ignore

        go timeStep arr

    let part1 (lines : StringSplitEnumerator) : int =
        let board, width, height = parse lines

        let boardAtTime = boardAtTime width height board
        let availableMoves = availableMoves width height boardAtTime

        goToEnd width height availableMoves 0

    let part2 (lines : StringSplitEnumerator) : int =
        let board, width, height = parse lines

        let boardAtTime = boardAtTime width height board
        let availableMoves = availableMoves width height boardAtTime

        let toEnd = goToEnd width height availableMoves 0
        let backToStart = goToStart width height availableMoves toEnd
        goToEnd width height availableMoves backToStart
