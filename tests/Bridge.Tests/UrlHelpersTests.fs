module UrlHelpersTests

open System
open Expecto
open Fossa.Bridge.Services.UrlHelpers

[<Tests>]
let tests =
    testList
        "UrlHelpersTests"
        [ testList
              "getBackendOrigin"
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

          testList
              "composeRelativeUrl"
              [ testCase "string sections and no query parameters"
                <| fun _ ->
                    let result = composeRelativeUrl [ "api"; "users" ] []
                    Expect.equal result "api/users" "Should compose simple string paths correctly"

                testCase "string sections with numeric string and no query parameters"
                <| fun _ ->
                    let result = composeRelativeUrl [ "api"; "users"; 123 ] []

                    Expect.equal
                        result
                        "api/users/123"
                        "Should compose simple string paths correctly with numeric parts"

                testCase "api/users/123 homogeneously built natively with integer types"
                <| fun _ ->
                    let result =
                        composeRelativeUrl [ ("api": UrlPart); ("users": UrlPart); (123: UrlPart) ] []

                    Expect.equal
                        result
                        "api/users/123"
                        "Should natively format string and int paths seamlessly when uniformly referenced"

                testCase "query parameters"
                <| fun _ ->
                    let result = composeRelativeUrl [ "api"; "users" ] [ "sort", "desc" ]

                    Expect.equal result "api/users?sort=desc" "Should compose query parameters correctly"

                testCase "numeric query parameters natively"
                <| fun _ ->
                    let result =
                        composeRelativeUrl [ "api"; "users" ] [ "limit", (10: UrlPart); "offset", (20: UrlPart) ]

                    Expect.equal
                        result
                        "api/users?limit=10&offset=20"
                        "Should compose numeric query parameters seamlessly"

                testCase "multiple query parameters with the same name"
                <| fun _ ->
                    let result =
                        composeRelativeUrl [ "api"; "users" ] [ "filter", "active"; "filter", "admin" ]

                    Expect.equal
                        result
                        "api/users?filter=active&filter=admin"
                        "Should keep duplicate query parameters correctly"

                testCase "url encoding"
                <| fun _ ->
                    let result = composeRelativeUrl [ "api"; "special paths" ] [ "q", "test query" ]

                    Expect.equal
                        result
                        "api/special%20paths?q=test%20query"
                        "Should url encode paths and query parameters correctly"

                testCase "int64 paths and query parameters"
                <| fun _ ->
                    let result =
                        composeRelativeUrl
                            [ "api"; (9223372036854775807L: UrlPart) ]
                            [ "id", (1234567890123456789L: UrlPart) ]

                    Expect.equal
                        result
                        "api/9223372036854775807?id=1234567890123456789"
                        "Should compose int64 correctly"

                testCase "float paths and query parameters"
                <| fun _ ->
                    let result =
                        composeRelativeUrl [ "api"; (123.45: UrlPart) ] [ "val", (67.89: UrlPart) ]

                    Expect.equal result "api/123.45?val=67.89" "Should compose float correctly"

                testCase "decimal paths and query parameters"
                <| fun _ ->
                    let result =
                        composeRelativeUrl [ "api"; (123.45m: UrlPart) ] [ "val", (67.89m: UrlPart) ]

                    Expect.equal result "api/123.45?val=67.89" "Should compose decimal correctly"

                testCase "Guid paths and query parameters"
                <| fun _ ->
                    let id1 = Guid.Parse("12345678-1234-1234-1234-123456789012")
                    let id2 = Guid.Parse("87654321-4321-4321-4321-210987654321")
                    let result = composeRelativeUrl [ "api"; (id1: UrlPart) ] [ "ref", (id2: UrlPart) ]

                    Expect.equal
                        result
                        "api/12345678-1234-1234-1234-123456789012?ref=87654321-4321-4321-4321-210987654321"
                        "Should compose Guid correctly"

                ] ]
