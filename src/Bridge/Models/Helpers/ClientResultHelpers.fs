namespace Fossa.Bridge.Models.Helpers

open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

module ClientResultHelpers =
    let success<'T when 'T: not struct and 'T: not null> (value: 'T) : ClientResult<'T> = ClientResult.Success value

    let problem<'T when 'T: not struct and 'T: not null> (problem: ProblemDetailsModel) : ClientResult<'T> =
        ClientResult.Failure problem

    let mapClientResult<'T, 'U when 'T: not struct and 'T: not null and 'U: not struct and 'U: not null>
        (result: ClientResult<'T>)
        (mapSuccess: 'T -> 'U)
        : ClientResult<'U> =
        match result with
        | ClientResult.Success value -> ClientResult.Success(mapSuccess value)
        | ClientResult.Failure problem -> ClientResult.Failure problem

    let foldClientResult<'T, 'TResult when 'T: not struct and 'T: not null>
        (result: ClientResult<'T>)
        (onSuccess: 'T -> 'TResult)
        (onFailure: ProblemDetailsModel -> 'TResult)
        : 'TResult =
        match result with
        | ClientResult.Success value -> onSuccess value
        | ClientResult.Failure problem -> onFailure problem

    let foldClientUnitResult<'TResult>
        (result: ClientUnitResult)
        (onSuccess: unit -> 'TResult)
        (onFailure: ProblemDetailsModel -> 'TResult)
        : 'TResult =
        match result with
        | ClientUnitResult.Success -> onSuccess ()
        | ClientUnitResult.Failure problem -> onFailure problem

    let handleClientResult<'T when 'T: not struct and 'T: not null>
        (result: ClientResult<'T>)
        (onSuccess: 'T -> unit)
        (onFailure: ProblemDetailsModel -> unit)
        : unit =
        match result with
        | ClientResult.Success value -> onSuccess value
        | ClientResult.Failure problem -> onFailure problem

    let handleClientUnitResult
        (result: ClientUnitResult)
        (onSuccess: unit -> unit)
        (onFailure: ProblemDetailsModel -> unit)
        : unit =
        match result with
        | ClientUnitResult.Success -> onSuccess ()
        | ClientUnitResult.Failure problem -> onFailure problem

    let isClientSuccess (result: IClientResult) : bool = result.IsSuccess

    let isClientFailure (result: IClientResult) : bool = not result.IsSuccess

    let getClientProblem (result: IClientResult) : ProblemDetailsModel option = result.GetClientProblem()

    let getClientValue<'T when 'T: not struct and 'T: not null> (result: ClientResult<'T>) : 'T option =
        match result with
        | ClientResult.Success value -> Some value
        | ClientResult.Failure _ -> None

    let unwrapClientResult<'T when 'T: not struct and 'T: not null> (result: ClientResult<'T>) : 'T =
        match result with
        | ClientResult.Success value -> value
        | ClientResult.Failure _ -> invalidOp "Client result failed."

    let unwrapClientUnitResult (result: ClientUnitResult) : unit =
        match result with
        | ClientUnitResult.Success -> ()
        | ClientUnitResult.Failure _ -> invalidOp "Client result failed."
