namespace Fossa.Bridge.Models.ApiModels

[<RequireQualifiedAccess>]
type ClientResult<'T when 'T: not struct and 'T: not null> =
    | Success of 'T
    | Failure of ProblemDetailsModel

    member this.Match(onSuccess, onFailure) =
        match this with
        | Success value -> onSuccess value
        | Failure problem -> onFailure problem
