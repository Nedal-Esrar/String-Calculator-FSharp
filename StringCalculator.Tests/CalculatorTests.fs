module StringCalculator.Tests.CalculatorTests

open Xunit
open FsUnit.Xunit
open StringCalculator.Calculator

[<Fact>]
let ``Add should throw NullReferenceException when null is passed`` () =
  (fun () -> add null |> ignore)
  |> should throw typeof<System.NullReferenceException>

[<Theory>]
[<InlineData("", 0)>]
[<InlineData("1", 1)>]
[<InlineData("2", 2)>]
[<InlineData("1,2", 3)>]
[<InlineData("1,3", 4)>]
[<InlineData("1,2,3,4", 10)>]
[<InlineData("1,10,20,4", 35)>]
[<InlineData("1\n10", 11)>]
[<InlineData("1\n10,20\n4", 35)>]
let ``Add should return the expected result when a valid string is passed`` (numbers: string, expected: int) =
  add numbers |> should equal expected

[<Theory>]
[<InlineData("1,")>]
[<InlineData("1    ")>]
[<InlineData("2,  ")>]
[<InlineData("1,   2\t")>]
[<InlineData("1b,2")>]
let ``Add should throw FormatException when an invalid string is passed`` (numbers: string) =
  (fun () -> add numbers |> ignore) |> should throw typeof<System.FormatException>

[<Theory>]
[<InlineData("//$\n1", 1)>]
[<InlineData("//%\n2%2", 4)>]
let ``Add should return the expected results when using custom delimiters`` (numbers: string, expected: int) =
  add numbers |> should equal expected

[<Theory>]
[<InlineData("//$\n1\n2")>]
[<InlineData("//%\n2,2")>]
let ``Add should ignore default delimiters when custom is used (throw FormatException)`` (numbers: string) =
  (fun () -> add numbers |> ignore) |> should throw typeof<System.FormatException>

[<Theory>]
[<InlineData("//$1\n2")>]
let ``Add with invalid format of custom delimiter should throw FormatException`` (numbers: string) =
  let ex = Assert.Throws<System.FormatException>(fun () -> add numbers |> ignore)
  
  ex.Message |> should equal "The input string should be of format \"//[delimiter]\n[numbers]\"."