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

    type Day21Name = int

    type Day21Input =
        | Literal of int
        | Operation of Day21Name * Day21Name * Day21Operation
        | Calculated of float

    /// Returns the name of the root node and human node, too.
    let parse (line : StringSplitEnumerator) : Day21Input[] * Day21Name * Day21Name =
        use mutable enum = line.GetEnumerator ()
        let output = Dictionary ()
        let mutable nodeCount = 0
        let nodeMapping = Dictionary<string, int> ()

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

                        let s1 =
                            match nodeMapping.TryGetValue s1 with
                            | false, _ ->
                                nodeMapping.Add (s1, nodeCount)
                                nodeCount <- nodeCount + 1
                                nodeCount - 1
                            | true, v -> v

                        let s2 =
                            match nodeMapping.TryGetValue s2 with
                            | false, _ ->
                                nodeMapping.Add (s2, nodeCount)
                                nodeCount <- nodeCount + 1
                                nodeCount - 1
                            | true, v -> v

                        Day21Input.Operation (s1, s2, op)

                let name =
                    match nodeMapping.TryGetValue name with
                    | false, _ ->
                        nodeMapping.Add (name, nodeCount)
                        nodeCount <- nodeCount + 1
                        nodeCount - 1
                    | true, v -> v

                output.Add (name, expr)

        let outputArr = Array.zeroCreate nodeCount

        for KeyValue (name, value) in output do
            outputArr.[name] <- value

        outputArr, nodeMapping.["root"], nodeMapping.["humn"]

    let inline compute (v1 : float) (v2 : float) (op : Day21Operation) : float =
        match op with
        | Day21Operation.Add -> v1 + v2
        | Day21Operation.Times -> v1 * v2
        | Day21Operation.Minus -> v1 - v2
        | Day21Operation.Divide -> v1 / v2
        | _ -> failwith "bad enum"

    let rec evaluate (d : Day21Input[]) (s : Day21Name) : float =
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
        let original, root, _ = parse lines

        let result = evaluate original root

        round result

    type Day21Expr =
        | Literal of float
        | Variable
        | Calc of Day21Expr * Day21Expr * Day21Operation

    let rec convert
        (human : Day21Name)
        (key : Day21Name)
        (d : Day21Input[])
        (result : Day21Expr ValueOption[])
        : Day21Expr
        =
        match result.[key] with
        | ValueSome v -> v
        | ValueNone ->

        if key = human then
            let answer = Day21Expr.Variable
            result.[human] <- ValueSome answer
            answer
        else

        match d.[key] with
        | Day21Input.Literal v ->
            let answer = Day21Expr.Literal (float v)
            result.[key] <- ValueSome answer
            answer
        | Day21Input.Calculated _ -> failwith "no never"
        | Day21Input.Operation (s1, s2, op) ->
            let v1 = convert human s1 d result
            let v2 = convert human s2 d result
            // One wave of simplification
            let answer =
                match v1, v2 with
                | Day21Expr.Literal l1, Day21Expr.Literal l2 -> Day21Expr.Literal (compute l1 l2 op)
                | _, _ -> Day21Expr.Calc (v1, v2, op)

            result.[key] <- ValueSome answer
            answer

    let part2 (lines : StringSplitEnumerator) : int64 =
        let original, root, human = parse lines

        let lhs, rhs =
            match original.[root] with
            | Day21Input.Literal _
            | Day21Input.Calculated _ -> failwith "expected operation"
            | Day21Input.Operation (s1, s2, _) -> s1, s2

        let converted = Array.zeroCreate original.Length
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
