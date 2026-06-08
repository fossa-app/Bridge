namespace Fossa.Bridge.Models.ApiModels.Helpers

open Fossa.Bridge.Models.ApiModels

module ClientUnitResultHelpers =
    let success: ClientUnitResult = ClientUnitResult.Success

    let problem (problem: ProblemDetailsModel) : ClientUnitResult = ClientUnitResult.Failure problem
