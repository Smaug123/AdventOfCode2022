namespace AdventOfCode2022.Test

open System.Collections.Generic
open AdventOfCode2022
open NUnit.Framework
open FsUnitTyped

[<TestFixture>]
module TestEnumerators =

    [<Test>]
    let ``MapEnumerator doesn't allocate`` () =
        let original = seq { 1..100 }

        let f (x : int) = byte x + 1uy

        let enum = MapEnumerator.make f (original.GetEnumerator ())
        //let foo = unbox<IEnumerator<byte>> (enum.GetEnumerator ())
        //let enum = MapEnumerator.make<byte, byte> id foo

        // Seq's GetEnumerator does allocate.
        let bytesBefore = System.GC.GetAllocatedBytesForCurrentThread ()
        let mutable expected = 2uy

        for output in enum do
            // shouldEqual allocates
            if output <> expected then
                failwithf "not equal, expected %i, actual %i" expected output

            expected <- expected + 1uy

        let bytesAfter = System.GC.GetAllocatedBytesForCurrentThread ()
        bytesAfter |> shouldEqual bytesBefore


    [<Test>]
    let ``ChooseEnumerator doesn't allocate`` () =
        let original = seq { 1..100 }

        let enum =
            ChooseEnumerator.make
                (fun x -> let x = byte x + 1uy in if x % 2uy = 0uy then ValueSome x else ValueNone)
                (original.GetEnumerator ())

        let bytesBefore = System.GC.GetAllocatedBytesForCurrentThread ()

        let mutable expected = 2uy

        for output in enum do
            // shouldEqual allocates
            if output <> expected then
                failwithf "not equal, expected %i, actual %i" expected output

            expected <- expected + 2uy

        let bytesAfter = System.GC.GetAllocatedBytesForCurrentThread ()
        bytesAfter |> shouldEqual bytesBefore
