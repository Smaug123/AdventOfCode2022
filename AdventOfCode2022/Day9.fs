namespace AdventOfCode2022

open System.Collections.Generic
open System

type Direction =
    | Up
    | Down
    | Left
    | Right

[<RequireQualifiedAccess>]
module Day9 =

    let parse (lines : StringSplitEnumerator) : (Direction * int) IReadOnlyList =
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

                let distance = Int32.Parse (line.Slice 2)

                output.Add (dir, distance)

        output :> _

    type Position = int * int

    let bringTailTogether (head : Position) (tail : Position) : Position =
        if abs (fst head - fst tail) <= 1 && abs (snd head - snd tail) <= 1 then
            tail
        elif fst head = fst tail then
            fst head, snd head + (if snd head < snd tail then 1 else -1)
        elif snd head = snd tail then
            fst head + (if fst head < fst tail then 1 else -1), snd head
        else
            [ (1, 1) ; (1, -1) ; (-1, 1) ; (-1, -1) ]
            |> List.map (fun (x, y) -> fst tail + x, snd tail + y)
            |> List.find (fun (x, y) -> abs (fst head - x) <= 1 && abs (snd head - y) <= 1)

    let newHead (pos : int * int) (direction : Direction) : int * int =
        match direction with
        | Direction.Up -> fst pos, snd pos + 1
        | Direction.Down -> fst pos, snd pos - 1
        | Direction.Left -> fst pos - 1, snd pos
        | Direction.Right -> fst pos + 1, snd pos

    let go (count : int) (directions : (Direction * int) seq) : int =
        let knots = Array.create count (0, 0)
        let tailVisits = HashSet ()

        for direction, distance in directions do
            for _ in 1..distance do
                let newHead = newHead knots.[0] direction
                knots.[0] <- newHead

                for knot in 1 .. knots.Length - 1 do
                    knots.[knot] <- bringTailTogether knots.[knot - 1] knots.[knot]

                tailVisits.Add knots.[count - 1] |> ignore

        tailVisits.Count

    let part1 (lines : StringSplitEnumerator) : int =
        let directions = parse lines

        go 2 directions

    let part2 (lines : StringSplitEnumerator) : int =
        let directions = parse lines

        go 10 directions
