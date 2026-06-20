module ClientResultTests

open System.Collections.Generic
open Expecto
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Models.Helpers

let private problem =
    { ``type`` = "https://example.com/problems/conflict"
      title = "Conflict"
      status = 409
      detail = "The requested change conflicts with current state."
      instance = "/companies/42"
      errors = Unchecked.defaultof<Dictionary<string, string array>>
      traceId = null }

[<Tests>]
let tests =
    testList
        "ClientResultTests"
        [ testCase "success helper creates success result"
          <| fun _ ->
              let result = ClientResultHelpers.success "bridge"

              match result with
              | ClientResult.Success value -> Expect.equal value "bridge" "Value should be set"
              | ClientResult.Failure _ -> failtest "Expected success"

          testCase "problem helper creates problem result"
          <| fun _ ->
              let result: ClientResult<string> = ClientResultHelpers.problem problem

              match result with
              | ClientResult.Success _ -> failtest "Expected problem"
              | ClientResult.Failure actual -> Expect.equal actual problem "Problem should be set"

          testCase "ergonomic helpers expose success result"
          <| fun _ ->
              let result = ClientResultHelpers.success "bridge"

              let matched = ClientResultHelpers.matchClientResult result id (fun _ -> "problem")

              Expect.equal matched "bridge" "Match should return success value"
              Expect.isTrue (ClientResultHelpers.isClientSuccess result) "Result should be success"
              Expect.isFalse (ClientResultHelpers.isClientFailure result) "Result should not be failure"
              Expect.equal (ClientResultHelpers.getClientValue result) (Some "bridge") "Value should be available"
              Expect.equal (ClientResultHelpers.getClientProblem result) None "Problem should not be available"
              Expect.equal (ClientResultHelpers.unwrapClientResult result) "bridge" "Unwrap should return value"

          testCase "ergonomic helpers expose failure result"
          <| fun _ ->
              let result: ClientResult<string> = ClientResultHelpers.problem problem

              let matched =
                  ClientResultHelpers.matchClientResult result id (fun actual -> string actual.title)

              Expect.equal matched (string problem.title) "Match should return problem projection"
              Expect.isFalse (ClientResultHelpers.isClientSuccess result) "Result should not be success"
              Expect.isTrue (ClientResultHelpers.isClientFailure result) "Result should be failure"
              Expect.equal (ClientResultHelpers.getClientValue result) None "Value should not be available"
              Expect.equal (ClientResultHelpers.getClientProblem result) (Some problem) "Problem should be available"
              Expect.throws (fun _ -> ClientResultHelpers.unwrapClientResult result |> ignore) "Unwrap should throw" ]
