namespace AdventOfCode2022

open System

type RPSMove =
    | Rock = 1
    | Paper = 2
    | Scissors = 0

[<RequireQualifiedAccess>]
module RockPaperScissors =

    let parse (c : char) : RPSMove =
        match Char.ToLower c with
        | 'a'
        | 'x' -> RPSMove.Rock
        | 'b'
        | 'y' -> RPSMove.Paper
        | 'c'
        | 'z' -> RPSMove.Scissors
        | _ -> failwithf $"Could not parse char: %c{c}"

    let outcome (self : RPSMove) (opponent : RPSMove) : int =
        if self = opponent then
            3
        else

        let self = int self
        let opponent = int opponent

        if self = (opponent + 2) % 3 then 0
        elif self = (opponent + 1) % 3 then 6
        else failwith $"oh no: {self}, {opponent}"

    let shapeScore (shape : RPSMove) : int =
        let score = int shape
        if score = 0 then 3 else score

    let wouldBeat (shape : RPSMove) : RPSMove = (int shape + 1) % 3 |> enum

    let wouldBeBeaten (shape : RPSMove) : RPSMove = (int shape + 2) % 3 |> enum

[<RequireQualifiedAccess>]
module Day2 =

    let part1 (lines : StringSplitEnumerator) : int =
        let mutable sum = 0

        for line in lines do
            if not (line.IsWhiteSpace ()) then
                let line = line.Trim ()

                if line.Length <> 3 || line.[1] <> ' ' then
                    failwithf $"Bad format of string: %s{line.ToString ()}"

                let opponent = RockPaperScissors.parse line.[0]
                let self = RockPaperScissors.parse line.[2]

                sum <-
                    sum
                    + RockPaperScissors.outcome self opponent
                    + RockPaperScissors.shapeScore self

        sum

    let part2 (lines : StringSplitEnumerator) : int =
        let mutable sum = 0

        for line in lines do
            if not (line.IsWhiteSpace ()) then
                let line = line.Trim ()

                if line.Length <> 3 || line.[1] <> ' ' then
                    failwithf $"Bad format of string: %s{line.ToString ()}"

                let opponent = RockPaperScissors.parse line.[0]

                let self =
                    match Char.ToLower line.[2] with
                    | 'x' ->
                        // Need to lose
                        RockPaperScissors.wouldBeBeaten opponent
                    | 'y' ->
                        // Need to draw
                        opponent
                    | 'z' ->
                        // Need to win
                        RockPaperScissors.wouldBeat opponent
                    | c -> failwithf $"Unexpected strategy: %c{c}"

                sum <-
                    sum
                    + RockPaperScissors.outcome self opponent
                    + RockPaperScissors.shapeScore self

        sum
