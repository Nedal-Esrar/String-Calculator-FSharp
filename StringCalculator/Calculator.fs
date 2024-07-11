module StringCalculator.Calculator

open System
open StringCalculator.Utils
open StringCalculator.Exceptions

let private parseDelimiters (numbers: string) : char[] * string =
  if numbers.StartsWith("//") then
    if numbers[3] <> '\n' then
      raise (FormatException "The input string should be of format \"//[delimiter]\n[numbers]\".")

    let delimiter = numbers[2]
    let numbers = numbers.Substring(4)

    [| delimiter |], numbers
  else
    [| ','; '\n' |], numbers

let private checkForNegative (numbers: int[]) : int[] =
  let negatives = numbers |> Array.filter (fun n -> n < 0)

  match negatives with
  | [||] -> numbers
  | _ ->
    let concatenatedNegatives = negatives |> Array.map string |> String.concat ", "
    raise (NegativeFoundException $"negatives not allowed - {concatenatedNegatives}")

let add (numbers: string) : int =
  match numbers with
  | null -> raise (NullReferenceException())
  | "" -> 0
  | _ ->
    let delimiters, numbers = parseDelimiters numbers

    numbers.Split delimiters
    |> Array.map parseInt
    |> checkForNegative
    |> Array.filter (fun n -> n <= 1000)
    |> Array.sum
