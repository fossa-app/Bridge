namespace Fossa.Bridge.Models.ApiModels

type ClientResult<'T when 'T: not struct and 'T: not null> =
    { Succeeded: bool
      Value: 'T | null
      Problem: ProblemDetailsModel | null }
