namespace Fossa.Bridge.Models.Helpers

open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

module ClientUnitResultHelpers =
    let success: ClientUnitResult = ClientUnitResult.Success

    let problem (problem: ProblemDetailsModel) : ClientUnitResult = ClientUnitResult.Failure problem
