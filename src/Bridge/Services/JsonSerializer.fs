#nowarn "3261"
namespace Fossa.Bridge.Services

open System
open System.Text.RegularExpressions

#if FABLE_COMPILER
open Fable.Core
#else
open System.Text.Json
#endif

type JsonSerializer() =
    let enquoteRegex =
        Regex(@"(?<=[:\[,]\s*)(-?\d{15,})(?=\s*[,\]}])", RegexOptions.Compiled)

    let dequoteRegex =
        Regex(@"(?<=[:\[,]\s*)""(-?\d{15,})""(?=\s*[,\]}])", RegexOptions.Compiled)

    interface IJsonSerializer with
        member _.Serialize<'T when 'T: not null>(value: 'T) : string =
#if FABLE_COMPILER
            let json = JS.JSON.stringify (box value)
            dequoteRegex.Replace(json, "$1")
#else
            System.Text.Json.JsonSerializer.Serialize(value)
#endif

        member _.Deserialize<'T when 'T: not null>(json: string) : 'T =
            if String.IsNullOrWhiteSpace(json) then
                Unchecked.defaultof<'T>
            else
#if FABLE_COMPILER
                let enquotedJson = enquoteRegex.Replace(json, "\"$1\"")
                JS.JSON.parse enquotedJson |> unbox<'T>
#else
                System.Text.Json.JsonSerializer.Deserialize<'T>(json)
#endif
