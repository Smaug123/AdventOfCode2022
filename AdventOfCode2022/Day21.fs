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

    type Day21Name = string

    type Day21Input =
        | Literal of int
        | Operation of Day21Name * Day21Name * Day21Operation

    /// Returns the name of the root node and human node, too.
    let parse (line : StringSplitEnumerator) : Dictionary<Day21Name, Day21Input> * Day21Name * Day21Name =
        use mutable enum = line.GetEnumerator ()
        let output = Dictionary ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let colon = enum.Current.IndexOf ':'
                let name = enum.Current.Slice(0, colon).ToString ()

                let rhs = enum.Current.Slice(colon + 2).TrimEnd ()

                let expr =
                    match Int32.TryParse rhs with
                    | true, v -> Day21Input.Literal v
                    | false, _ ->
                        let space1 = rhs.IndexOf ' '
                        let space2 = rhs.LastIndexOf ' '
                        let s1 = rhs.Slice(0, space1).ToString ()

                        let op =
                            let op = rhs.Slice (space1 + 1, space2 - space1 - 1)

                            if op.Length <> 1 then
                                failwithf "Expected exactly one char for op, got %i" op.Length

                            parseOp op.[0]

                        let s2 = rhs.Slice(space2 + 1).ToString ()

                        Day21Input.Operation (s1, s2, op)

                output.Add (name, expr)

        output, "root", "humn"

    let inline compute (v1 : float) (v2 : float) (op : Day21Operation) : float =
        match op with
        | Day21Operation.Add -> v1 + v2
        | Day21Operation.Times -> v1 * v2
        | Day21Operation.Minus -> v1 - v2
        | Day21Operation.Divide -> v1 / v2
        | _ -> failwith "bad enum"

    let rec evaluate
        (calculated : Dictionary<Day21Name, float>)
        (d : Dictionary<Day21Name, Day21Input>)
        (s : Day21Name)
        : float
        =
        match calculated.TryGetValue s with
        | true, v -> v
        | false, _ ->

        match d.[s] with
        | Day21Input.Literal v ->
            let result = float v
            calculated.[s] <- result
            result
        | Day21Input.Operation (s1, s2, op) ->
            let v1 = evaluate calculated d s1
            let v2 = evaluate calculated d s2

            let result = compute v1 v2 op

            calculated.[s] <- result
            result

    let inline round (v : float) : int64 =
        let rounded = int64 v

        if abs (float rounded - v) > 0.00000001 then
            failwith "not an int"

        rounded

    let part1 (lines : StringSplitEnumerator) : int64 =
        let original, root, _ = parse lines
        let calculated = Dictionary original.Count

        let result = evaluate calculated original root

        round result

    type Day21Expr =
        | Literal of float
        | Variable
        | Calc of Day21Expr * Day21Expr * Day21Operation

    let rec convert
        (human : Day21Name)
        (key : Day21Name)
        (d : Dictionary<Day21Name, Day21Input>)
        (result : Dictionary<Day21Name, Day21Expr>)
        : Day21Expr
        =
        match result.TryGetValue key with
        | true, v -> v
        | false, _ ->

        if key = human then
            let answer = Day21Expr.Variable
            result.[human] <- answer
            answer
        else

        match d.[key] with
        | Day21Input.Literal v ->
            let answer = Day21Expr.Literal (float v)
            result.[key] <- answer
            answer
        | Day21Input.Operation (s1, s2, op) ->
            let v1 = convert human s1 d result
            let v2 = convert human s2 d result
            // One wave of simplification
            let answer =
                match v1, v2 with
                | Day21Expr.Literal l1, Day21Expr.Literal l2 -> Day21Expr.Literal (compute l1 l2 op)
                | _, _ -> Day21Expr.Calc (v1, v2, op)

            result.[key] <- answer
            answer

    let part2 (lines : StringSplitEnumerator) : int64 =
        let original, root, human = parse lines

        let lhs, rhs =
            match original.[root] with
            | Day21Input.Literal _ -> failwith "expected operation"
            | Day21Input.Operation (s1, s2, _) -> s1, s2

        let converted = Dictionary original.Count
        let mutable lhs = convert human lhs original converted
        let mutable rhs = convert human rhs original converted

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
