namespace StringCalculator.Exceptions

type NegativeFoundException(message: string) =
  inherit System.Exception(message)

  override this.Message = message
