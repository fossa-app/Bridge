module ClientUnitResultTests

open System.Collections.Generic
open Expecto
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Models.ApiModels.Helpers

let private problem =
    { Type = "https://example.com/problems/conflict"
      Title = "Conflict"
      Status = 409
      Detail = "The requested change conflicts with current state."
      Instance = "/companies/42"
      Errors = Unchecked.defaultof<Dictionary<string, string array>>
      TraceId = null }

[<Tests>]
let tests =
    testList
        "ClientUnitResultTests"
        [ testCase "success helper creates success result"
          <| fun _ ->
              let result = ClientUnitResultHelpers.success

              match result with
              | ClientUnitResult.Success -> ()
              | ClientUnitResult.Failure _ -> failtest "Expected success"

          testCase "problem helper creates problem result"
          <| fun _ ->
              let result = ClientUnitResultHelpers.problem problem

              match result with
              | ClientUnitResult.Success -> failtest "Expected problem"
              | ClientUnitResult.Failure actual -> Expect.equal actual problem "Problem should be set"

          testCase "ergonomic helpers expose success result"
          <| fun _ ->
              let result = ClientUnitResultHelpers.success

              let matched =
                  ClientResultHelpers.matchClientUnitResult result (fun () -> "ok") (fun _ -> "problem")

              Expect.equal matched "ok" "Match should return success projection"
              Expect.isTrue (ClientResultHelpers.isClientSuccess result) "Result should be success"
              Expect.isFalse (ClientResultHelpers.isClientFailure result) "Result should not be failure"
              Expect.equal (ClientResultHelpers.getClientProblem result) None "Problem should not be available"
              Expect.equal (ClientResultHelpers.unwrapClientUnitResult result) () "Unwrap should return unit"

          testCase "ergonomic helpers expose failure result"
          <| fun _ ->
              let result = ClientUnitResultHelpers.problem problem

              let matched =
                  ClientResultHelpers.matchClientUnitResult result (fun () -> "ok") (fun actual -> string actual.Title)

              Expect.equal matched (string problem.Title) "Match should return problem projection"
              Expect.isFalse (ClientResultHelpers.isClientSuccess result) "Result should not be success"
              Expect.isTrue (ClientResultHelpers.isClientFailure result) "Result should be failure"
              Expect.equal (ClientResultHelpers.getClientProblem result) (Some problem) "Problem should be available"
              Expect.throws (fun _ -> ClientResultHelpers.unwrapClientUnitResult result) "Unwrap should throw" ]
