module StringCalculator.Calculator

open System
open StringCalculator.Utils

let Add (numbers : string) : int =
  if numbers = null then
    raise (NullReferenceException())

  if String.IsNullOrEmpty numbers then
    0
  else
    let defaultDelimiters = [| ','; '\n' |]

    numbers.Split defaultDelimiters
    |> Array.map ParseInt
    |> Array.sum
    