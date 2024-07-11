module StringCalculator.Calculator

open System
open StringCalculator.Utils

let private ParseDelimiters (numbers: string) : char[] * string =
  if numbers.StartsWith("//") then
    let delimiter = numbers[2]
    let numbers = numbers.Substring(4)

    [| delimiter |], numbers
  else
    [| ','; '\n' |], numbers

let Add (numbers: string) : int =
  match numbers with
  | null -> raise (NullReferenceException())
  | "" -> 0
  | _ ->
    let delimiters, numbers = ParseDelimiters numbers
    numbers.Split delimiters
    |> Array.map ParseInt
    |> Array.sum
