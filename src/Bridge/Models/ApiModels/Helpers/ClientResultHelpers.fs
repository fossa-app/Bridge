namespace Fossa.Bridge.Models.ApiModels.Helpers

open System
open Fossa.Bridge.Models.ApiModels

module ClientResultHelpers =
    let success<'T when 'T: not struct and 'T: not null> (value: 'T) : ClientResult<'T> = ClientResult.Success value

    let problem<'T when 'T: not struct and 'T: not null> (problem: ProblemDetailsModel) : ClientResult<'T> =
        ClientResult.Failure problem

    let matchClientResult<'T, 'TResult when 'T: not struct and 'T: not null>
        (result: ClientResult<'T>)
        (onSuccess: 'T -> 'TResult)
        (onFailure: ProblemDetailsModel -> 'TResult)
        : 'TResult =
        result.Match(onSuccess, onFailure)

    let matchClientUnitResult<'TResult>
        (result: ClientUnitResult)
        (onSuccess: unit -> 'TResult)
        (onFailure: ProblemDetailsModel -> 'TResult)
        : 'TResult =
        result.Match(onSuccess, onFailure)

    let isClientSuccess (result: IClientResult) : bool = result.IsSuccess

    let isClientFailure (result: IClientResult) : bool = not result.IsSuccess

    let getClientProblem (result: IClientResult) : ProblemDetailsModel option = result.GetClientProblem()

    let getClientValue<'T when 'T: not struct and 'T: not null> (result: ClientResult<'T>) : 'T option =
        match result with
        | ClientResult.Success value -> Some value
        | ClientResult.Failure _ -> None

    let private problemMessage (problem: ProblemDetailsModel) =
        let title = problem.Title

        if String.IsNullOrWhiteSpace title then
            "Client result failed."
        else
            string title

    let unwrapClientResult<'T when 'T: not struct and 'T: not null> (result: ClientResult<'T>) : 'T =
        match result with
        | ClientResult.Success value -> value
        | ClientResult.Failure problem -> invalidOp (problemMessage problem)

    let unwrapClientUnitResult (result: ClientUnitResult) : unit =
        match result with
        | ClientUnitResult.Success -> ()
        | ClientUnitResult.Failure problem -> invalidOp (problemMessage problem)
