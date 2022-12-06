namespace AdventOfCode2022.App

open System.Diagnostics
open System.IO
open System.Reflection
open AdventOfCode2022

module Program =
    type Dummy =
        class
        end

    let readResource (name : string) : string =
        let asm = Assembly.GetAssembly typeof<Dummy>

        use stream = asm.GetManifestResourceStream (sprintf "AdventOfCode2022.App.%s" name)

        use reader = new StreamReader (stream)
        reader.ReadToEnd ()

    [<EntryPoint>]
    let main _ =
        let days = Array.init 6 (fun day -> readResource $"Day%i{day + 1}.txt")

        let inline day (i : int) = days.[i - 1]

        let time = Stopwatch.StartNew ()
        time.Restart ()

        do
            let lines = StringSplitEnumerator.make '\n' (day 1)
            printfn "%i" (Day1.part1 lines)
            printfn "%i" (Day1.part2 lines)

        do
            let lines = StringSplitEnumerator.make '\n' (day 2)
            printfn "%i" (Day2.part1 lines)
            printfn "%i" (Day2.part2 lines)

        do
            let day3 = day 3
            let lines = StringSplitEnumerator.make '\n' day3
            printfn "%i" (Day3Efficient.part1 lines)
            printfn "%i" (Day3Efficient.part2 (day3.Split '\n'))

        do
            let lines = StringSplitEnumerator.make '\n' (day 4)
            printfn "%i" (Day4.part1 lines)
            printfn "%i" (Day4.part2 lines)

        do
            let lines = StringSplitEnumerator.make '\n' (day 5)
            printfn "%s" (Day5.part1 lines)
            printfn "%s" (Day5.part2 lines)

        do
            let day6 = day 6
            printfn "%i" (Day6.part1 day6)
            printfn "%i" (Day6.part2 day6)

        time.Stop ()
        printfn $"Took %i{time.ElapsedMilliseconds}ms"
        0
