namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day16 =

    let parse (lines : StringSplitEnumerator) : int ResizeArray =
        use mutable enum = lines
        let sensors = ResizeArray ()
        let beacons = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let line = enum.Current.TrimEnd ()
                let mutable equalsSplit = StringSplitEnumerator.make' '=' line
                StringSplitEnumerator.chomp "Sensor at x" &equalsSplit

                if not (equalsSplit.MoveNext ()) then
                    failwith "expected another entry"

                let x = Int32.Parse (equalsSplit.Current.Slice (0, equalsSplit.Current.IndexOf ','))

                if not (equalsSplit.MoveNext ()) then
                    failwith "expected another entry"

                let y = Int32.Parse (equalsSplit.Current.Slice (0, equalsSplit.Current.IndexOf ':'))

                let sensor =
                    {
                        X = x
                        Y = y
                    }

                if not (equalsSplit.MoveNext ()) then
                    failwith "expected another entry"

                let x = Int32.Parse (equalsSplit.Current.Slice (0, equalsSplit.Current.IndexOf ','))

                if not (equalsSplit.MoveNext ()) then
                    failwith "expected another entry"

                let y = Int32.Parse equalsSplit.Current

                let closest =
                    {
                        X = x
                        Y = y
                    }

                sensors.Add sensor
                beacons.Add closest

        failwith ""


    let part1 (lines : StringSplitEnumerator) : int =
        let sensors = parse lines

        0

    let part2 (lines : StringSplitEnumerator) : int64 =
        let sensors = parse lines

        0L
