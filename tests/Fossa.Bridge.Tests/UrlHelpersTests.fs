module UrlHelpersTests

open Expecto
open Fossa.Bridge.Services.UrlHelpers

[<Tests>]
let tests =
    testList
        "UrlHelpersTests"
        [ testCase "dev localhost 4211 mapped to 5210"
          <| fun _ ->
              let result = getBackendOrigin "http://app.dev.localhost:4211"
              Expect.equal result "http://app.dev.localhost:5210" "Should map dev correctly"

          testCase "test localhost 4210 mapped to 5211"
          <| fun _ ->
              let result = getBackendOrigin "http://app.test.localhost:4210"
              Expect.equal result "http://app.test.localhost:5211" "Should map test correctly"

          testCase "test localhost 4211 mapped to 5211"
          <| fun _ ->
              let result = getBackendOrigin "http://app.test.localhost:4211"
              Expect.equal result "http://app.test.localhost:5211" "Should map test correctly"

          testCase "localhost 4210 mapped to 5210"
          <| fun _ ->
              let result = getBackendOrigin "http://app.localhost:4210"
              Expect.equal result "http://app.localhost:5210" "Should map localhost correctly"

          testCase "unmapped origin is unchanged"
          <| fun _ ->
              let result = getBackendOrigin "http://app.prod.com"
              Expect.equal result "http://app.prod.com" "Should not change if no mapping" ]
