namespace AdventOfCode2022

open System.Collections.Generic
open System

[<Measure>]
type monkey

[<RequireQualifiedAccess>]
module Day11 =

    type Arg =
        | Literal of int
        | Old

    type Monkey =
        {
            Number : int<monkey>
            StartingItems : int ResizeArray
            Operation : (int -> int -> int) * Arg * Arg
            TestDivisibleBy : int
            TrueCase : int<monkey>
            FalseCase : int<monkey>
        }

    let parse (lines : StringSplitEnumerator) : Monkey IReadOnlyList =
        use mutable enum = lines
        let output = ResizeArray ()

        while enum.MoveNext () do
            let monkey =
                let line = enum.Current.TrimEnd ()
                let mutable enum = StringSplitEnumerator.make' ' ' (line.TrimEnd ':')
                StringSplitEnumerator.chomp "Monkey" &enum
                let monkey = StringSplitEnumerator.consumeInt &enum * 1<monkey>

                if enum.MoveNext () then
                    failwith "Bad Monkey row"

                monkey

            if not (enum.MoveNext ()) then
                failwith "Ran out of rows"

            let line = enum.Current

            let startItems =
                let line = line.Trim ()
                let mutable enum = StringSplitEnumerator.make' ' ' line
                StringSplitEnumerator.chomp "Starting" &enum
                StringSplitEnumerator.chomp "items:" &enum
                let items = ResizeArray ()

                while enum.MoveNext () do
                    let s = enum.Current.TrimEnd ','
                    items.Add (Int32.Parse s)

                items

            if not (enum.MoveNext ()) then
                failwith "Ran out of rows"

            let line = enum.Current

            let operation, arg1, arg2 =
                let line = line.Trim ()
                let mutable enum = StringSplitEnumerator.make' ':' line
                StringSplitEnumerator.chomp "Operation" &enum

                if not (enum.MoveNext ()) then
                    failwith "expected an operation"

                let line = enum.Current.Trim ()

                if enum.MoveNext () then
                    failwith "bad formatting on operation"

                let mutable enum = StringSplitEnumerator.make' '=' line
                StringSplitEnumerator.chomp "new " &enum

                if not (enum.MoveNext ()) then
                    failwith "expected an RHS"

                let rhs = enum.Current.Trim ()

                if enum.MoveNext () then
                    failwith "too many equals signs"

                let mutable enum = StringSplitEnumerator.make' ' ' rhs

                if not (enum.MoveNext ()) then
                    failwith "expected an RHS"

                let arg1 =
                    if EfficientString.equals "old" enum.Current then
                        Arg.Old
                    else
                        failwith "never encountered"

                if not (enum.MoveNext ()) then
                    failwith "expected three elements on RHS"

                let op =
                    if enum.Current.Length > 1 then
                        failwith "expected operation of exactly 1 char"

                    match enum.Current.[0] with
                    | '*' -> (*)
                    | '+' -> (+)
                    | c -> failwithf "Unrecognised op: %c" c

                if not (enum.MoveNext ()) then
                    failwith "expected three elements on RHS"

                let arg2 =
                    if EfficientString.equals "old" enum.Current then
                        Arg.Old
                    else
                        Arg.Literal (Int32.Parse enum.Current)

                if enum.MoveNext () then
                    failwith "too many entries on row"

                op, arg1, arg2

            if not (enum.MoveNext ()) then
                failwith "Ran out of rows"

            let line = enum.Current.Trim ()

            let test =
                if not (line.StartsWith "Test: divisible by ") then
                    failwith "bad formatting on test line"

                Int32.Parse (line.Slice 19)

            if not (enum.MoveNext ()) then
                failwith "Ran out of rows"

            let line = enum.Current.Trim ()

            let ifTrue =
                if not (line.StartsWith "If true: throw to monkey ") then
                    failwith "bad formatting for ifTrue line"

                Int32.Parse (line.Slice 24) * 1<monkey>

            if not (enum.MoveNext ()) then
                failwith "Ran out of rows"

            let line = enum.Current.Trim ()

            let ifFalse =
                if not (line.StartsWith "If false: throw to monkey ") then
                    failwith "bad formatting for ifFalse line"

                Int32.Parse (line.Slice 25) * 1<monkey>

            // We may be at the end, in which case there's no empty row.
            enum.MoveNext () |> ignore

            if ifTrue = monkey then
                failwith "assumption broken: throws to self"

            if ifFalse = monkey then
                failwith "assumption broken: throws to self"

            {
                Number = monkey
                StartingItems = startItems
                Operation = operation, arg1, arg2
                TestDivisibleBy = test
                TrueCase = ifTrue
                FalseCase = ifFalse
            }
            |> output.Add

        output :> IReadOnlyList<_>

    let oneRound (logDebug : string -> unit) (monkeys : IReadOnlyList<Monkey>) (inspections : int array) =
        for i in 0 .. monkeys.Count - 1 do
            logDebug $"Monkey %i{i}:"
            let monkey = monkeys.[i]
            inspections.[i] <- inspections.[i] + monkey.StartingItems.Count

            for worry in monkey.StartingItems do
                logDebug $"  Monkey inspects an item with a worry level of %i{worry}."

                let newWorry =
                    match monkey.Operation with
                    | op, arg1, arg2 ->
                        let arg1 =
                            match arg1 with
                            | Arg.Old -> worry
                            | Arg.Literal l -> l

                        let arg2 =
                            match arg2 with
                            | Arg.Old -> worry
                            | Arg.Literal l -> l

                        op arg1 arg2

                logDebug $"    Worry level is changed to %i{newWorry}."
                let newWorry = newWorry / 3
                logDebug $"    Monkey gets bored with item. Worry level is divided by 3 to %i{newWorry}."

                let target =
                    if newWorry % monkey.TestDivisibleBy = 0 then
                        logDebug $"    Current worry level is divisible by %i{monkey.TestDivisibleBy}."
                        monkey.TrueCase
                    else
                        logDebug $"    Current worry level is not divisible by %i{monkey.TestDivisibleBy}."
                        monkey.FalseCase

                logDebug $"    Item with worry level %i{newWorry} is thrown to monkey %i{target}."

                monkeys.[target / 1<monkey>].StartingItems.Add newWorry

            monkey.StartingItems.Clear ()

    let part1 (emitCounts : int list seq -> unit) (lines : StringSplitEnumerator) : int =
        let monkeys = parse lines

        let mutable inspections = Array.zeroCreate<int> monkeys.Count

        for _round in 1..20 do
            oneRound ignore monkeys inspections

            seq {
                for monkey in monkeys do
                    yield monkey.StartingItems |> List.ofSeq
            }
            |> emitCounts

        inspections |> Array.sortInPlace
        inspections.[inspections.Length - 1] * inspections.[inspections.Length - 2]

    let part2 (lines : StringSplitEnumerator) : int =
        let directions = parse lines

        0
