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
        frontendOrigin.Substring(0, prefixLength) + backendSuffix
    | None -> frontendOrigin

type UrlPart(value: Choice<string, ISpanFormattable>) =
    member _.Value = value

    static member op_Implicit(s: string) = UrlPart(Choice1Of2 s)
    static member op_Implicit(f: ISpanFormattable) = UrlPart(Choice2Of2 f)

    static member op_Implicit(b: bool) =
        UrlPart(Choice1Of2(if b then "true" else "false"))

    static member op_Implicit(i: int) =
        UrlPart(Choice2Of2(i :> ISpanFormattable))

    static member op_Implicit(i: int64) =
        UrlPart(Choice2Of2(i :> ISpanFormattable))

    static member op_Implicit(f: float) =
        UrlPart(Choice2Of2(f :> ISpanFormattable))

    static member op_Implicit(d: decimal) =
        UrlPart(Choice2Of2(d :> ISpanFormattable))

    static member op_Implicit(g: Guid) =
        UrlPart(Choice2Of2(g :> ISpanFormattable))

    static member op_Implicit(dt: DateTime) =
        UrlPart(Choice2Of2(dt :> ISpanFormattable))

    static member op_Implicit(dto: DateTimeOffset) =
        UrlPart(Choice2Of2(dto :> ISpanFormattable))

let composeRelativeUrl (relativePathSections: UrlPart list) (queryParameters: (string * UrlPart) list) : string =
    let sections =
        relativePathSections
        |> List.map (fun section ->
            match section.Value with
            | Choice1Of2 str -> str
            | Choice2Of2 formattable -> string formattable)
        |> List.map Uri.EscapeDataString

    let relativePath = Endpoints.BasePath :: sections |> String.concat "/"

    let queryString =
        match queryParameters with
        | [] -> ""
        | _ ->
            "?"
            + (queryParameters
               |> List.map (fun (key, value) ->
                   match value.Value with
                   | Choice1Of2 str -> (key, str)
                   | Choice2Of2 formattable -> (key, string formattable))
               |> List.map (fun (key, value) -> Uri.EscapeDataString key + "=" + Uri.EscapeDataString value)
               |> String.concat "&")

    relativePath + queryString
