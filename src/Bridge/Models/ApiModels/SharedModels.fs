namespace Fossa.Bridge.Models.ApiModels

open System
open System.Collections.Generic

[<CLIMutable>]
type ProblemDetailsModel =
    { ``type``: string | null
      title: string | null
      status: int
      detail: string | null
      instance: string | null
      errors: Dictionary<string, string array>
      traceId: string | null }

[<CLIMutable>]
type AddressModel =
    { line1: string | null
      line2: string | null
      city: string | null
      subdivision: string | null
      postalCode: string | null
      countryCode: string | null }

[<CLIMutable>]
type CountryModel =
    { name: string | null
      code: string | null }

[<CLIMutable>]
type TimeZoneModel =
    { id: string | null
      name: string | null
      countryCode: string | null
      currentOffset: Nullable<TimeSpan> }

[<CLIMutable>]
type PartyModel =
    { longName: string | null
      shortName: string | null }

[<CLIMutable>]
type LicenseTermsModel =
    { licensor: PartyModel | null
      licensee: PartyModel | null
      notBefore: Nullable<DateTimeOffset>
      notAfter: Nullable<DateTimeOffset> }
