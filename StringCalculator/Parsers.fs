module StringCalculator.Parsers

open System
open Utils

let parseWithoutBrackets (numbers: string) : Result<string array * string, CustomError> =
  if numbers[1] <> '\n' then
    "The input string should be of format \"//[delimiter]\n[numbers]\"."
    |> InvalidFormat
    |> Error
  else
    let delimiter = numbers[0].ToString()
    let numbers = numbers.Substring(2)

    Ok([| delimiter |], numbers)

let parseWithBrackets (numbers: string) : Result<string array * string, CustomError> =
  // opening bracket index is 0
  let closingBracketIndex = numbers.LastIndexOf(']')

  if
    closingBracketIndex < 1
    || numbers.Length <= closingBracketIndex + 2
    || numbers[closingBracketIndex + 1] <> '\n'
  then
    "The input string should be of format \"//[delimiter]\n[numbers]\"."
    |> InvalidFormat
    |> Error
  else
    let delimiters =
      numbers
        .Substring(1, closingBracketIndex - 1)
        .Split([| "][" |], StringSplitOptions.None)

    let numbers = numbers.Substring(closingBracketIndex + 2)

    Ok(delimiters, numbers)

let getParser (numbers: string) : Result<string -> Result<string array * string, CustomError>, CustomError> =
  if String.IsNullOrWhiteSpace numbers then
    "The input string should be of format \"//[delimiter]\n[numbers]\"."
    |> InvalidFormat
    |> Error
  else
    match numbers[0] with
    | '[' -> Ok parseWithBrackets
    | _ -> Ok parseWithoutBrackets
