module StatusCodeHelpersTests

open System.Collections.Generic
open Expecto
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services.StatusCodeHelpers

let private problemWithStatus status =
    { Type = null
      Title = null
      Status = status
      Detail = null
      Instance = null
      Errors = Unchecked.defaultof<Dictionary<string, string array>>
      TraceId = null }

[<Tests>]
let tests =
    testList
        "StatusCodeHelpersTests"
        [ testList
              "isStatusCodeClientError"
              [ testCase "returns false below 400"
                <| fun _ -> Expect.isFalse (isStatusCodeClientError 399) "399 should not be a client error"

                testCase "returns true at 400"
                <| fun _ -> Expect.isTrue (isStatusCodeClientError 400) "400 should be a client error"

                testCase "returns true at 499"
                <| fun _ -> Expect.isTrue (isStatusCodeClientError 499) "499 should be a client error"

                testCase "returns false at 500"
                <| fun _ -> Expect.isFalse (isStatusCodeClientError 500) "500 should not be a client error" ]

          testList
              "isStatusCodeServerError"
              [ testCase "returns false at 499"
                <| fun _ -> Expect.isFalse (isStatusCodeServerError 499) "499 should not be a server error"

                testCase "returns true at 500"
                <| fun _ -> Expect.isTrue (isStatusCodeServerError 500) "500 should be a server error"

                testCase "returns true at 599"
                <| fun _ -> Expect.isTrue (isStatusCodeServerError 599) "599 should be a server error"

                testCase "returns false at 600"
                <| fun _ -> Expect.isFalse (isStatusCodeServerError 600) "600 should not be a server error" ]

          testList
              "isStatusCodeSuccess"
              [ testCase "returns false below 200"
                <| fun _ -> Expect.isFalse (isStatusCodeSuccess 199) "199 should not be success"

                testCase "returns true at 200"
                <| fun _ -> Expect.isTrue (isStatusCodeSuccess 200) "200 should be success"

                testCase "returns true at 299"
                <| fun _ -> Expect.isTrue (isStatusCodeSuccess 299) "299 should be success"

                testCase "returns false at 300"
                <| fun _ -> Expect.isFalse (isStatusCodeSuccess 300) "300 should not be success" ]

          testList
              "ProblemDetailsModel helpers"
              [ testCase "client problem returns client true and server false"
                <| fun _ ->
                    let problem = problemWithStatus 404

                    Expect.isTrue (isClientProblem problem) "4xx problem should be a client problem"
                    Expect.isFalse (isServerProblem problem) "4xx problem should not be a server problem"

                testCase "server problem returns server true and client false"
                <| fun _ ->
                    let problem = problemWithStatus 500

                    Expect.isTrue (isServerProblem problem) "5xx problem should be a server problem"
                    Expect.isFalse (isClientProblem problem) "5xx problem should not be a client problem"

                testCase "non-error problem returns false for both helpers"
                <| fun _ ->
                    let problem = problemWithStatus 200

                    Expect.isFalse (isClientProblem problem) "Non-error problem should not be a client problem"
                    Expect.isFalse (isServerProblem problem) "Non-error problem should not be a server problem" ] ]
