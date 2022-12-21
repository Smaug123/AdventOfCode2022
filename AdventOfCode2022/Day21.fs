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

    let inline parseOp (c : char) =
        match c with
        | '+' -> Day21Operation.Add
        | '*' -> Day21Operation.Times
        | '/' -> Day21Operation.Divide
        | '-' -> Day21Operation.Minus
        | _ -> failwithf "bad op char: %c" c

    type Day21Input =
        | Literal of int
        | Operation of string * string * Day21Operation
        | Calculated of float

    let parse (line : StringSplitEnumerator) : Dictionary<string, Day21Input> =
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

                        Day21Input.Literal v
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

                        Day21Input.Operation (s1, s2, op)

                output.Add (name, expr)

        output

    let inline compute (v1 : float) (v2 : float) (op : Day21Operation) : float =
        match op with
        | Day21Operation.Add -> v1 + v2
        | Day21Operation.Times -> v1 * v2
        | Day21Operation.Minus -> v1 - v2
        | Day21Operation.Divide -> v1 / v2
        | _ -> failwith "bad enum"

    let rec evaluate (d : Dictionary<string, Day21Input>) (s : string) : float =
        match d.[s] with
        | Day21Input.Literal v ->
            let result = float v
            d.[s] <- Day21Input.Calculated result
            result
        | Day21Input.Calculated f -> f
        | Day21Input.Operation (s1, s2, op) ->
            let v1 = evaluate d s1
            let v2 = evaluate d s2

            let result = compute v1 v2 op

            d.[s] <- Day21Input.Calculated result
            result

    let inline round (v : float) : int64 =
        let rounded = int64 v

        if abs (float rounded - v) > 0.00000001 then
            failwith "not an int"

        rounded

    let part1 (lines : StringSplitEnumerator) : int64 =
        let original = parse lines

        let result = evaluate original "root"

        round result

    type Day21Expr =
        | Literal of float
        | Variable
        | Calc of Day21Expr * Day21Expr * Day21Operation

    let rec convert
        (key : string)
        (d : Dictionary<string, Day21Input>)
        (result : Dictionary<string, Day21Expr>)
        : Day21Expr
        =
        match result.TryGetValue key with
        | true, v -> v
        | false, _ ->

        if key = "humn" then
            let answer = Day21Expr.Variable
            result.["humn"] <- answer
            answer
        else

        match d.[key] with
        | Day21Input.Literal v ->
            let answer = Day21Expr.Literal (float v)
            result.[key] <- answer
            answer
        | Day21Input.Calculated _ -> failwith "no never"
        | Day21Input.Operation (s1, s2, op) ->
            let v1 = convert s1 d result
            let v2 = convert s2 d result
            // One wave of simplification
            let answer =
                match v1, v2 with
                | Day21Expr.Literal l1, Day21Expr.Literal l2 -> Day21Expr.Literal (compute l1 l2 op)
                | _, _ -> Day21Expr.Calc (v1, v2, op)

            result.[key] <- answer
            answer

    let part2 (lines : StringSplitEnumerator) : int64 =
        let original = parse lines

        let lhs, rhs =
            match original.["root"] with
            | Day21Input.Literal _
            | Day21Input.Calculated _ -> failwith "expected operation"
            | Day21Input.Operation (s1, s2, _) -> s1, s2

        let converted = Dictionary ()
        let mutable lhs = convert lhs original converted
        let mutable rhs = convert rhs original converted

        let mutable answer = nan

        while Double.IsNaN answer do
            match lhs, rhs with
            | Day21Expr.Literal l, Day21Expr.Variable
            | Day21Expr.Variable, Day21Expr.Literal l -> answer <- l
            | Day21Expr.Literal l, Day21Expr.Calc (v1, v2, op)
            | Day21Expr.Calc (v1, v2, op), Day21Expr.Literal l ->
                match v1, v2 with
                | v1, Day21Expr.Literal v2 ->
                    lhs <- v1

                    rhs <-
                        match op with
                        | Day21Operation.Add -> Day21Expr.Literal (l - v2)
                        | Day21Operation.Times -> Day21Expr.Literal (l / v2)
                        | Day21Operation.Divide -> Day21Expr.Literal (l * v2)
                        | Day21Operation.Minus -> Day21Expr.Literal (l + v2)
                        | _ -> failwith "bad op"
                | Day21Expr.Literal v1, v2 ->
                    lhs <- v2

                    rhs <-
                        match op with
                        | Day21Operation.Add -> Day21Expr.Literal (l - v1)
                        | Day21Operation.Times -> Day21Expr.Literal (l / v1)
                        | Day21Operation.Divide -> Day21Expr.Literal (v1 / l)
                        | Day21Operation.Minus -> Day21Expr.Literal (v1 - l)
                        | _ -> failwith "bad op"
                | _, _ -> failwith "problem is too hard: had variables on both sides"
            | Day21Expr.Variable, Day21Expr.Variable
            | Day21Expr.Variable, Day21Expr.Calc _
            | Day21Expr.Calc _, Day21Expr.Calc _
            | Day21Expr.Calc _, Day21Expr.Variable -> failwith "one side is always a literal"
            | Day21Expr.Literal _, Day21Expr.Literal _ -> failwith "can't both be literals"

        round answer
