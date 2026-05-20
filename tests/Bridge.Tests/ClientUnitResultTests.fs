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

              Expect.isTrue result.Succeeded "Success should be success"
              Expect.isNull result.Problem "Problem should be absent"

          testCase "problem helper creates problem result"
          <| fun _ ->
              let result = ClientUnitResultHelpers.problem problem

              Expect.isFalse result.Succeeded "Problem should not be success"
              Expect.equal result.Problem problem "Problem should be set" ]
