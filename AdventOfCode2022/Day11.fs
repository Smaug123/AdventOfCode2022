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
            OperationIsPlus : bool
            /// Negative is None
            Argument : int64
            TestDivisibleBy : int64
            TrueCase : int<monkey>
            FalseCase : int<monkey>
        }

    let parse (memory : nativeptr<int64>) (lines : StringSplitEnumerator) : Monkey array * nativeptr<int64>[] * int[] =
        use mutable enum = lines
        let output = ResizeArray ()
        let startingItems = ResizeArray ()

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

            startingItems.Add startItems

            {
                OperationIsPlus = operationIsPlus
                Argument = arg
                TestDivisibleBy = test
                TrueCase = ifTrue
                FalseCase = ifFalse
            }
            |> output.Add

        let totalItemCount = startingItems |> Seq.sumBy (fun x -> x.Count)

        let items =
            Array.init
                startingItems.Count
                (fun i ->
                    for j in 0 .. startingItems.[i].Count - 1 do
                        NativePtr.write (NativePtr.add memory (totalItemCount * i + j)) startingItems.[i].[j]

                    NativePtr.add memory (totalItemCount * i)
                )

        let counts = Array.init startingItems.Count (fun i -> startingItems.[i].Count)

        output.ToArray (), items, counts

    let oneRoundDivThree
        (monkeys : IReadOnlyList<Monkey>)
        (items : nativeptr<int64>[])
        (counts : nativeptr<int>)
        (inspections : int array)
        =
        for i in 0 .. monkeys.Count - 1 do
            let monkey = monkeys.[i]
            let countsI = NativePtr.get counts i
            inspections.[i] <- inspections.[i] + countsI

            for worryIndex in 0 .. countsI - 1 do
                let worry = NativePtr.get items.[i] worryIndex

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

                let target = target / 1<monkey>
                let targetCount = NativePtr.get counts target
                NativePtr.write (NativePtr.add items.[target] targetCount) newWorry
                NativePtr.write (NativePtr.add counts target) (targetCount + 1)

            NativePtr.write (NativePtr.add counts i) 0

    let inline maxTwo (arr : int array) : struct (int * int) =
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
        let memory = NativePtr.stackalloc<int64> 1000
        let monkeys, items, counts = parse memory lines
        use counts = fixed counts

        let mutable inspections = Array.zeroCreate<int> monkeys.Length

        for _round in 1..20 do
            oneRoundDivThree monkeys items counts inspections

        let struct (a, b) = maxTwo inspections

        int64 a * int64 b

    let oneRound
        (modulus : int64)
        (monkeys : Monkey array)
        (items : nativeptr<nativeptr<int64>>)
        (counts : nativeptr<int>)
        (inspections : nativeptr<int>)
        =
        for i in 0 .. monkeys.Length - 1 do
            let monkey = monkeys.[i]
            let entry = NativePtr.add inspections i
            let countI = NativePtr.get counts i
            NativePtr.write entry (NativePtr.read entry + countI)

            for worryIndex in 0 .. countI - 1 do
                let worry = NativePtr.get (NativePtr.get items i) worryIndex

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

                let target = target / 1<monkey>
                NativePtr.write (NativePtr.add (NativePtr.get items target) (NativePtr.get counts target)) newWorry
                NativePtr.write (NativePtr.add counts target) (NativePtr.get counts target + 1)

            NativePtr.write (NativePtr.add counts i) 0

    let inline unsafeMaxTwo< ^a when ^a : unmanaged and ^a : comparison> (len : int) (arr : nativeptr<'a>) : struct ('a * 'a) =
        let arr0 = NativePtr.read arr
        let arr1 = NativePtr.get arr 1
        let mutable best = LanguagePrimitives.GenericMaximum arr0 arr1
        let mutable secondBest = min arr0 arr1

        for i in 2 .. len - 1 do
            let arrI = NativePtr.get arr i

            if arrI > best then
                secondBest <- best
                best <- arrI
            elif arrI > secondBest then
                secondBest <- arrI

        struct (secondBest, best)

    let part2 (lines : StringSplitEnumerator) : int64 =
        let memory = NativePtr.stackalloc<int64> 1000
        let monkeys, items, counts = parse memory lines
        use counts = fixed counts
        use items = fixed items

        let inspections = NativePtr.stackalloc<int> monkeys.Length

        let modulus =
            (1L, monkeys) ||> Seq.fold (fun i monkey -> i * monkey.TestDivisibleBy)

        for _round in 1..10000 do
            oneRound modulus monkeys items counts inspections

        let struct (a, b) = unsafeMaxTwo monkeys.Length inspections
        int64 a * int64 b
