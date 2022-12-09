namespace AdventOfCode2022

open System
open System.Collections.Generic
open System.Runtime.CompilerServices

type EfficientString = System.ReadOnlySpan<char>

[<RequireQualifiedAccess>]
module EfficientString =

    let inline isEmpty (s : EfficientString) : bool = s.IsEmpty


    let inline ofString (s : string) : EfficientString = s.AsSpan ()

    let inline toString (s : EfficientString) : string = s.ToString ()

    let inline trimStart (s : EfficientString) : EfficientString = s.TrimStart ()

    let inline slice (start : int) (length : int) (s : EfficientString) : EfficientString = s.Slice (start, length)

    let inline equals (a : string) (other : EfficientString) : bool =
        MemoryExtensions.Equals (other, a.AsSpan (), StringComparison.Ordinal)

    /// Mutates the input to drop up to the first instance of the input char,
    /// and returns what was dropped.
    /// If the char is not present, deletes the input.
    let takeUntil<'a> (c : char) (s : EfficientString byref) : EfficientString =
        let first = s.IndexOf c

        if first < 0 then
            let toRet = s
            s <- EfficientString.Empty
            toRet
        else
            let toRet = slice 0 first s
            s <- slice (first + 1) (s.Length - first - 1) s
            toRet

[<Struct>]
[<IsByRefLike>]
type StringSplitEnumerator =
    internal
        {
            Original : EfficientString
            mutable Remaining : EfficientString
            mutable InternalCurrent : EfficientString
            SplitOn : char
        }

    interface IDisposable with
        member this.Dispose () = ()

    member this.Current : EfficientString = this.InternalCurrent

    member this.MoveNext () =
        if this.Remaining.Length = 0 then
            false
        else
            this.InternalCurrent <- EfficientString.takeUntil this.SplitOn &this.Remaining
            true

    member this.GetEnumerator () = this

[<RequireQualifiedAccess>]
module StringSplitEnumerator =

    let make (splitChar : char) (s : string) : StringSplitEnumerator =
        {
            Original = EfficientString.ofString s
            Remaining = EfficientString.ofString s
            InternalCurrent = EfficientString.Empty
            SplitOn = splitChar
        }

    let make' (splitChar : char) (s : ReadOnlySpan<char>) : StringSplitEnumerator =
        {
            Original = s
            Remaining = s
            InternalCurrent = EfficientString.Empty
            SplitOn = splitChar
        }

    let chomp (s : string) (e : byref<StringSplitEnumerator>) =
        if not (e.MoveNext ()) || not (EfficientString.equals s e.Current) then
            failwithf "expected '%s'" s

    let consumeInt (e : byref<StringSplitEnumerator>) : int =
        if not (e.MoveNext ()) then
            failwith "expected an int, got nothing"

        Int32.Parse e.Current

[<Struct>]
[<IsByRefLike>]
type MapEnumerator<'a, 'b> =
    internal
        {
            F : 'a -> 'b
            Seq : 'a IEnumerator
            mutable CurrentInternal : 'b
        }

    interface IDisposable with
        member this.Dispose () = this.Seq.Dispose ()

    interface IEnumerator<'b> with
        member this.Current = this.CurrentInternal
        member this.get_Current () = box this.CurrentInternal
        member this.Reset () = this.Seq.Reset ()

        member this.MoveNext () =
            if this.Seq.MoveNext () then
                this.CurrentInternal <- this.F this.Seq.Current
                true
            else
                false

    member this.Current = this.CurrentInternal

    member this.MoveNext () =
        if this.Seq.MoveNext () then
            this.CurrentInternal <- this.F this.Seq.Current
            true
        else
            false

    member this.GetEnumerator () = this

[<RequireQualifiedAccess>]
module MapEnumerator =
    let make<'a, 'b> (f : 'a -> 'b) (s : 'a IEnumerator) =
        {
            F = f
            Seq = s
            CurrentInternal = Unchecked.defaultof<_>
        }

[<Struct>]
[<IsByRefLike>]
type ChooseEnumerator<'a, 'b> =
    internal
        {
            F : 'a -> 'b ValueOption
            Seq : 'a IEnumerator
            mutable CurrentOutput : 'b
        }

    interface IDisposable with
        member this.Dispose () = this.Seq.Dispose ()

    member this.Current : 'b = this.CurrentOutput

    member this.Reset () =
        this.Seq.Reset ()
        this.CurrentOutput <- Unchecked.defaultof<_>

    member this.MoveNext () : bool =
        let mutable keepGoing = true
        let mutable toRet = true

        while keepGoing do
            if this.Seq.MoveNext () then
                match this.F this.Seq.Current with
                | ValueNone -> ()
                | ValueSome v ->
                    this.CurrentOutput <- v
                    keepGoing <- false
            else
                keepGoing <- false
                toRet <- false

        toRet

    member this.GetEnumerator () = this

[<RequireQualifiedAccess>]
module ChooseEnumerator =
    let make<'a, 'b> (f : 'a -> 'b ValueOption) (s : 'a IEnumerator) : ChooseEnumerator<'a, 'b> =
        {
            F = f
            Seq = s
            CurrentOutput = Unchecked.defaultof<_>
        }

[<Struct>]
[<IsByRefLike>]
type RangeEnumerator<'T
    when 'T : (static member (+) : 'T -> 'T -> 'T) and 'T : (static member One : 'T) and 'T : equality> =
    {
        /// Do not mutate this!
        mutable Started : bool
        /// Do not mutate this!
        mutable CurrentOutput : 'T
        Start : 'T
        End : 'T
    }

    member inline this.Current : 'T = this.CurrentOutput

    member inline this.MoveNext () =
        if not this.Started then
            this.Started <- true
            true
        elif this.End = this.CurrentOutput then
            false
        else
            this.CurrentOutput <- this.CurrentOutput + LanguagePrimitives.GenericOne
            true

    member inline this.GetEnumerator () = this

[<RequireQualifiedAccess>]
module RangeEnumerator =

    let inline make<'T
        when 'T : equality and 'T : (static member (+) : 'T -> 'T -> 'T) and 'T : (static member One : 'T)>
        (start : 'T)
        (endAtInclusive : 'T)
        : RangeEnumerator<'T>
        =
        {
            Started = false
            Start = start
            End = endAtInclusive
            CurrentOutput = Unchecked.defaultof<_>
        }
