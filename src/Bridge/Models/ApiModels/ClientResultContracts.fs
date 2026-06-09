namespace Fossa.Bridge.Models.ApiModels

type IClientResult =
    abstract IsSuccess: bool
    abstract GetClientProblem: unit -> ProblemDetailsModel option
