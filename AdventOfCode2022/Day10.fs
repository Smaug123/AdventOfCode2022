namespace AdventOfCode2022

open System.Collections.Generic
open System

type Day10Instruction =
    | Noop
    | Addx of int

[<RequireQualifiedAccess>]
module Day10 =

    let parse (lines : StringSplitEnumerator) : Day10Instruction IReadOnlyList =
        use mutable enum = lines
        let output = ResizeArray ()

        for line in enum do
            let line = line.TrimEnd ()

            if not (line.IsWhiteSpace ()) then
                let toAdd =
                    if EfficientString.equals "noop" line then
                        Day10Instruction.Noop
                    else
                        let mutable split = StringSplitEnumerator.make' ' ' line
                        StringSplitEnumerator.chomp "addx" &split
                        let value = StringSplitEnumerator.consumeInt &split

                        if split.MoveNext () then
                            failwith "line had too many spaces"

                        Day10Instruction.Addx value

                output.Add toAdd

        output :> _

    let perform (startState : 'a) (onCycle : 'a -> int -> int -> 'a) (instructions : Day10Instruction IReadOnlyList) =
        let mutable cycle = 0
        let mutable state = startState
        let mutable spritePos = 1

        let inline incrCycle () =
            state <- onCycle state cycle spritePos
            cycle <- cycle + 1

        for instruction in instructions do
            incrCycle ()

            match instruction with
            | Day10Instruction.Noop -> ()
            | Day10Instruction.Addx newX ->
                incrCycle ()
                spritePos <- spritePos + newX

        state

    let part1 (lines : StringSplitEnumerator) : int =
        let instructions = parse lines

        let updateState cost cycle spritePos =
            if cycle % 40 = 19 then
                cost + ((cycle + 1) * spritePos)
            else
                cost

        perform 0 updateState instructions

    let part2 (lines : StringSplitEnumerator) : bool[] =
        let instructions = parse lines
        let arr = Array.zeroCreate<bool> 240

        let updateState (arr : bool[]) cycle spritePos =
            if spritePos - 1 = cycle % 40 && spritePos % 40 <> 0 then
                arr.[cycle] <- true
            elif spritePos + 1 = cycle % 40 && spritePos % 40 <> 39 then
                arr.[cycle] <- true
            elif spritePos = cycle % 40 then
                arr.[cycle] <- true

            arr

        perform arr updateState instructions

    let render (print : string -> unit) (arr : bool[]) : unit =
        for row in Array.chunkBySize 40 arr do
            let toPrint = row |> Array.map (fun s -> if s then '#' else '.') |> System.String
            print toPrint
