module StringCalculator.Calculator

open System
open StringCalculator.Utils
open StringCalculator.Exceptions
open StringCalculator.Parsers

let private parseDelimiters (numbers: string) : string[] * string =
  if numbers.StartsWith("//") then
    let trimmedNumbers = numbers.Substring(2)
    
    let parse = getParser trimmedNumbers
    
    parse trimmedNumbers
  else
    [| ","; "\n" |], numbers

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

    numbers.Split(delimiters, StringSplitOptions.None)
    |> Array.map parseInt
    |> checkForNegative
    |> Array.filter (fun n -> n <= 1000)
    |> Array.sum
