namespace Fossa.Bridge.Models.ApiModels

type ClientUnitResult =
    { Succeeded: bool
      Problem: ProblemDetailsModel | null }
