module ClientResultTests

open Expecto
open Fossa.Bridge.Models.ApiModels

let private problem =
    { Type = "https://example.com/problems/conflict"
      Title = "Conflict"
      Status = 409
      Detail = "The requested change conflicts with current state."
      Instance = "/companies/42" }

[<Tests>]
let tests =
    testList
        "ClientResultTests"
        [ testList
              "ClientResult helpers"
              [ testCase "success helpers expose the value"
                <| fun _ ->
                    let result = ClientResult.Success 21

                    Expect.isTrue (ClientResult.isSuccess result) "Success should be success"
                    Expect.isFalse (ClientResult.isProblem result) "Success should not be problem"

                    Expect.equal
                        (ClientResult.map ((*) 2) result)
                        (ClientResult.Success 42)
                        "Map should transform value"

                    Expect.equal (ClientResult.defaultValue 0 result) 21 "Default value should not be used"
                    Expect.equal (ClientResult.valueOrDefault result) 21 "Value should be returned"
                    Expect.equal (ClientResult.valueOrNone result) (Some 21) "Value option should be present"
                    Expect.equal (ClientResult.problemOrNone result) None "Problem option should be absent"

                testCase "problem helpers expose the problem"
                <| fun _ ->
                    let result: ClientResult<int> = ClientResult.Problem problem

                    Expect.isFalse (ClientResult.isSuccess result) "Problem should not be success"
                    Expect.isTrue (ClientResult.isProblem result) "Problem should be problem"

                    Expect.equal
                        (ClientResult.map ((*) 2) result)
                        (ClientResult.Problem problem)
                        "Map should preserve problem"

                    Expect.equal
                        (ClientResult.bind (fun value -> ClientResult.Success(value * 2)) result)
                        (ClientResult.Problem problem)
                        "Bind should preserve problem"

                    Expect.equal (ClientResult.defaultValue 42 result) 42 "Default value should be used"
                    Expect.equal (ClientResult.valueOrDefault result) 0 "Default int should be returned"
                    Expect.equal (ClientResult.valueOrNone result) None "Value option should be absent"
                    Expect.equal (ClientResult.problemOrNone result) (Some problem) "Problem option should be present" ]

          testList
              "ClientUnitResult helpers"
              [ testCase "success converts to generic unit success"
                <| fun _ ->
                    let result = ClientUnitResult.Success

                    Expect.isTrue (ClientUnitResult.isSuccess result) "Success should be success"
                    Expect.isFalse (ClientUnitResult.isProblem result) "Success should not be problem"
                    Expect.equal (ClientUnitResult.problemOrNone result) None "Problem option should be absent"

                    Expect.equal
                        (ClientUnitResult.toGeneric result)
                        (ClientResult.Success())
                        "Generic result should be success"

                testCase "problem converts through generic unit result"
                <| fun _ ->
                    let result = ClientUnitResult.Problem problem
                    let generic = ClientUnitResult.toGeneric result

                    Expect.isFalse (ClientUnitResult.isSuccess result) "Problem should not be success"
                    Expect.isTrue (ClientUnitResult.isProblem result) "Problem should be problem"

                    Expect.equal
                        (ClientUnitResult.problemOrNone result)
                        (Some problem)
                        "Problem option should be present"

                    Expect.equal generic (ClientResult.Problem problem) "Generic result should preserve problem"
                    Expect.equal (ClientUnitResult.ofGeneric generic) result "Unit result should round-trip" ] ]
