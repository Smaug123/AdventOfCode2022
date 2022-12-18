namespace AdventOfCode2022

open System
open System.Collections.Generic
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#else
#nowarn "9"
#endif


[<RequireQualifiedAccess>]
module Day18 =

    /// Returns the points, and also minX, minY, minZ, maxX, maxY, maxZ.
    let parse
        (line : StringSplitEnumerator)
        : struct (int * int * int) ResizeArray * int * int * int * int * int * int
        =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()
        let mutable minX = Int32.MaxValue
        let mutable minY = Int32.MaxValue
        let mutable minZ = Int32.MaxValue
        let mutable maxX = Int32.MinValue
        let mutable maxY = Int32.MinValue
        let mutable maxZ = Int32.MinValue

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable split = StringSplitEnumerator.make' ',' enum.Current
                let x = StringSplitEnumerator.consumeInt &split

                if x < minX then
                    minX <- x

                if x > maxX then
                    maxX <- x

                let y = StringSplitEnumerator.consumeInt &split

                if y < minY then
                    minY <- y

                if y > maxY then
                    maxY <- y

                let z = StringSplitEnumerator.consumeInt &split

                if z < minZ then
                    minZ <- z

                if z > maxZ then
                    maxZ <- z

                assert (not (split.MoveNext ()))
                output.Add (struct (x, y, z))

        output, minX, minY, minZ, maxX, maxY, maxZ

    let inline private doPart1
        (cubes : ResizeArray<_>)
        (arr : Arr3D<int>)
        (minX : int)
        (minY : int)
        (minZ : int)
        maxX
        maxY
        maxZ
        =
        let mutable exposedFaces = 0

        let maxX = maxX - minX
        let maxY = maxY - minY
        let maxZ = maxZ - minZ

        for i in 0 .. cubes.Count - 1 do
            let struct (x, y, z) = cubes.[i]
            let x = x - minX
            let y = y - minY
            let z = z - minZ

            if not (x > 0 && Arr3D.get arr (x - 1) y z = 1) then
                exposedFaces <- exposedFaces + 1

            if not (x < maxX && Arr3D.get arr (x + 1) y z = 1) then
                exposedFaces <- exposedFaces + 1

            if not (y > 0 && Arr3D.get arr x (y - 1) z = 1) then
                exposedFaces <- exposedFaces + 1

            if not (y < maxY && Arr3D.get arr x (y + 1) z = 1) then
                exposedFaces <- exposedFaces + 1

            if not (z > 0 && Arr3D.get arr x y (z - 1) = 1) then
                exposedFaces <- exposedFaces + 1

            if not (z < maxZ && Arr3D.get arr x y (z + 1) = 1) then
                exposedFaces <- exposedFaces + 1

        exposedFaces

    let part1 (line : StringSplitEnumerator) : int =
        let cubes, minX, minY, minZ, maxX, maxY, maxZ = parse line

        let xSpan = maxX - minX + 1
        let ySpan = maxY - minY + 1
        let zSpan = maxZ - minZ + 1

#if DEBUG
        let arr = Arr3D.zeroCreate<int> xSpan ySpan zSpan
#else
        let backing = Array.zeroCreate<int> (xSpan * ySpan * zSpan)
        use ptr = fixed backing
        let arr = Arr3D.zeroCreate<int> ptr xSpan ySpan zSpan
#endif

        for i in 0 .. cubes.Count - 1 do
            let struct (x, y, z) = cubes.[i]
            Arr3D.set arr (x - minX) (y - minY) (z - minZ) 1

        doPart1 cubes arr minX minY minZ maxX maxY maxZ

    // Semantics:
    // 3 means "in progress",
    // 2 means "this definitely flood fills to the outside",
    // 1 means "definitely full",
    // 0 means "initially empty",
    let floodFill (arr : Arr3D<int>) maxX maxY maxZ (x : int) (y : int) (z : int) : unit =
        /// Returns true if it hits the outside.
        let rec go (x : int) (y : int) (z : int) : bool =
            let mutable hitsOutside = false

            match Arr3D.get arr x y z with
            | 0 ->
                Arr3D.set arr x y z 3

                hitsOutside <- hitsOutside || (if x > 0 then go (x - 1) y z else true)

                hitsOutside <- hitsOutside || (if y > 0 then go x (y - 1) z else true)

                hitsOutside <- hitsOutside || (if z > 0 then go x y (z - 1) else true)

                hitsOutside <- hitsOutside || (if x < maxX then go (x + 1) y z else true)

                hitsOutside <- hitsOutside || (if y < maxY then go x (y + 1) z else true)

                hitsOutside <- hitsOutside || (if z < maxZ then go x y (z + 1) else true)
            | 2 -> hitsOutside <- true
            | _ -> ()

            hitsOutside

        if go x y z then
            // Convert all our "in progress" to "flood fills to outside".
            for x in 0..maxX do
                for y in 0..maxY do
                    for z in 0..maxZ do
                        if Arr3D.get arr x y z = 3 then
                            Arr3D.set arr x y z 2
        else
            // Convert all our "in progress" to "does not flood fill to outside".
            for x in 0..maxX do
                for y in 0..maxY do
                    for z in 0..maxZ do
                        if Arr3D.get arr x y z = 3 then
                            Arr3D.set arr x y z 1

    let part2 (line : StringSplitEnumerator) : int =
        let cubes, minX, minY, minZ, maxX, maxY, maxZ = parse line

        let xSpan = maxX - minX + 1
        let ySpan = maxY - minY + 1
        let zSpan = maxZ - minZ + 1

#if DEBUG
        let arr = Arr3D.zeroCreate<int> xSpan ySpan zSpan
#else
        let backing = Array.zeroCreate<int> (xSpan * ySpan * zSpan)
        use ptr = fixed backing
        let arr = Arr3D.zeroCreate<int> ptr xSpan ySpan zSpan
#endif

        for i in 0 .. cubes.Count - 1 do
            let struct (x, y, z) = cubes.[i]
            Arr3D.set arr (x - minX) (y - minY) (z - minZ) 1

        // Flood-fill the internals.
        for x in 0 .. maxX - minX do
            for y in 0 .. maxY - minY do
                for z in 0 .. maxZ - minZ do
                    floodFill arr (maxX - minX) (maxY - minY) (maxZ - minZ) x y z

        doPart1 cubes arr minX minY minZ maxX maxY maxZ
