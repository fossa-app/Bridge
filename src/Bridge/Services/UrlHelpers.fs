module Fossa.Bridge.Services.UrlHelpers


open System

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
        $"{frontendOrigin.Substring(0, prefixLength)}{backendSuffix}"
    | None -> frontendOrigin

type UrlPart(value: string) =
    member _.Value = value

    static member op_Implicit(s: string) = UrlPart s

    static member op_Implicit(b: bool) = UrlPart(if b then "true" else "false")

    static member op_Implicit(i: int) = UrlPart(string i)

    static member op_Implicit(i: int64) = UrlPart(string i)

    static member op_Implicit(f: float) = UrlPart(string f)

    static member op_Implicit(d: decimal) = UrlPart(string d)

    static member op_Implicit(g: Guid) = UrlPart(string g)

    static member op_Implicit(dt: DateTime) = UrlPart(string dt)

    static member op_Implicit(dto: DateTimeOffset) = UrlPart(string dto)

let composeRelativeUrl
    (endpointUrl: string)
    (requiresToken: EndpointSecurity)
    (relativePathSections: UrlPart list)
    (queryParameters: (string * UrlPart) list)
    : string * EndpointSecurity =

    let sections =
        relativePathSections
        |> List.map (fun section -> section.Value)
        |> List.map Uri.EscapeDataString

    let relativePath =
        Endpoints.BasePath :: endpointUrl :: sections |> String.concat "/"

    let queryString =
        match queryParameters with
        | [] -> ""
        | _ ->
            let queryParamsStr =
                queryParameters
                |> List.map (fun (key, value) -> $"{Uri.EscapeDataString key}={Uri.EscapeDataString value.Value}")
                |> String.concat "&"

            $"?{queryParamsStr}"

    ($"{relativePath}{queryString}", requiresToken)
