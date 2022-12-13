namespace AdventOfCode2022

open System
open System.Collections.Generic
open System.Globalization

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day14 =

    let parse (lines : StringSplitEnumerator) : int ResizeArray =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable stack = Stack<_> ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let line = enum.Current.TrimEnd ()
                ()
        output

    let part1 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        0


    let part2 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        0
