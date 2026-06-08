module ClientUnitResultTests

open Expecto
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Models.ApiModels.Helpers

let private problem =
    { Type = "https://example.com/problems/conflict"
      Title = "Conflict"
      Status = 409
      Detail = "The requested change conflicts with current state."
      Instance = "/companies/42" }

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
              | ClientUnitResult.Failure actual -> Expect.equal actual problem "Problem should be set" ]
