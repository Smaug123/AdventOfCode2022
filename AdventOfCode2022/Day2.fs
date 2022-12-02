namespace AdventOfCode2022

open System

type RPSMove =
    private
    | Rock
    | Paper
    | Scissors

    static member Parse (c : char) : RPSMove =
        match Char.ToLower c with
        | 'a'
        | 'x' -> Rock
        | 'b'
        | 'y' -> Paper
        | 'c'
        | 'z' -> Scissors
        | _ -> failwithf $"Could not parse char: %c{c}"

[<RequireQualifiedAccess>]
module RockPaperScissors =
    let outcome (self : RPSMove) (opponent : RPSMove) : int =
        match self, opponent with
        | Rock, Paper -> 0
        | Rock, Scissors -> 6
        | Rock, Rock -> 3
        | Paper, Paper -> 3
        | Paper, Scissors -> 0
        | Paper, Rock -> 6
        | Scissors, Paper -> 6
        | Scissors, Scissors -> 3
        | Scissors, Rock -> 0

    let shapeScore (shape : RPSMove) : int =
        match shape with
        | Rock -> 1
        | Paper -> 2
        | Scissors -> 3

    let wouldBeat (shape : RPSMove) : RPSMove =
        match shape with
        | Rock -> Paper
        | Paper -> Scissors
        | Scissors -> Rock

    let wouldBeBeaten (shape : RPSMove) : RPSMove =
        match shape with
        | Rock -> Scissors
        | Paper -> Rock
        | Scissors -> Paper

[<RequireQualifiedAccess>]
module Day2 =

    let part1 (lines : string seq) : int =
        lines
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.map (fun s ->
            let s = s.Trim ()

            if s.Length <> 3 || s.[1] <> ' ' then
                failwithf "Bad format of string: %s" s

            RPSMove.Parse s.[0], RPSMove.Parse s.[2]
        )
        |> Seq.map (fun (opponent, self) -> RockPaperScissors.outcome self opponent + RockPaperScissors.shapeScore self)
        |> Seq.sum


    let part2 (lines : string seq) : int =
        lines
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.map (fun s ->
            let s = s.Trim ()

            if s.Length <> 3 || s.[1] <> ' ' then
                failwithf "Bad format of string: %s" s

            let move = RPSMove.Parse s.[0]
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
                | c ->
                    failwithf "Unexpected strategy: %c" c
            move, myMove
        )
        |> Seq.map (fun (opponent, self) -> RockPaperScissors.outcome self opponent + RockPaperScissors.shapeScore self)
        |> Seq.sum
