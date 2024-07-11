module StringCalculator.Parsers

open System

let parseWithoutBrackets (numbers: string) : string[] * string =
  if numbers[1] <> '\n' then
    raise (FormatException "The input string should be of format \"//[delimiter]\n[numbers]\".")

  let delimiter = numbers[0]
  let numbers = numbers.Substring(2)

  [| delimiter.ToString() |], numbers

let parseWithBrackets (numbers: string) : string[] * string =
  // opening bracket index is 0
  let closingBracketIndex = numbers.LastIndexOf(']')

  if
    closingBracketIndex < 1
    || numbers.Length <= closingBracketIndex + 2
    || numbers[closingBracketIndex + 1] <> '\n'
  then
    raise (FormatException "The input string should be of format \"//[delimiter]\n[numbers]\".")

  let delimiters =
    numbers
      .Substring(1, closingBracketIndex - 1)
      .Split([| "][" |], StringSplitOptions.None)

  let numbers = numbers.Substring(closingBracketIndex + 2)

  delimiters, numbers

let getParser (numbers: string) : string -> string[] * string =
  if String.IsNullOrWhiteSpace(numbers) then
    raise (FormatException "The input string should be of format \"//[delimiter]\n[numbers]\".")

  match numbers[0] with
  | '[' -> parseWithBrackets
  | _ -> parseWithoutBrackets
