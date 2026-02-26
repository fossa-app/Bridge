namespace Fossa.Bridge.Services

open Fossa.Bridge.Models
open System
open System.Text.RegularExpressions

module Validation =

    let private isValidEmail (email: string) =
        if String.IsNullOrWhiteSpace(email) then
            false
        else
            try
                Regex.IsMatch(
                    email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250.0)
                )
            with :? RegexMatchTimeoutException ->
                false
