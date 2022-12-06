namespace AdventOfCode2022.App

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
        do
            let day1 = readResource "Day1.txt"
            let lines = StringSplitEnumerator.make '\n' day1
            printfn "%i" (Day1.part1 lines)
            printfn "%i" (Day1.part2 lines)

        0
