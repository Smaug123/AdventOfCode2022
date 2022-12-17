﻿namespace AdventOfCode2022.App

open System.Diagnostics
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Running

type Benchmark1To5 () =
    [<GlobalSetup>]
    member _.Setup () = Run.shouldWrite <- false

    [<Params(1, 2, 3, 4, 5)>]
    member val Day = 0 with get, set

    [<Params(false, true)>]
    member val IsPartOne = false with get, set

    [<Benchmark>]
    member this.Benchmark () : unit =
        Run.allRuns.[this.Day - 1] (not this.IsPartOne) (Inputs.day this.Day)

    [<GlobalCleanup>]
    member _.Cleanup () = Run.shouldWrite <- true


type Benchmark6To10 () =
    [<GlobalSetup>]
    member _.Setup () = Run.shouldWrite <- false

    [<Params(6, 7, 8, 9, 10)>]
    member val Day = 0 with get, set

    [<Params(false, true)>]
    member val IsPartOne = false with get, set

    [<Benchmark>]
    member this.Benchmark () : unit =
        Run.allRuns.[this.Day - 1] (not this.IsPartOne) (Inputs.day this.Day)

    [<GlobalCleanup>]
    member _.Cleanup () = Run.shouldWrite <- true

type Benchmark11To15 () =
    [<GlobalSetup>]
    member _.Setup () = Run.shouldWrite <- false

    [<Params(11, 12, 13, 14, 15)>]
    member val Day = 0 with get, set

    [<Params(false, true)>]
    member val IsPartOne = false with get, set

    [<Benchmark>]
    member this.Benchmark () : unit =
        Run.allRuns.[this.Day - 1] (not this.IsPartOne) (Inputs.day this.Day)

    [<GlobalCleanup>]
    member _.Cleanup () = Run.shouldWrite <- true

type Benchmark16To20 () =
    [<GlobalSetup>]
    member _.Setup () = Run.shouldWrite <- false

    [<Params(16)>]
    member val Day = 0 with get, set

    [<Params(false, true)>]
    member val IsPartOne = false with get, set

    [<Benchmark>]
    member this.Benchmark () : unit =
        Run.allRuns.[this.Day - 1] (not this.IsPartOne) (Inputs.day this.Day)

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

            let _summary = BenchmarkRunner.Run<Benchmark1To5> config
            let _summary = BenchmarkRunner.Run<Benchmark6To10> config
            let _summary = BenchmarkRunner.Run<Benchmark11To15> config
            let _summary = BenchmarkRunner.Run<Benchmark16To20> config
            0
        | _ ->

        let time = Stopwatch.StartNew ()
        time.Restart ()

        //for day in 1 .. Run.allRuns.Length do
        //    Run.allRuns.[day - 1] false (Inputs.day day)
        //    Run.allRuns.[day - 1] true (Inputs.day day)
        Run.allRuns.[16 - 1] false (Inputs.day 16)

        time.Stop ()
        printfn $"Took %i{time.ElapsedMilliseconds}ms"
        0
