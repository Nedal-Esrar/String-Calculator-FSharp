module StringCalculator.Utils

open System

let parseInt (number: string) : int =
  // Cases that are not handled in Int32.Parse, but are required in my case.
  // if the string has leading or trailing whitespace, it will be ignored by
  // Int32.Parse
  if
    String.IsNullOrWhiteSpace number
    || Char.IsWhiteSpace number[0]
    || Char.IsWhiteSpace number[number.Length - 1]
  then
    raise (FormatException $"The input string '{number}' was not in a correct format.")

  Int32.Parse number
