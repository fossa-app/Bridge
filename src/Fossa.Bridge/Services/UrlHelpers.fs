module Fossa.Bridge.Services.UrlHelpers

let private suffixMappings =
    [ ".dev.localhost:4211", ".dev.localhost:5210"
      ".test.localhost:4210", ".test.localhost:5211"
      ".test.localhost:4211", ".test.localhost:5211"
      ".localhost:4210", ".localhost:5210" ]

let getBackendOrigin (frontendOrigin: string) : string =
    let mapping =
        suffixMappings
        |> List.tryFind (fun (frontendSuffix, _) -> frontendOrigin.EndsWith(frontendSuffix))

    match mapping with
    | Some(frontendSuffix, backendSuffix) ->
        let prefixLength = frontendOrigin.Length - frontendSuffix.Length
        frontendOrigin.Substring(0, prefixLength) + backendSuffix
    | None -> frontendOrigin
