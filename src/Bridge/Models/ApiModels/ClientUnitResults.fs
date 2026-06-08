namespace Fossa.Bridge.Models.ApiModels

[<RequireQualifiedAccess>]
type ClientUnitResult =
    | Success
    | Failure of ProblemDetailsModel

    member this.Match(onSuccess, onFailure) =
        match this with
        | Success -> onSuccess ()
        | Failure problem -> onFailure problem
