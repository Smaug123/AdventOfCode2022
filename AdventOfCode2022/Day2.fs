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

    let part1 (lines : string seq) : int =
        lines
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.map (fun s ->
            let s = s.Trim ()

            if s.Length <> 3 || s.[1] <> ' ' then
                failwithf $"Bad format of string: %s{s}"

            RockPaperScissors.parse s.[0], RockPaperScissors.parse s.[2]
        )
        |> Seq.map (fun (opponent, self) -> RockPaperScissors.outcome self opponent + RockPaperScissors.shapeScore self)
        |> Seq.sum


    let part2 (lines : string seq) : int =
        lines
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.map (fun s ->
            let s = s.Trim ()

            if s.Length <> 3 || s.[1] <> ' ' then
                failwithf $"Bad format of string: %s{s}"

            let move = RockPaperScissors.parse s.[0]

            let myMove =
                match Char.ToLower s.[2] with
                | 'x' ->
                    // Need to lose
                    RockPaperScissors.wouldBeBeaten move
                | 'y' ->
                    // Need to draw
                    move
                | 'z' ->
                    // Need to win
                    RockPaperScissors.wouldBeat move
                | c -> failwithf $"Unexpected strategy: %c{c}"

            move, myMove
        )
        |> Seq.map (fun (opponent, self) -> RockPaperScissors.outcome self opponent + RockPaperScissors.shapeScore self)
        |> Seq.sum
