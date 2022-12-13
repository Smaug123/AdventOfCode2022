namespace AdventOfCode2022

open System

#if DEBUG
open Checked
#endif

type Day13Packet =
    | PacketList of Day13Packet[]
    | Int of int

[<RequireQualifiedAccess>]
module Day13 =

    /// True if they're in the right order.
    let rec cmp (packet1 : Day13Packet) (packet2 : Day13Packet) : bool option =
        match packet1, packet2 with
        | Day13Packet.Int i1, Day13Packet.Int i2 ->
            if i1 < i2 then Some true
            elif i1 > i2 then Some false
            else None
        | PacketList [||], PacketList [||] -> None
        | PacketList [||], PacketList _ -> Some true
        | PacketList _, PacketList [||] -> Some false
        | PacketList arr1, PacketList arr2 ->
            match cmp arr1.[0] arr2.[0] with
            | Some v -> Some v
            | None -> cmp (PacketList arr1.[1..]) (PacketList arr2.[1..])
        | PacketList _, Int _ -> cmp packet1 (PacketList [| packet2 |])
        | Int _, PacketList _ -> cmp (PacketList [| packet1 |]) packet2

    let parse (lines : StringSplitEnumerator) : Day13Packet ResizeArray =
        use mutable enum = lines
        let output = ResizeArray ()
        let mutable stack = []

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let line = enum.Current.TrimEnd ()
                assert (line.[0] = '[')
                let mutable elements = StringSplitEnumerator.make' ',' line

                while elements.MoveNext () do
                    let mutable curr = elements.Current

                    while curr.[0] = '[' do
                        stack <- ResizeArray () :: stack
                        curr <- curr.Slice 1

                    if curr.[0] <> ']' then
                        (List.head stack).Add (Day13Packet.Int (Int32.Parse (curr.TrimEnd (']'))))

                    while curr.Length > 0 && curr.[curr.Length - 1] = ']' do
                        let closed = Day13Packet.PacketList (stack.Head.ToArray ())

                        match stack with
                        | [] -> failwith "oh no"
                        | [ head ] -> output.Add closed
                        | head :: tail -> tail.Head.Add closed

                        stack <- stack.Tail
                        curr <- curr.Slice (0, curr.Length - 1)

        output

    let part1 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        let mutable sum = 0

        for i in 0..2 .. data.Count - 1 do
            match cmp data.[i] data.[i + 1] with
            | None -> failwith "should have decided"
            | Some false -> ()
            | Some true -> sum <- sum + (i / 2 + 1)

        sum


    let part2 (lines : StringSplitEnumerator) : int =
        let data = parse lines

        let marker1 =
            Day13Packet.PacketList [| Day13Packet.PacketList [| Day13Packet.Int 2 |] |]

        data.Add marker1

        let marker2 =
            Day13Packet.PacketList [| Day13Packet.PacketList [| Day13Packet.Int 6 |] |]

        data.Add marker2
        let data = data.ToArray () |> Array.indexed
        Array.sortInPlaceWith (fun (_, a) (_, b) -> if Option.get (cmp a b) then -1 else 1) data

        let mutable answer = 1
        let mutable keepGoing = true
        let mutable i = 0

        while keepGoing do
            if answer = 1 && fst data.[i] = data.Length - 2 then
                answer <- answer * (i + 1)
            elif fst data.[i] = data.Length - 1 then
                answer <- answer * (i + 1)
                keepGoing <- false

            i <- i + 1

        answer
