namespace AdventOfCode2022

open System
open System.Collections.Generic

#if DEBUG
open Checked
#endif

[<RequireQualifiedAccess>]
module Day15 =

    let parse (lines : StringSplitEnumerator) : Coordinate ResizeArray * Coordinate ResizeArray =
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

        sensors, beacons


    let inline manhattan (p1 : Coordinate) (p2 : Coordinate) : int = abs (p1.X - p2.X) + abs (p1.Y - p2.Y)

    let inline couldBeBeacon
        (sensors : ResizeArray<Coordinate>)
        (closestManhattans : int[])
        (point : Coordinate)
        : bool
        =
        let mutable keepGoing = true
        let mutable i = 0

        while keepGoing && i < sensors.Count do
            if manhattan point sensors.[i] <= closestManhattans.[i] then
                keepGoing <- false

            i <- i + 1

        keepGoing

    let toHashSet (arr : Coordinate ResizeArray) : Coordinate HashSet =
        let output = HashSet ()

        for beacon in arr do
            output.Add beacon |> ignore

        output


    let part1 (y : int) (lines : StringSplitEnumerator) : int =
        let sensors, beacons = parse lines

        // The ith sensor has no beacons with Manhattan distance smaller than closestManhattans.[i]
        let closestManhattans =
            Array.init sensors.Count (fun i -> manhattan sensors.[i] beacons.[i])

        let mutable minX = Int32.MaxValue
        let mutable maxX = Int32.MinValue

        let mutable i = 0

        for point in sensors do
            minX <- min minX (point.X - closestManhattans.[i])
            maxX <- max maxX (point.X + closestManhattans.[i])
            i <- i + 1

        let mutable count = 0

        for x in minX..maxX do
            let point =
                {
                    X = x
                    Y = y
                }

            if not (couldBeBeacon sensors closestManhattans point) then
                count <- count + 1

        // We've overcounted by the number of beacons in the row - they not only *could* be beacons, they *are* beacons!
        let beacons = toHashSet beacons

        for beacon in beacons do
            if minX <= beacon.X && beacon.X <= maxX && beacon.Y = y then
                count <- count - 1

        count

    let part2 (maxSearch : int) (lines : StringSplitEnumerator) : int64 =
        let sensors, beacons = parse lines

        let sensorXCoords =
            let arr = Array.zeroCreate (sensors.Count + 2)

            for i in 0 .. sensors.Count - 1 do
                arr.[i] <- sensors.[i].X

            arr.[sensors.Count] <- maxSearch
            Array.sortInPlace arr
            arr

        let sensorYCoords =
            let arr = Array.zeroCreate (sensors.Count + 2)

            for i in 0 .. sensors.Count - 1 do
                arr.[i] <- sensors.[i].Y

            arr.[sensors.Count] <- maxSearch
            Array.sortInPlace arr
            arr

        let mutable answer = -1L

        let mutable xIndex = 0

        while answer <> 1L && xIndex < sensorXCoords.Length - 1 do
            let xMin = sensorXCoords.[xIndex]
            xIndex <- xIndex + 1
            let xMax = sensorXCoords.[xIndex]

            let mutable yIndex = 0

            while answer <> 1L && yIndex < sensorYCoords.Length - 1 do
                let yMin = sensorYCoords.[yIndex]
                yIndex <- yIndex + 1
                let yMax = sensorYCoords.[yIndex]

                let mutable minusXMinusYConstraint = Int32.MaxValue
                let mutable minusXPlusYConstraint = Int32.MaxValue
                let mutable plusXMinusYConstraint = Int32.MaxValue
                let mutable plusXPlusYConstraint = Int32.MaxValue

                for i in 0 .. sensors.Count - 1 do
                    let sensor = sensors.[i]
                    let beacon = beacons.[i]
                    let manhattan = abs (sensor.X - beacon.X) + abs (sensor.Y - beacon.Y)

                    if sensor.X <= xMax then
                        if sensor.Y <= yMax then
                            minusXMinusYConstraint <- min minusXMinusYConstraint (-manhattan - sensor.X - sensor.Y - 1)
                        else
                            minusXPlusYConstraint <- min minusXPlusYConstraint (-manhattan - sensor.X + sensor.Y - 1)
                    else if sensor.Y <= yMax then
                        plusXMinusYConstraint <- min plusXMinusYConstraint (-manhattan + sensor.X - sensor.Y - 1)
                    else
                        plusXPlusYConstraint <- min plusXPlusYConstraint (-manhattan + sensor.X + sensor.Y - 1)

                let yMax =
                    if
                        plusXPlusYConstraint <> Int32.MaxValue
                        && plusXMinusYConstraint <> Int32.MaxValue
                    then
                        min ((plusXPlusYConstraint - plusXMinusYConstraint) / 2) yMax
                    else
                        yMax

                let xMax =
                    if
                        plusXPlusYConstraint <> Int32.MaxValue
                        && minusXPlusYConstraint <> Int32.MaxValue
                    then
                        min ((plusXPlusYConstraint - minusXPlusYConstraint) / 2) xMax
                    else
                        xMax

                let xMin =
                    if
                        minusXMinusYConstraint <> Int32.MaxValue
                        && minusXPlusYConstraint <> Int32.MaxValue
                    then
                        max xMin (-(minusXMinusYConstraint + minusXPlusYConstraint) / 2)
                    else
                        xMin

                let yMin =
                    if
                        minusXMinusYConstraint <> Int32.MaxValue
                        && plusXMinusYConstraint <> Int32.MaxValue
                    then
                        max yMin (-(minusXMinusYConstraint + plusXMinusYConstraint) / 2)
                    else
                        yMin

                // (fst constraints).{x, y} <= (snd constraints), and also xMin <= x <= xMax and
                // yMin <= y <= yMax.
                let mutable falsified = false

                if minusXMinusYConstraint <> Int32.MaxValue then
                    // Have -x-y <= upperBound1, i.e. -upperBound1 <= x+y,
                    // which we could falsify if -upperBound1 > x+y.
                    if xMax + yMax < -minusXMinusYConstraint then
                        falsified <- true

                if minusXPlusYConstraint <> Int32.MaxValue then
                    // Have -x + y <= upperBound1, which we could falsify if y > upperBound1 + x
                    if yMin > minusXPlusYConstraint + xMax then
                        falsified <- true

                if plusXPlusYConstraint <> Int32.MaxValue then
                    // Have x + y <= upperBound1, which we could falsify always if xMin + yMin > upperBound1.
                    if xMin + yMin > plusXPlusYConstraint then
                        falsified <- true

                if plusXMinusYConstraint <> Int32.MaxValue then
                    // Have x - y <= upperBound1, which we could falsify if x > upperBound1 + y
                    if xMin > plusXMinusYConstraint + yMax then
                        falsified <- true

                if not falsified then
                    answer <- int64 xMax * 4000000L + int64 yMax

        answer
