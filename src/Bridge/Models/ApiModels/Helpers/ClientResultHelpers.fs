namespace Fossa.Bridge.Models.ApiModels.Helpers

open Fossa.Bridge.Models.ApiModels

module ClientResultHelpers =
    let success<'T when 'T: not struct and 'T: not null> (value: 'T) : ClientResult<'T> = ClientResult.Success value

    let problem<'T when 'T: not struct and 'T: not null> (problem: ProblemDetailsModel) : ClientResult<'T> =
        ClientResult.Failure problem
