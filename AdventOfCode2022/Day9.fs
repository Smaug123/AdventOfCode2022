namespace AdventOfCode2022

open System.Collections.Generic
open System

type Direction =
    | Up = 0
    | Down = 1
    | Left = 2
    | Right = 3

[<RequireQualifiedAccess>]
module Day9 =

    let parse (lines : StringSplitEnumerator) : (Direction * byte) IReadOnlyList =
        use mutable enum = lines
        let output = ResizeArray ()

        for line in enum do
            let line = line.TrimEnd ()

            if not (line.IsWhiteSpace ()) then
                let dir =
                    match Char.ToUpperInvariant line.[0] with
                    | 'U' -> Direction.Up
                    | 'D' -> Direction.Down
                    | 'L' -> Direction.Left
                    | 'R' -> Direction.Right
                    | _ -> failwith "Unexpected direction"

                let distance = Byte.Parse (line.Slice 2)

                output.Add (dir, distance)

        output :> _

    type Position = (struct (int * int))
    let inline fst (struct (x, y)) = x
    let inline snd (struct (x, y)) = y

    let bringTailTogether (head : Position) (tail : Position) : Position =
        if abs (fst head - fst tail) <= 1 && abs (snd head - snd tail) <= 1 then
            tail
        elif fst head = fst tail then
            fst head, snd head + (if snd head < snd tail then 1 else -1)
        elif snd head = snd tail then
            fst head + (if fst head < fst tail then 1 else -1), snd head
        else
            let fstCoord =
                if abs (fst head - fst tail + 1) <= 1 then
                    fst tail - 1
                else
                    fst tail + 1

            let sndCoord =
                if abs (snd head - snd tail + 1) <= 1 then
                    snd tail - 1
                else
                    snd tail + 1

            (fstCoord, sndCoord)

    let newHead (x : int) (y : int) (direction : Direction) : Position =
        match direction with
        | Direction.Up -> x, y + 1
        | Direction.Down -> x, y - 1
        | Direction.Left -> x - 1, y
        | Direction.Right -> x + 1, y
        | _ -> failwith "bad enum"

    let go (count : int) (directions : (Direction * byte) seq) : int =
        let knots = Array.create count (struct (0, 0))
        let tailVisits = HashSet ()
        tailVisits.Add (struct (0, 0)) |> ignore

        for direction, distance in directions do
            for _ in 1uy .. distance do
                let newHead = newHead (fst knots.[0]) (snd knots.[0]) direction
                knots.[0] <- newHead

                for knot in 1 .. knots.Length - 2 do
                    knots.[knot] <- bringTailTogether knots.[knot - 1] knots.[knot]

                let newTail = bringTailTogether knots.[knots.Length - 2] knots.[knots.Length - 1]

                if
                    fst newTail <> fst knots.[knots.Length - 1]
                    || snd newTail <> snd knots.[knots.Length - 1]
                then
                    knots.[knots.Length - 1] <- newTail
                    tailVisits.Add newTail |> ignore

        tailVisits.Count

    let part1 (lines : StringSplitEnumerator) : int =
        let directions = parse lines

        go 2 directions

    let part2 (lines : StringSplitEnumerator) : int =
        let directions = parse lines

        go 10 directions
