namespace Fossa.Bridge.Models

open Fossa.Bridge.Models.ApiModels

[<RequireQualifiedAccess>]
type ClientResult<'T when 'T: not struct and 'T: not null> =
    | Success of 'T
    | Failure of ProblemDetailsModel

    member this.Match(onSuccess, onFailure) =
        match this with
        | Success value -> onSuccess value
        | Failure problem -> onFailure problem

    interface IClientResult with
        member this.IsSuccess =
            match this with
            | Success _ -> true
            | Failure _ -> false

        member this.GetClientProblem() =
            match this with
            | Success _ -> None
            | Failure problem -> Some problem
