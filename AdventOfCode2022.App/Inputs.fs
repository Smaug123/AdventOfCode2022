namespace AdventOfCode2022.App

[<RequireQualifiedAccess>]
module Inputs =
    let days = Array.init 13 (fun day -> Assembly.readResource $"Day%i{day + 1}.txt")
    let inline day (i : int) = days.[i - 1]
