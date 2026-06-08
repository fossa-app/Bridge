module ClientResultTests

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
              | ClientResult.Failure actual -> Expect.equal actual problem "Problem should be set" ]
