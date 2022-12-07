namespace AdventOfCode2022

open System.Collections.Generic
open System

type Day7Entry =
    | Directory of string
    | File of string * int

type Day7Command =
    | Ls of Day7Entry IReadOnlyList
    | Cd of destination : string

[<RequireQualifiedAccess>]
module Day7 =

    let parse (lines : IEnumerator<string>) =
        seq {
            if not (lines.MoveNext ()) then
                failwith "empty enumerator"

            let mutable isDone = false

            while not isDone do
                let line = lines.Current.TrimEnd ()

                if String.IsNullOrEmpty line then
                    isDone <- true
                else
                    match line.[0] with
                    | '$' ->
                        if line.[1] <> ' ' then
                            failwith "expected space"

                        match line.[2..3] with
                        | "cd" ->
                            yield Day7Command.Cd line.[5..]

                            if not (lines.MoveNext ()) then
                                failwith "expected an ls after a cd"
                        | "ls" ->
                            let outputs = ResizeArray ()

                            while lines.MoveNext ()
                                  && not (String.IsNullOrEmpty lines.Current)
                                  && lines.Current.[0] <> '$' do
                                let current = lines.Current.TrimEnd ()

                                match current.[0] with
                                | 'd' ->
                                    if current.[0..3] <> "dir " then
                                        failwith "expected directory"

                                    outputs.Add (Day7Entry.Directory current.[4..])
                                | _ ->
                                    match current.Split ' ' with
                                    | [| size ; file |] -> outputs.Add (Day7Entry.File (file, int size))
                                    | _ -> failwith "unexpected spaces"

                            yield Day7Command.Ls outputs
                        | _ -> failwith "unrecognised command"
                    | _ -> failwith "should have been a command"
        }

    type Path = string list

    type private Directory =
        {
            Path : Path
            SubDirs : string list
            Files : Map<string, int>
        }

    let private tree (commands : Day7Command seq) : Map<Path, Directory> =
        let dirs, _ =
            (([], []), commands)
            ||> Seq.fold (fun (dirs, parentPath) command ->
                match command with
                | Day7Command.Ls outputs ->
                    let subDirs, files =
                        (([], Map.empty), outputs)
                        ||> Seq.fold (fun (subDirs, files) output ->
                            match output with
                            | Day7Entry.Directory d -> d :: subDirs, files
                            | Day7Entry.File (name, size) -> subDirs, (Map.add name size files)
                        )

                    let current =
                        {
                            Path = parentPath
                            SubDirs = subDirs
                            Files = files
                        }

                    let dirs = current :: dirs

                    dirs, parentPath

                | Day7Command.Cd dir ->
                    let newPath =
                        match dir, parentPath with
                        | "..", _ :: rest -> rest
                        | "..", [] -> failwith "can't cd above root"
                        | _ -> dir :: parentPath

                    dirs, newPath
            )

        dirs |> Seq.map (fun dir -> dir.Path, dir) |> Map.ofSeq

    let rec private cata<'ret>
        (atFile : string -> int -> 'ret)
        (atDir : Path -> 'ret list -> 'ret)
        (startAt : Path)
        (dirs : Map<Path, Directory>)
        =
        let dir =
            match dirs.TryGetValue startAt with
            | false, _ -> failwithf "could not find key: %+A" startAt
            | true, v -> v

        let fileResults =
            dir.Files
            |> Seq.map (fun (KeyValue (name, size)) -> atFile name size)
            |> List.ofSeq

        let dirResults =
            dir.SubDirs
            |> List.map (fun subDir -> cata atFile atDir (subDir :: startAt) dirs)

        atDir startAt (fileResults @ dirResults)

    let private totalSize =
        cata (fun _ i -> i) (fun _ results -> List.sum results) [ "/" ]

    let mergeTwo (m1 : Map<_, _>) (m2 : Map<_, _>) =
        (m1, m2) ||> Map.fold (fun m k v -> Map.add k v m)

    let mergeMaps (maps : Map<_, _> seq) =
        (Map.empty, maps) ||> Seq.fold (fun result newMap -> mergeTwo result newMap)

    let private cumulativeDirectorySizes =
        let doFile (_name : string) (size : int) : int * Map<Path, int> = size, Map.empty

        let doDir (path : Path) (subResults : (int * Map<Path, int>) list) : int * Map<Path, int> =
            let sum = subResults |> Seq.map fst |> Seq.sum
            let map = subResults |> Seq.map snd |> mergeMaps
            sum, Map.add path sum map

        cata doFile doDir [ "/" ]

    let part1 (lines : string seq) : int =
        let dirs =
            seq {
                use enum = lines.GetEnumerator ()
                yield! parse enum
            }
            |> tree

        let _totalSize, results = cumulativeDirectorySizes dirs

        results
        |> Seq.choose (fun (KeyValue (_, size)) -> if size <= 100000 then Some size else None)
        |> Seq.sum

    let part2 (lines : string seq) : int =
        let dirs =
            seq {
                use enum = lines.GetEnumerator ()
                yield! parse enum
            }
            |> tree

        let totalSize, results = cumulativeDirectorySizes dirs

        let unused = 70000000 - totalSize
        let required = 30000000 - unused

        results
        |> Seq.choose (fun (KeyValue (path, size)) -> if size >= required then Some size else None)
        |> Seq.min
