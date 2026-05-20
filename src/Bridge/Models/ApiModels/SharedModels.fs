namespace Fossa.Bridge.Models.ApiModels

open System

[<CLIMutable>]
type ProblemDetailsModel =
    { Type: string | null
      Title: string | null
      Status: int
      Detail: string | null
      Instance: string | null }

[<RequireQualifiedAccess>]
type ClientResult<'T> =
    | Success of 'T
    | Problem of ProblemDetailsModel

[<RequireQualifiedAccess>]
type ClientUnitResult =
    | Success
    | Problem of ProblemDetailsModel

[<RequireQualifiedAccess>]
module ClientResult =
    let isSuccess (result: ClientResult<'T>) : bool =
        match result with
        | ClientResult.Success _ -> true
        | ClientResult.Problem _ -> false

    let isProblem (result: ClientResult<'T>) : bool = not (isSuccess result)

    let map (mapper: 'T -> 'U) (result: ClientResult<'T>) : ClientResult<'U> =
        match result with
        | ClientResult.Success value -> ClientResult.Success(mapper value)
        | ClientResult.Problem problem -> ClientResult.Problem problem

    let bind (binder: 'T -> ClientResult<'U>) (result: ClientResult<'T>) : ClientResult<'U> =
        match result with
        | ClientResult.Success value -> binder value
        | ClientResult.Problem problem -> ClientResult.Problem problem

    let defaultValue (fallback: 'T) (result: ClientResult<'T>) : 'T =
        match result with
        | ClientResult.Success value -> value
        | ClientResult.Problem _ -> fallback

    let valueOrDefault (result: ClientResult<'T>) : 'T =
        match result with
        | ClientResult.Success value -> value
        | ClientResult.Problem _ -> Unchecked.defaultof<'T>

    let problemOrNone (result: ClientResult<'T>) : ProblemDetailsModel option =
        match result with
        | ClientResult.Success _ -> None
        | ClientResult.Problem problem -> Some problem

    let valueOrNone (result: ClientResult<'T>) : 'T option =
        match result with
        | ClientResult.Success value -> Some value
        | ClientResult.Problem _ -> None

[<RequireQualifiedAccess>]
module ClientUnitResult =
    let isSuccess (result: ClientUnitResult) : bool =
        match result with
        | ClientUnitResult.Success -> true
        | ClientUnitResult.Problem _ -> false

    let isProblem (result: ClientUnitResult) : bool = not (isSuccess result)

    let problemOrNone (result: ClientUnitResult) : ProblemDetailsModel option =
        match result with
        | ClientUnitResult.Success -> None
        | ClientUnitResult.Problem problem -> Some problem

    let toGeneric (result: ClientUnitResult) : ClientResult<unit> =
        match result with
        | ClientUnitResult.Success -> ClientResult.Success()
        | ClientUnitResult.Problem problem -> ClientResult.Problem problem

    let ofGeneric (result: ClientResult<unit>) : ClientUnitResult =
        match result with
        | ClientResult.Success() -> ClientUnitResult.Success
        | ClientResult.Problem problem -> ClientUnitResult.Problem problem

[<CLIMutable>]
type AddressModel =
    { Line1: string | null
      Line2: string | null
      City: string | null
      Subdivision: string | null
      PostalCode: string | null
      CountryCode: string | null }

[<CLIMutable>]
type CountryModel =
    { Name: string | null
      Code: string | null }

[<CLIMutable>]
type TimeZoneModel =
    { Id: string | null
      Name: string | null
      CountryCode: string | null
      CurrentOffset: Nullable<TimeSpan> }

[<CLIMutable>]
type PartyModel =
    { LongName: string | null
      ShortName: string | null }

[<CLIMutable>]
type LicenseTermsModel =
    { Licensor: PartyModel | null
      Licensee: PartyModel | null
      NotBefore: Nullable<DateTimeOffset>
      NotAfter: Nullable<DateTimeOffset> }
