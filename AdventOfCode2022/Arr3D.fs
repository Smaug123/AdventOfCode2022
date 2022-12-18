namespace AdventOfCode2022

#if DEBUG
#else
#nowarn "9"
#endif

open Microsoft.FSharp.NativeInterop

[<Struct>]
#if DEBUG
type Arr3D<'a> =
    {
        Elements : 'a array
        Width : int
        WidthTimesHeight : int
    }

    member this.Depth = this.Elements.Length / this.WidthTimesHeight
#else
type Arr3D<'a when 'a : unmanaged> =
    {
        Elements : nativeptr<'a>
        Length : int
        Width : int
        WidthTimesHeight : int
    }

    member this.Depth = this.Length / this.WidthTimesHeight
#endif

[<RequireQualifiedAccess>]
module Arr3D =

    /// It's faster to iterate forward over the first argument, `x`, and then the
    /// second argument, `y`.
    let inline get (arr : Arr3D<'a>) (x : int) (y : int) (z : int) : 'a =
#if DEBUG
        arr.Elements.[z * arr.WidthTimesHeight + y * arr.Width + x]
#else
        NativePtr.get arr.Elements (z * arr.WidthTimesHeight + y * arr.Width + x)
#endif

    let inline set (arr : Arr3D<'a>) (x : int) (y : int) (z : int) (newVal : 'a) : unit =
#if DEBUG
        arr.Elements.[z * arr.WidthTimesHeight + y * arr.Width + x] <- newVal
#else
        NativePtr.write (NativePtr.add arr.Elements (z * arr.WidthTimesHeight + y * arr.Width + x)) newVal
#endif

#if DEBUG
    let create (width : int) (height : int) (depth : int) (value : 'a) : Arr3D<'a> =
        let arr = Array.create (width * height * depth) value

        {
            Width = width
            WidthTimesHeight = width * height
            Elements = arr
        }
#else
    /// The input array must be at least of size width * height * depth
    let create (arr : nativeptr<'a>) (width : int) (height : int) (depth : int) (value : 'a) : Arr3D<'a> =
        {
            Width = width
            Elements = arr
            Length = width * height * depth
            WidthTimesHeight = width * height
        }
#endif

    [<RequiresExplicitTypeArguments>]
#if DEBUG
    let zeroCreate<'a when 'a : unmanaged> (width : int) (height : int) (depth : int) : Arr3D<'a> =
        {
            Elements = Array.zeroCreate (width * height * depth)
            Width = width
            WidthTimesHeight = width * height
        }
#else
    let zeroCreate<'a when 'a : unmanaged>
        (elts : nativeptr<'a>)
        (width : int)
        (height : int)
        (depth : int)
        : Arr3D<'a>
        =
        {
            Elements = elts
            Width = width
            WidthTimesHeight = width * height
            Length = width * height * depth
        }
#endif

    let inline clear (a : Arr3D<'a>) : unit =
#if DEBUG
        System.Array.Clear a.Elements
#else
        NativePtr.initBlock a.Elements 0uy (uint32 sizeof<'a> * uint32 a.Length)
#endif
