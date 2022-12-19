namespace AdventOfCode2022

open System
open FSharp.Collections.ParallelSeq

#if DEBUG
open Checked
#else
#nowarn "9"
#endif

type Day19Blueprint =
    {
        Index : int
        Ore : int
        Clay : int
        ObsidianOre : int
        ObsidianClay : int
        GeodeOre : int
        GeodeObsidian : int
    }

[<RequireQualifiedAccess>]
module Day19 =

    //Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 4 ore. Each obsidian robot costs 4 ore and 8 clay. Each geode robot costs 2 ore and 18 obsidian.

    let parse (line : StringSplitEnumerator) : Day19Blueprint ResizeArray =
        use mutable enum = line.GetEnumerator ()
        let output = ResizeArray ()

        while enum.MoveNext () do
            if not (enum.Current.IsWhiteSpace ()) then
                let mutable lineEnum = StringSplitEnumerator.make' ' ' enum.Current
                StringSplitEnumerator.chomp "Blueprint" &lineEnum

                if not (lineEnum.MoveNext ()) then
                    failwith "expected number"

                let index = Int32.Parse (lineEnum.Current.TrimEnd ':')

                StringSplitEnumerator.chomp "Each" &lineEnum
                StringSplitEnumerator.chomp "ore" &lineEnum
                StringSplitEnumerator.chomp "robot" &lineEnum
                StringSplitEnumerator.chomp "costs" &lineEnum

                let oreCost = StringSplitEnumerator.consumeInt &lineEnum
                StringSplitEnumerator.chomp "ore." &lineEnum
                StringSplitEnumerator.chomp "Each" &lineEnum
                StringSplitEnumerator.chomp "clay" &lineEnum
                StringSplitEnumerator.chomp "robot" &lineEnum
                StringSplitEnumerator.chomp "costs" &lineEnum
                let clayCost = StringSplitEnumerator.consumeInt &lineEnum
                StringSplitEnumerator.chomp "ore." &lineEnum
                StringSplitEnumerator.chomp "Each" &lineEnum
                StringSplitEnumerator.chomp "obsidian" &lineEnum
                StringSplitEnumerator.chomp "robot" &lineEnum
                StringSplitEnumerator.chomp "costs" &lineEnum
                let obsidianOreCost = StringSplitEnumerator.consumeInt &lineEnum
                StringSplitEnumerator.chomp "ore" &lineEnum
                StringSplitEnumerator.chomp "and" &lineEnum
                let obsidianClayCost = StringSplitEnumerator.consumeInt &lineEnum
                StringSplitEnumerator.chomp "clay." &lineEnum
                StringSplitEnumerator.chomp "Each" &lineEnum
                StringSplitEnumerator.chomp "geode" &lineEnum
                StringSplitEnumerator.chomp "robot" &lineEnum
                StringSplitEnumerator.chomp "costs" &lineEnum
                let geodeOreCost = StringSplitEnumerator.consumeInt &lineEnum
                StringSplitEnumerator.chomp "ore" &lineEnum
                StringSplitEnumerator.chomp "and" &lineEnum
                let geodeObsidianCost = StringSplitEnumerator.consumeInt &lineEnum
                StringSplitEnumerator.chomp "obsidian." &lineEnum

                {
                    Ore = oreCost
                    Clay = clayCost
                    ObsidianOre = obsidianOreCost
                    ObsidianClay = obsidianClayCost
                    GeodeOre = geodeOreCost
                    GeodeObsidian = geodeObsidianCost
                    Index = index
                }
                |> output.Add

        output

    /// If we started only creating new geode creators right now, how many geodes could we
    /// mine by the end?
    let inline bestPossible (timeRemaining : int) = timeRemaining * (timeRemaining - 1) / 2

    let rec go
        (blueprint : Day19Blueprint)
        (bestSoFar : int)
        (timeRemaining : int)
        (oreCount : int)
        (clayCount : int)
        (obsidianCount : int)
        (geodeCount : int)
        (oreRobotCount : int)
        (clayRobotCount : int)
        (obsidianRobotCount : int)
        (geodeRobotCount : int)
        =
        if timeRemaining = 1 then
            max bestSoFar (geodeCount + geodeRobotCount)
        elif bestSoFar > geodeCount + geodeRobotCount * timeRemaining + bestPossible timeRemaining
        then
            bestSoFar
        else

        let newOreCount = oreCount + oreRobotCount
        let newClayCount = clayCount + clayRobotCount
        let newObsidianCount = obsidianCount + obsidianRobotCount
        let newGeodeCount = geodeCount + geodeRobotCount

        let mutable best = bestSoFar

        // If there are fewer than 10 turns remaining, it's always correct to create a geode
        // builder if we can. Indeed, ore is not the limiting factor, so if we instead chose
        // to make an obsidian builder, we'd lose 1 geode of opportunity cost, and then
        // would have to wait at least 7 turns for the new obsidian builder to produce enough
        // obsidian to make a replacement geode builder, then build the new geode builder on
        // the 8th turn, then it produces a replacement geode on the 9th turn, breaking even.
        if
            timeRemaining < 10
            && oreCount >= blueprint.GeodeOre
            && obsidianCount >= blueprint.GeodeObsidian
        then
            go
                blueprint
                best
                (timeRemaining - 1)
                (newOreCount - blueprint.GeodeOre)
                newClayCount
                (newObsidianCount - blueprint.GeodeObsidian)
                newGeodeCount
                oreRobotCount
                clayRobotCount
                obsidianRobotCount
                (geodeRobotCount + 1)

        else

        if oreCount >= blueprint.GeodeOre && obsidianCount >= blueprint.GeodeObsidian then
            best <-
                go
                    blueprint
                    best
                    (timeRemaining - 1)
                    (newOreCount - blueprint.GeodeOre)
                    newClayCount
                    (newObsidianCount - blueprint.GeodeObsidian)
                    newGeodeCount
                    oreRobotCount
                    clayRobotCount
                    obsidianRobotCount
                    (geodeRobotCount + 1)

        // Can we build an obsidian robot?
        // Note that if we have enough obsidian robots, then we're producing it as fast as we can consume it.
        if
            obsidianRobotCount < blueprint.GeodeObsidian
            && oreCount >= blueprint.ObsidianOre
            && clayCount >= blueprint.ObsidianClay
        then
            best <-
                go
                    blueprint
                    best
                    (timeRemaining - 1)
                    (newOreCount - blueprint.ObsidianOre)
                    (newClayCount - blueprint.ObsidianClay)
                    newObsidianCount
                    newGeodeCount
                    oreRobotCount
                    clayRobotCount
                    (obsidianRobotCount + 1)
                    geodeRobotCount

        // Can we build an ore robot?
        // Note that the biggest ore cost of anything is 4 ore, so if we have 4 ore-collecting robots then we
        // are already producing ore as fast as we can consume it.
        if oreRobotCount < 4 && oreCount >= blueprint.Ore then
            best <-
                go
                    blueprint
                    best
                    (timeRemaining - 1)
                    (newOreCount - blueprint.Ore)
                    newClayCount
                    newObsidianCount
                    newGeodeCount
                    (oreRobotCount + 1)
                    clayRobotCount
                    obsidianRobotCount
                    geodeRobotCount

        // Can we build a clay robot?
        if clayRobotCount < blueprint.ObsidianClay && oreCount >= blueprint.Clay then
            best <-
                go
                    blueprint
                    best
                    (timeRemaining - 1)
                    (newOreCount - blueprint.Clay)
                    newClayCount
                    newObsidianCount
                    newGeodeCount
                    oreRobotCount
                    (clayRobotCount + 1)
                    obsidianRobotCount
                    geodeRobotCount

        go
            blueprint
            best
            (timeRemaining - 1)
            newOreCount
            newClayCount
            newObsidianCount
            newGeodeCount
            oreRobotCount
            clayRobotCount
            obsidianRobotCount
            geodeRobotCount

    let doPart1 (blueprint : Day19Blueprint) : int =
        go blueprint 0 24 0 0 0 0 1 0 0 0

    let part1 (line : StringSplitEnumerator) : int =
        let blueprints = parse line

        seq { 0 .. blueprints.Count - 1 }
        |> PSeq.map (fun i -> blueprints.[i].Index * doPart1 blueprints.[i])
        |> Seq.sum

    let doPart2 (blueprint : Day19Blueprint) : int =
        go blueprint 0 32 0 0 0 0 1 0 0 0

    let part2 (line : StringSplitEnumerator) : int =
        let blueprints = parse line
        let blueprints = Array.init (min blueprints.Count 3) (fun i -> blueprints.[i])

        blueprints :> seq<_> |> PSeq.map doPart2 |> Seq.fold (*) 1
