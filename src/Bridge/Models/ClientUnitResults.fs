namespace Fossa.Bridge.Models

open Fossa.Bridge.Models.ApiModels

[<RequireQualifiedAccess>]
type ClientUnitResult =
    | Success
    | Failure of ProblemDetailsModel

    member this.Match(onSuccess, onFailure) =
        match this with
        | Success -> onSuccess ()
        | Failure problem -> onFailure problem

    interface IClientResult with
        member this.IsSuccess =
            match this with
            | Success -> true
            | Failure _ -> false

        member this.GetClientProblem() =
            match this with
            | Success -> None
            | Failure problem -> Some problem
