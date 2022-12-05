namespace AdventOfCode2022

open System
open System.Collections.Generic

type Day5Instruction =
    {
        Count : int
        From : int
        To : int
    }

[<RequireQualifiedAccess>]
module Day5 =

    let parse (s : string seq) : char list array * Day5Instruction list =
        use enumerator = s.GetEnumerator ()

        let rec parseDrawing (piles : char list ResizeArray) : char list array =
            enumerator.MoveNext () |> ignore
            let s = enumerator.Current

            if String.IsNullOrWhiteSpace s then
                for i in 0 .. piles.Count - 1 do
                    piles.[i] <- List.rev piles.[i]

                piles |> Array.ofSeq
            else if not (s.Contains ']') then
                // Bottom row
                parseDrawing piles
            else
                for i in 1..4 .. s.Length - 1 do
                    let pile = (i - 1) / 4

                    if not (Char.IsWhiteSpace s.[i]) then
                        // TODO(perf): don't do this on every iteration
                        while piles.Count <= pile do
                            piles.Add []

                        piles.[pile] <- s.[i] :: piles.[pile]

                parseDrawing piles

        let rec parseInstruction (instructions : Day5Instruction list) =
            if not (enumerator.MoveNext ()) || (String.IsNullOrWhiteSpace enumerator.Current) then
                instructions |> List.rev
            else
                let s = enumerator.Current

                match s.Split ' ' with
                | [| "move" ; i ; "from" ; from ; "to" ; toDest |] ->
                    let newInstruction =
                        {
                            From = from |> int
                            To = toDest.TrimEnd () |> int
                            Count = i |> int
                        }

                    parseInstruction (newInstruction :: instructions)
                | _ -> failwithf "bad string"

        let piles = parseDrawing (ResizeArray ())
        piles, parseInstruction []

    let part1 (lines : string seq) : string =
        let piles, instructions = parse lines

        let rec go (instructions : _ list) (piles : _ list array) =
            match instructions with
            | [] -> piles
            | instr :: rest ->
                match piles.[instr.From - 1] with
                | [] -> failwith "couldn't pop"
                | from :: fromRest ->
                    piles.[instr.To - 1] <- from :: piles.[instr.To - 1]
                    piles.[instr.From - 1] <- fromRest

                if instr.Count = 1 then
                    go rest piles
                else
                    go
                        ({ instr with
                            Count = instr.Count - 1
                         }
                         :: rest)
                        piles

        String (go instructions piles |> Array.map List.head)

    let part2 (lines : string seq) : string =
        let piles, instructions = parse lines

        let rec go (instructions : _ list) (piles : _ list array) =
            match instructions with
            | [] -> piles
            | instr :: rest ->
                piles.[instr.To - 1] <- piles.[instr.From - 1].[0 .. instr.Count - 1] @ piles.[instr.To - 1]
                piles.[instr.From - 1] <- piles.[instr.From - 1].[instr.Count ..]
                go rest piles

        String (go instructions piles |> Array.map List.head)
