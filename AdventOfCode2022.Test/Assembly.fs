namespace AdventOfCode2022.Test

open System.IO
open System.Reflection

[<RequireQualifiedAccess>]
module Assembly =

    type private Dummy = class end

    let readResource (name : string) : string =
        let asm = Assembly.GetAssembly typeof<Dummy>

        use stream =
            asm.GetManifestResourceStream (sprintf "AdventOfCode2022.Test.Inputs.%s" name)

        use reader = new StreamReader (stream)
        reader.ReadToEnd ()
