namespace AdventOfCode2022.App

open System.IO
open System.Reflection

[<RequireQualifiedAccess>]
module Assembly =
    type private Dummy = class end

    let readResource (name : string) : string =
        let asm = Assembly.GetAssembly typeof<Dummy>

        use stream = asm.GetManifestResourceStream $"AdventOfCode2022.App.%s{name}"

        use reader = new StreamReader (stream)
        reader.ReadToEnd ()
