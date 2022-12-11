namespace AdventOfCode2022

open System.Collections.Generic
open System

[<Measure>]
type monkey

[<RequireQualifiedAccess>]
module Day11 =

    type Arg<'T> =
        | Literal of 'T
        | Old

    type Monkey<'T> =
        {
            Number : int<monkey>
            StartingItems : 'T ResizeArray
            Operation : string * ('T -> 'T -> 'T) * Arg<'T>
            TestDivisibleBy : 'T
            TrueCase : int<monkey>
            FalseCase : int<monkey>
        }

    let parse (lines : StringSplitEnumerator) : Monkey<int64> IReadOnlyList =
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
                    items.Add (Int64.Parse s)

                items

            if not (enum.MoveNext ()) then
                failwith "Ran out of rows"

            let line = enum.Current

            let opDescriptor, operation, arg =
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

                StringSplitEnumerator.chomp "old" &enum

                if not (enum.MoveNext ()) then
                    failwith "expected three elements on RHS"

                let opDescriptor, op =
                    if enum.Current.Length > 1 then
                        failwith "expected operation of exactly 1 char"

                    match enum.Current.[0] with
                    | '*' -> sprintf "is multiplied by %s", (*)
                    | '+' -> sprintf "increases by %s", (+)
                    | c -> failwithf "Unrecognised op: %c" c

                if not (enum.MoveNext ()) then
                    failwith "expected three elements on RHS"

                let arg, opDescriptor =
                    if EfficientString.equals "old" enum.Current then
                        Arg.Old, opDescriptor "itself"
                    else
                        let literal = Int64.Parse enum.Current
                        Arg.Literal literal, opDescriptor (literal.ToString ())

                if enum.MoveNext () then
                    failwith "too many entries on row"

                opDescriptor, op, arg

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
                Operation = opDescriptor, operation, arg
                TestDivisibleBy = test
                TrueCase = ifTrue
                FalseCase = ifFalse
            }
            |> output.Add

        output :> IReadOnlyList<_>

    let oneRoundDivThree (monkeys : IReadOnlyList<Monkey<int64>>) (inspections : int64 array) =
        for i in 0 .. monkeys.Count - 1 do
            let monkey = monkeys.[i]
            inspections.[i] <- inspections.[i] + int64 monkey.StartingItems.Count

            for worry in monkey.StartingItems do
                let _descriptor, newWorry =
                    match monkey.Operation with
                    | opDescriptor, op, arg ->
                        let arg =
                            match arg with
                            | Arg.Old -> worry
                            | Arg.Literal l -> l

                        opDescriptor, op worry arg

                let newWorry = newWorry / 3L

                let target =
                    if newWorry % monkey.TestDivisibleBy = 0 then
                        monkey.TrueCase
                    else
                        monkey.FalseCase

                monkeys.[target / 1<monkey>].StartingItems.Add newWorry

            monkey.StartingItems.Clear ()

    let part1 (lines : StringSplitEnumerator) : int64 =
        let monkeys = parse lines

        let mutable inspections = Array.zeroCreate<int64> monkeys.Count

        for _round in 1..20 do
            oneRoundDivThree monkeys inspections

        inspections |> Array.sortInPlace
        inspections.[inspections.Length - 1] * inspections.[inspections.Length - 2]

    let oneRound (modulus : int64) (monkeys : IReadOnlyList<Monkey<int64>>) (inspections : int64 array) =
        for i in 0 .. monkeys.Count - 1 do
            let monkey = monkeys.[i]
            inspections.[i] <- inspections.[i] + int64 monkey.StartingItems.Count

            for worry in monkey.StartingItems do
                let _descriptor, newWorry =
                    match monkey.Operation with
                    | opDescriptor, op, arg ->
                        let arg =
                            match arg with
                            | Arg.Old -> worry
                            | Arg.Literal l -> l

                        opDescriptor, op worry arg

                let newWorry = newWorry % modulus

                let target =
                    if newWorry % monkey.TestDivisibleBy = 0 then
                        monkey.TrueCase
                    else
                        monkey.FalseCase

                monkeys.[target / 1<monkey>].StartingItems.Add newWorry

            monkey.StartingItems.Clear ()


    let part2 (lines : StringSplitEnumerator) : int64 =
        let monkeys = parse lines

        let mutable inspections = Array.zeroCreate<int64> monkeys.Count

        let modulus =
            (1L, monkeys) ||> Seq.fold (fun i monkey -> i * monkey.TestDivisibleBy)

        for _round in 1..10000 do
            oneRound modulus monkeys inspections

        inspections |> Array.sortInPlace
        inspections.[inspections.Length - 1] * inspections.[inspections.Length - 2]
