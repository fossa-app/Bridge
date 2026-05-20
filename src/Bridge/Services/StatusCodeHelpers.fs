module Fossa.Bridge.Services.StatusCodeHelpers

open Fossa.Bridge.Models.ApiModels

let isStatusCodeClientError (statusCode: int) : bool = statusCode >= 400 && statusCode <= 499

let isStatusCodeServerError (statusCode: int) : bool = statusCode >= 500 && statusCode <= 599

let isStatusCodeSuccess (statusCode: int) : bool = statusCode >= 200 && statusCode <= 299

let isClientProblem (problem: ProblemDetailsModel) : bool = isStatusCodeClientError problem.Status

let isServerProblem (problem: ProblemDetailsModel) : bool = isStatusCodeServerError problem.Status
