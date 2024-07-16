module StringCalculator.Calculator

open System
open Result
open StringCalculator.Utils
open StringCalculator.Parsers
open FsToolkit.ErrorHandling

let private parseDelimiters (numbers: string) : Result<string array * string, CustomError> =
  if numbers.StartsWith("//") then
    let trimmedNumbers = numbers.Substring(2)

    trimmedNumbers
    |> getParser
    |> bind (fun parse -> parse trimmedNumbers)
  else
    Ok([| ","; "\n" |], numbers)

let private extractNumbers (numbers: string) : Result<int array, CustomError> =
  numbers
  |> parseDelimiters
  |> map (fun (delimiters, numbers) -> numbers.Split(delimiters, StringSplitOptions.None))
  |> bind (Array.toList >> List.map parseInt >> List.sequenceResultM)
  |> map List.toArray

let private checkForNegative (numbers: int array) : Result<int array, CustomError> =
  let negatives = numbers |> Array.filter (fun n -> n < 0)

  match negatives.Length with
  | 0 -> Ok numbers
  | _ ->
    negatives
    |> Array.map string
    |> String.concat ", "
    |> fun negatives -> $"negatives not allowed - {negatives}"
    |> NegativeFound
    |> Error

let add (numbers: string) : Result<int, CustomError> =
  match numbers with
  | null -> Error NullReference
  | "" -> Ok 0
  | _ ->
    numbers
    |> extractNumbers
    |> bind checkForNegative
    |> map (Array.filter (fun n -> n <= 1000))
    |> map Array.sum
