namespace AdventOfCode2022

open System.Collections.Generic
open System
open Microsoft.FSharp.NativeInterop

#if DEBUG
open Checked
#endif

#nowarn "9"

[<Measure>]
type monkey

[<RequireQualifiedAccess>]
module Day11 =

    type Monkey =
        {
            Number : int<monkey>
            StartingItems : int64 ResizeArray
            OperationIsPlus : bool
            /// Negative is None
            Argument : int64
            TestDivisibleBy : int64
            TrueCase : int<monkey>
            FalseCase : int<monkey>
        }

    let parse (lines : StringSplitEnumerator) : Monkey array =
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

            let operationIsPlus, arg =
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

                let operationIsPlus =
                    if enum.Current.Length > 1 then
                        failwith "expected operation of exactly 1 char"

                    match enum.Current.[0] with
                    | '*' -> false
                    | '+' -> true
                    | c -> failwithf "Unrecognised op: %c" c

                if not (enum.MoveNext ()) then
                    failwith "expected three elements on RHS"

                let arg =
                    if EfficientString.equals "old" enum.Current then
                        -1L
                    else
                        let literal = Int64.Parse enum.Current
                        literal

                if enum.MoveNext () then
                    failwith "too many entries on row"

                operationIsPlus, arg

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
                OperationIsPlus = operationIsPlus
                Argument = arg
                TestDivisibleBy = test
                TrueCase = ifTrue
                FalseCase = ifFalse
            }
            |> output.Add

        output.ToArray ()

    let oneRoundDivThree (monkeys : IReadOnlyList<Monkey>) (inspections : int64 array) =
        for i in 0 .. monkeys.Count - 1 do
            let monkey = monkeys.[i]
            inspections.[i] <- inspections.[i] + int64 monkey.StartingItems.Count

            for worry in monkey.StartingItems do
                let newWorry =
                    let arg =
                        match monkey.Argument with
                        | -1L -> worry
                        | l -> l

                    if monkey.OperationIsPlus then worry + arg else worry * arg

                let newWorry = newWorry / 3L

                let target =
                    if newWorry % monkey.TestDivisibleBy = 0 then
                        monkey.TrueCase
                    else
                        monkey.FalseCase

                monkeys.[target / 1<monkey>].StartingItems.Add newWorry

            monkey.StartingItems.Clear ()

    let inline maxTwo (arr : int64 array) : struct (int64 * int64) =
        let mutable best = max arr.[1] arr.[0]
        let mutable secondBest = min arr.[1] arr.[0]

        for i in 2 .. arr.Length - 1 do
            if arr.[i] > best then
                secondBest <- best
                best <- arr.[i]
            elif arr.[i] > secondBest then
                secondBest <- arr.[i]

        struct (secondBest, best)

    let part1 (lines : StringSplitEnumerator) : int64 =
        let monkeys = parse lines

        let mutable inspections = Array.zeroCreate<int64> monkeys.Length

        for _round in 1..20 do
            oneRoundDivThree monkeys inspections

        let struct (a, b) = maxTwo inspections
        a * b

    let oneRound (modulus : int64) (monkeys : Monkey array) (inspections : nativeptr<int64>) =
        for i in 0 .. monkeys.Length - 1 do
            let monkey = monkeys.[i]
            let entry = NativePtr.add inspections i
            NativePtr.write entry (NativePtr.read entry + int64 monkey.StartingItems.Count)

            for worryIndex in 0 .. monkey.StartingItems.Count - 1 do
                let worry = monkey.StartingItems.[worryIndex]

                let newWorry =
                    let arg =
                        match monkey.Argument with
                        | -1L -> worry
                        | l -> l

                    if monkey.OperationIsPlus then worry + arg else worry * arg

                let newWorry = newWorry % modulus

                let target =
                    if newWorry % monkey.TestDivisibleBy = 0 then
                        monkey.TrueCase
                    else
                        monkey.FalseCase

                monkeys.[target / 1<monkey>].StartingItems.Add newWorry

            monkey.StartingItems.Clear ()

    let inline unsafeMaxTwo (len : int) (arr : nativeptr<int64>) : struct (int64 * int64) =
        let arr0 = NativePtr.read arr
        let arr1 = NativePtr.read (NativePtr.add arr 1)
        let mutable best = max arr0 arr1
        let mutable secondBest = min arr0 arr1

        for i in 2 .. len - 1 do
            let arrI = NativePtr.read (NativePtr.add arr i)

            if arrI > best then
                secondBest <- best
                best <- arrI
            elif arrI > secondBest then
                secondBest <- arrI

        struct (secondBest, best)

    let part2 (lines : StringSplitEnumerator) : int64 =
        let monkeys = parse lines

        use inspections = fixed Array.zeroCreate<int64> monkeys.Length

        let modulus =
            (1L, monkeys) ||> Seq.fold (fun i monkey -> i * monkey.TestDivisibleBy)

        for _round in 1..10000 do
            oneRound modulus monkeys inspections

        let struct (a, b) = unsafeMaxTwo monkeys.Length inspections
        a * b
