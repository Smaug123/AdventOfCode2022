namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

[<RequireQualifiedAccess>]
module Day21 =

    type Day21Operation =
        | Add = 0
        | Times = 1
        | Divide = 2
        | Minus = 3

    let parseOp (c : char) =
        match c with
        | '+' -> Day21Operation.Add
        | '*' -> Day21Operation.Times
        | '/' -> Day21Operation.Divide
        | '-' -> Day21Operation.Minus
        | _ -> failwithf "bad op char: %c" c

    type Day21Expression =
        | Literal of int
        | Operation of string * string * Day21Operation
        | Calculated of float

    let parse (line : StringSplitEnumerator) : Dictionary<string, Day21Expression> =
        use mutable enum = line.GetEnumerator ()
        let output = Dictionary ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable line = StringSplitEnumerator.make' ':' enum.Current

                if not (line.MoveNext ()) then
                    failwith "expected nonempty"

                let name = line.Current.ToString ()

                if not (line.MoveNext ()) then
                    failwith "expected a RHS"

                let rhs = line.Current.Trim ()
                let mutable rhs = StringSplitEnumerator.make' ' ' rhs

                if not (rhs.MoveNext ()) then
                    failwith "expected space on RHS"

                let expr =
                    match Int32.TryParse rhs.Current with
                    | true, v ->
                        if rhs.MoveNext () then
                            failwithf "bad assumption: number %i followed by more text" v

                        Day21Expression.Literal v
                    | false, _ ->
                        let s1 = rhs.Current.ToString ()

                        if not (rhs.MoveNext ()) then
                            failwith "bad assumption: no operation"

                        let op = parseOp rhs.Current.[0]

                        if not (rhs.MoveNext ()) then
                            failwith "bad assumption: no second operand"

                        let s2 = rhs.Current.ToString ()

                        if rhs.MoveNext () then
                            failwith "bad assumption: stuff came after second operand"

                        Day21Expression.Operation (s1, s2, op)

                output.Add (name, expr)

        output

    let rec evaluate (d : Dictionary<string, Day21Expression>) (s : string) : float =
        match d.[s] with
        | Day21Expression.Literal v ->
            let result = float v
            d.[s] <- Day21Expression.Calculated result
            result
        | Day21Expression.Calculated f -> f
        | Day21Expression.Operation (s1, s2, op) ->
            let v1 = evaluate d s1
            let v2 = evaluate d s2
            let result =
                match op with
                | Day21Operation.Add -> v1 + v2
                | Day21Operation.Times -> v1 * v2
                | Day21Operation.Minus -> v1 - v2
                | Day21Operation.Divide ->
                    v1 / v2
                | _ -> failwith "bad enum"
            d.[s] <- Day21Expression.Calculated result
            result

    let part1 (lines : StringSplitEnumerator) : int64 =
        let original = parse lines

        let result = evaluate original "root"
        let rounded = int64 result
        if abs (float rounded - result) > 0.00000001 then
            failwith "not an int"
        rounded

    let part2 (lines : StringSplitEnumerator) : int =
        let original = parse lines

        -1
