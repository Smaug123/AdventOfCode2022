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
    let private parseDrawing (enumerator : byref<StringSplitEnumerator>) : char list array =
        let piles = ResizeArray ()

        while enumerator.MoveNext () && not (enumerator.Current.IsWhiteSpace ()) do
            let s = enumerator.Current

            if s.IndexOf ']' >= 0 then
                for i in 1..4 .. s.Length - 1 do
                    let pile = (i - 1) / 4

                    if not (Char.IsWhiteSpace s.[i]) then
                        // TODO(perf): don't do this on every iteration
                        while piles.Count <= pile do
                            piles.Add (ResizeArray ())

                        piles.[pile].Add s.[i]

        Array.init piles.Count (fun i -> List.init piles.[i].Count (fun j -> piles.[i].[j]))

    let rec private parseInstruction (enumerator : byref<StringSplitEnumerator>) : Day5Instruction IReadOnlyList =
        let outputs = ResizeArray ()

        while (enumerator.MoveNext ()) && (not (enumerator.Current.IsWhiteSpace ())) do
            let s = enumerator.Current
            use mutable spaces = StringSplitEnumerator.make' ' ' s

            StringSplitEnumerator.chomp "move" &spaces

            let i = StringSplitEnumerator.consumeInt &spaces

            StringSplitEnumerator.chomp "from" &spaces

            let from = StringSplitEnumerator.consumeInt &spaces

            StringSplitEnumerator.chomp "to" &spaces

            let toDest = StringSplitEnumerator.consumeInt &spaces

            if spaces.MoveNext () then
                failwith "got too many entries"

            {
                From = from
                To = toDest
                Count = i
            }
            |> outputs.Add

        outputs

    let parse (s : StringSplitEnumerator) : char list array * Day5Instruction IReadOnlyList  =
        use mutable enumerator = s

        let piles = parseDrawing &enumerator
        let instructions = parseInstruction &enumerator
        piles, instructions

    let part1 (lines : StringSplitEnumerator) : string =
        let piles, instructions = parse lines

        let go (instructions : _ IReadOnlyList) (piles : _ list array) =
            for instr in instructions do
                piles.[instr.To - 1] <- List.rev piles.[instr.From - 1].[0 .. instr.Count - 1] @ piles.[instr.To - 1]
                piles.[instr.From - 1] <- piles.[instr.From - 1].[instr.Count ..]

            piles

        String (go instructions piles |> Array.map List.head)

    let part2 (lines : StringSplitEnumerator) : string =
        let piles, instructions = parse lines

        let rec go (instructions : _ IReadOnlyList) (piles : _ list array) =
            for instr in instructions do
                piles.[instr.To - 1] <- piles.[instr.From - 1].[0 .. instr.Count - 1] @ piles.[instr.To - 1]
                piles.[instr.From - 1] <- piles.[instr.From - 1].[instr.Count ..]

            piles

        String (go instructions piles |> Array.map List.head)
