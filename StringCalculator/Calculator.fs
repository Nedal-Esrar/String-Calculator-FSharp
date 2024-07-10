module StringCalculator.Calculator

open System
open StringCalculator.Utils

let Add (numbers : string) : int =
  if numbers = null then
    raise (NullReferenceException())

  if String.IsNullOrEmpty numbers then
    0
  else
    let defaultDelimiters = [| ',' |]

    let splitNumbers = numbers.Split defaultDelimiters

    match splitNumbers.Length with
    | 1 -> ParseInt splitNumbers[0]
    | 2 -> ParseInt splitNumbers[0] + ParseInt splitNumbers[1]
    | _ -> raise (ArgumentException "The input string has more than 2 parts.")