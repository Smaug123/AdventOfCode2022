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

    type Day24Board = ImmutableDictionary<Coordinate, Direction list>

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
                    | '>' -> output.Add (coord, [ Direction.Right ])
                    | '^' -> output.Add (coord, [ Direction.Up ])
                    | 'v' -> output.Add (coord, [ Direction.Down ])
                    | '<' -> output.Add (coord, [ Direction.Left ])
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
            directions
            |> Seq.map (fun dir ->
                let newPos =
                    match dir with
                    | Direction.Down ->
                        { position with
                            Y = if position.Y = height - 2 then 1 else position.Y + 1
                        }
                    | Direction.Up ->
                        { position with
                            Y = if position.Y = 1 then height - 2 else position.Y - 1
                        }
                    | Direction.Left ->
                        { position with
                            X = if position.X = 1 then width - 2 else position.X - 1
                        }
                    | Direction.Right ->
                        { position with
                            X = if position.X = width - 2 then 1 else position.X + 1
                        }
                    | _ -> failwith "unexpected direction"

                newPos, dir
            )
        )
        |> Seq.groupBy fst
        |> Seq.map (fun (coord, directions) -> KeyValuePair (coord, List.ofSeq (Seq.map snd directions)))
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

    let printBoard (locations : Coordinate Set) (width : int) (height : int) (board : Day24Board) : unit =
        do
            let arr = Array.create width '#'
            arr.[1] <- 'E'
            printfn $"%s{String arr}"

        for y = 1 to height - 2 do
            printf "#"

            for x = 1 to width - 2 do
                match
                    board.TryGetValue
                        {
                            X = x
                            Y = y
                        }
                with
                | false, _ ->
                    if
                        locations.Contains
                            {
                                X = x
                                Y = y
                            }
                    then
                        'E'
                    else
                        '.'
                    |> printf "%c"
                | true, [] -> failwith "bad"
                | true, [ Direction.Down ] -> printf "v"
                | true, [ Direction.Up ] -> printf "^"
                | true, [ Direction.Left ] -> printf "<"
                | true, [ Direction.Right ] -> printf ">"
                | true, v -> printf $"%i{v.Length}"

            printfn "#"

        do
            let arr = Array.create width '#'
            arr.[width - 2] <- '.'
            printfn $"%s{String arr}"

        printfn ""

    let goToEnd (width : int) (height : int) availableMoves (timeStep : int) =
        let rec go (timeStep : int) (toExplore : Coordinate Set) =
            if
                toExplore.Contains
                    {
                        X = width - 2
                        Y = height - 2
                    }
            then
                timeStep + 1
            else

            let next =
                toExplore
                |> Seq.collect (fun currPos -> availableMoves (timeStep, currPos))
                |> Set.ofSeq

            go (timeStep + 1) next

        go
            timeStep
            (Set.singleton
                {
                    X = 1
                    Y = 0
                })

    let goToStart (width : int) (height : int) availableMoves (timeStep : int) =
        let rec go (timeStep : int) (toExplore : Coordinate Set) =
            if
                toExplore.Contains
                    {
                        X = 1
                        Y = 1
                    }
            then
                timeStep + 1
            else

            let next =
                toExplore
                |> Seq.collect (fun currPos -> availableMoves (timeStep, currPos))
                |> Set.ofSeq

            go (timeStep + 1) next

        go
            timeStep
            (Set.singleton
                {
                    X = width - 2
                    Y = height - 1
                })

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
