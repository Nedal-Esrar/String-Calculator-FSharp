module StringCalculator.Utils

open System

type CustomError =
  | NullReference
  | InvalidFormat of string
  | NegativeFound of string

let parseInt (number: string) : Result<int, CustomError> =
  // Cases that are not handled in Int32.Parse, but are required in my case.
  // if the string has leading or trailing whitespace, it will be ignored by
  // Int32.Parse
  if
    String.IsNullOrWhiteSpace number
    || Char.IsWhiteSpace number[0]
    || Char.IsWhiteSpace number[number.Length - 1]
  then
    $"The input string '{number}' was not in a correct format."
    |> InvalidFormat
    |> Error
  else
    try
      number |> Int32.Parse |> Ok
    with :? FormatException as ex ->
      ex.Message |> InvalidFormat |> Error
