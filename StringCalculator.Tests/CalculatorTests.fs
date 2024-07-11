module StringCalculator.Tests.CalculatorTests

open Xunit
open FsUnit.Xunit
open StringCalculator.Calculator

[<Fact>]
let ``Add should throw NullReferenceException when null is passed`` () =
   (fun () -> Add null |> ignore) |> should throw typeof<System.NullReferenceException>

[<Theory>]
[<InlineData("", 0)>]
[<InlineData("1", 1)>]
[<InlineData("2", 2)>]
[<InlineData("1,2", 3)>]
[<InlineData("1,3", 4)>]
let ``Add should return the expected result when a valid string is passed`` (numbers : string, expected : int) =
   Add numbers |> should equal expected
   
[<Theory>]
[<InlineData("1,")>]
[<InlineData("1    ")>]
[<InlineData("2,  ")>]
[<InlineData("1,   2\t")>]
[<InlineData("1b,2")>]
let ``Add should throw FormatException when an invalid string is passed`` (numbers : string) =
   (fun () -> Add numbers |> ignore) |> should throw typeof<System.FormatException>
   
[<Theory>]
[<InlineData("1,2,3,4")>]
let ``Add should throw ArgumentException when more than 2 numbers exists`` (numbers : string) =
   (fun () -> Add numbers |> ignore) |> should throw typeof<System.ArgumentException>