﻿namespace AdventOfCode2022.App

open System.Diagnostics
open System.IO
open System.Reflection
open AdventOfCode2022
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Running

type Benchmarks () =
    [<GlobalSetup>]
    member _.Setup () = Run.shouldWrite <- false

    [<Params(false, true)>]
    member val IsPartOne = false with get, set

    [<Params(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)>]
    member val Day = 0 with get, set

    [<Benchmark>]
    member this.Benchmark () : unit =
        Run.allRuns.[this.Day - 1] this.IsPartOne (Inputs.day this.Day)

    [<GlobalCleanup>]
    member _.Cleanup () = Run.shouldWrite <- true

module Program =

    [<EntryPoint>]
    let main args =
        match args with
        | [| "bench" |] ->
            let config =
                ManualConfig
                    .Create(DefaultConfig.Instance)
                    .WithOptions ConfigOptions.DisableOptimizationsValidator

            let summary = BenchmarkRunner.Run<Benchmarks> config
            0
        | _ ->

        let time = Stopwatch.StartNew ()
        time.Restart ()

        for day in 1 .. Run.allRuns.Length do
            Run.allRuns.[day - 1] false (Inputs.day day)
            Run.allRuns.[day - 1] true (Inputs.day day)

        time.Stop ()
        printfn $"Took %i{time.ElapsedMilliseconds}ms"
        0
