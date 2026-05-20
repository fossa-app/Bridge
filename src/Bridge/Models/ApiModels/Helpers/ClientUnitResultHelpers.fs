namespace Fossa.Bridge.Models.ApiModels.Helpers

open Fossa.Bridge.Models.ApiModels

module ClientUnitResultHelpers =
    let success: ClientUnitResult = { Succeeded = true; Problem = null }

    let problem (problem: ProblemDetailsModel) : ClientUnitResult =
        { Succeeded = false; Problem = problem }
