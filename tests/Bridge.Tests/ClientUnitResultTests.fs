module ClientUnitResultTests

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

              let folded =
                  ClientResultHelpers.foldClientUnitResult result (fun () -> "ok") (fun _ -> "problem")

              let mutable successHandled = false
              let mutable failureHandled = false

              ClientResultHelpers.handleClientUnitResult result (fun () -> successHandled <- true) (fun _ ->
                  failureHandled <- true)

              Expect.equal folded "ok" "Fold should return success projection"
              Expect.isTrue successHandled "Handle should invoke success callback"
              Expect.isFalse failureHandled "Handle should not invoke failure callback"
              Expect.isTrue (ClientResultHelpers.isClientSuccess result) "Result should be success"
              Expect.isFalse (ClientResultHelpers.isClientFailure result) "Result should not be failure"
              Expect.equal (ClientResultHelpers.getClientProblem result) None "Problem should not be available"
              Expect.equal (ClientResultHelpers.unwrapClientUnitResult result) () "Unwrap should return unit"

          testCase "ergonomic helpers expose failure result"
          <| fun _ ->
              let result = ClientUnitResultHelpers.problem problem

              let folded =
                  ClientResultHelpers.foldClientUnitResult result (fun () -> "ok") (fun actual -> string actual.title)

              let mutable successHandled = false
              let mutable failureHandled = false

              ClientResultHelpers.handleClientUnitResult result (fun () -> successHandled <- true) (fun actual ->
                  failureHandled <- actual = problem)

              Expect.equal folded (string problem.title) "Fold should return problem projection"
              Expect.isFalse successHandled "Handle should not invoke success callback"
              Expect.isTrue failureHandled "Handle should invoke failure callback"
              Expect.isFalse (ClientResultHelpers.isClientSuccess result) "Result should not be success"
              Expect.isTrue (ClientResultHelpers.isClientFailure result) "Result should be failure"
              Expect.equal (ClientResultHelpers.getClientProblem result) (Some problem) "Problem should be available"
              Expect.throws (fun _ -> ClientResultHelpers.unwrapClientUnitResult result) "Unwrap should throw" ]
