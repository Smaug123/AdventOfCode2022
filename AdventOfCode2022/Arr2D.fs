namespace AdventOfCode2022

type Arr2D<'a> =
    {
        Elements : 'a[]
        Width : int
        Height : int
    }

[<RequireQualifiedAccess>]
module Arr2D =

    /// It's faster to iterate forward over the first argument, `x`.
    let inline get (arr : Arr2D<'a>) (x : int) (y : int) : 'a = arr.Elements.[y * arr.Width + x]

    let inline set (arr : Arr2D<'a>) (x : int) (y : int) (newVal : 'a) : unit =
        arr.Elements.[y * arr.Width + x] <- newVal

    let create<'a> (width : int) (height : int) (value : 'a) : Arr2D<'a> =
        {
            Elements = Array.create (width * height) value
            Width = width
            Height = height
        }

    [<RequiresExplicitTypeArguments>]
    let zeroCreate<'a> (width : int) (height : int) : Arr2D<'a> =
        {
            Elements = Array.zeroCreate (width * height)
            Width = width
            Height = height
        }

    /// The closure is given x and then y.
    let inline init<'a> (width : int) (height : int) (f : int -> int -> 'a) : Arr2D<'a> =
        let result = zeroCreate<'a> width height

        for y in 0 .. height - 1 do
            for x in 0 .. width - 1 do
                set result x y (f x y)

        result
