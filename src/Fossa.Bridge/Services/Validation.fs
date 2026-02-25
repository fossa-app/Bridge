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

    let validateUserProfile (user: UserProfile) : string list =
        let mutable errors = []

        if String.IsNullOrWhiteSpace(user.Username) then
            errors <- "Username is required." :: errors

        if user.Username <> null && user.Username.Length < 3 then
            errors <- "Username must be at least 3 characters long." :: errors

        if not (isValidEmail user.Email) then
            errors <- "A valid Email is required." :: errors

        if String.IsNullOrWhiteSpace(user.FirstName) then
            errors <- "First Name is required." :: errors

        if String.IsNullOrWhiteSpace(user.LastName) then
            errors <- "Last Name is required." :: errors

        if user.DateOfBirth.HasValue then
            let dob = user.DateOfBirth.Value

            if dob > DateTimeOffset.UtcNow then
                errors <- "Date of Birth cannot be in the future." :: errors

        List.rev errors
