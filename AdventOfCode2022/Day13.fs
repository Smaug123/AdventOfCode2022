namespace AdventOfCode2022

open System
open System.Collections.Generic
open System.Globalization
open System.Runtime.CompilerServices

#if DEBUG
open Checked
#endif

[<Struct>]
[<IsReadOnly>]
type Day13Packet =
    {
        Packet : Day13Packet ResizeArray ValueOption
        /// If Packet is None, this is an int.
        /// If Packet is Some, this is the start index of a slice.
        StartIndexOrInt : int
    }

[<RequireQualifiedAccess>]
module Day13 =

    /// True if they're in the right order.
    let rec cmp (packet1 : Day13Packet) (packet2 : Day13Packet) : bool ValueOption =
        match packet1.Packet, packet2.Packet with
        | ValueNone, ValueNone ->
            if packet1.StartIndexOrInt < packet2.StartIndexOrInt then
                ValueSome true
            elif packet1.StartIndexOrInt > packet2.StartIndexOrInt then
                ValueSome false
            else
                ValueNone
        | ValueSome arr1, ValueSome arr2 ->
            if packet1.StartIndexOrInt = arr1.Count then
                if packet2.StartIndexOrInt = arr2.Count then
                    ValueNone
                else
                    ValueSome true
            elif packet2.StartIndexOrInt = arr2.Count then
                ValueSome false
            else
                match cmp arr1.[packet1.StartIndexOrInt] arr2.[packet2.StartIndexOrInt] with
                | ValueSome v -> ValueSome v
                | ValueNone ->
                    let advanced1 =
                        {
                            Packet = ValueSome arr1
                            StartIndexOrInt = packet1.StartIndexOrInt + 1
                        }

                    let advanced2 =
                        {
                            Packet = ValueSome arr2
                            StartIndexOrInt = packet2.StartIndexOrInt + 1
                        }

                    cmp advanced1 advanced2
        | ValueSome arr1, ValueNone ->
            if packet1.StartIndexOrInt = arr1.Count then
                ValueSome true
            else
                match cmp arr1.[packet1.StartIndexOrInt] packet2 with
                | ValueSome v -> ValueSome v
                | ValueNone ->
                    if packet1.StartIndexOrInt + 1 = arr1.Count then
                        ValueNone
                    else
                        ValueSome false
        | ValueNone, ValueSome arr2 ->
            if packet2.StartIndexOrInt = arr2.Count then
                ValueSome false
            else
                match cmp packet1 arr2.[packet2.StartIndexOrInt] with
                | ValueSome v -> ValueSome v
                | ValueNone ->
                    if packet2.StartIndexOrInt + 1 = arr2.Count then
                        ValueNone
                    else
                        ValueSome true

    let parse (lines : StringSplitEnumerator) : Day13Packet ResizeArray =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable stack = Stack<_> ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let line = enum.Current.TrimEnd ()
                assert (line.[0] = '[')
                let mutable elements = StringSplitEnumerator.make' ',' line

                while elements.MoveNext () do
                    let mutable curr = elements.Current

                    while curr.[0] = '[' do
                        stack.Push (ResizeArray ())
                        curr <- curr.Slice 1

                    if curr.[0] <> ']' then
                        let int =
                            {
                                StartIndexOrInt = Int32.Parse (curr.TrimEnd ']', NumberStyles.None)
                                Packet = ValueNone
                            }

                        stack.Peek().Add int

                    while curr.Length > 0 && curr.[curr.Length - 1] = ']' do
                        let closed =
                            {
                                Packet = ValueSome (stack.Pop ())
                                StartIndexOrInt = 0
                            }

                        if stack.Count = 0 then
                            output.Add closed
                        else
                            stack.Peek().Add closed

                        curr <- curr.Slice (0, curr.Length - 1)

        output

    let part1 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        let mutable sum = 0

        for i in 0..2 .. data.Count - 1 do
            match cmp data.[i] data.[i + 1] with
            | ValueNone -> failwith "should have decided"
            | ValueSome false -> ()
            | ValueSome true -> sum <- sum + (i / 2 + 1)

        sum

    let part2 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        let marker1 =
            let arr = ResizeArray 1
            let arr2 = ResizeArray 1

            arr2.Add
                {
                    StartIndexOrInt = 2
                    Packet = ValueNone
                }

            arr.Add
                {
                    StartIndexOrInt = 0
                    Packet = ValueSome arr2
                }

            {
                Packet = ValueSome arr
                StartIndexOrInt = 0
            }

        let marker2 =
            let arr = ResizeArray 1
            let arr2 = ResizeArray 1

            arr2.Add
                {
                    StartIndexOrInt = 6
                    Packet = ValueNone
                }

            arr.Add
                {
                    StartIndexOrInt = 0
                    Packet = ValueSome arr2
                }

            {
                Packet = ValueSome arr
                StartIndexOrInt = 0
            }

        let mutable howManyLessThan = 0
        let mutable howManyGreaterThan = 0

        for i in 0 .. data.Count - 1 do
            match cmp data.[i] marker1 with
            | ValueSome true -> howManyLessThan <- howManyLessThan + 1
            | _ ->
                match cmp marker2 data.[i] with
                | ValueSome true -> howManyGreaterThan <- howManyGreaterThan + 1
                | _ -> ()

        (howManyLessThan + 1) * (data.Count - howManyGreaterThan + 2)
