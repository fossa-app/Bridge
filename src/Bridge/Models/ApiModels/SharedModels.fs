namespace Fossa.Bridge.Models.ApiModels

open System

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
