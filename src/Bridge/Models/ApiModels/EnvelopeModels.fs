namespace Fossa.Bridge.Models.ApiModels

open Fossa.Bridge

open System
open System.Collections.Generic

[<CLIMutable>]
type LicenseResponseModel<'TEntitlementsModel> =
    { Terms: LicenseTermsModel
      Entitlements: 'TEntitlementsModel }

[<CLIMutable>]
type PagingResponseModel<'T> =
    { PageNumber: Nullable<int>
      PageSize: Nullable<int>
      Items: IReadOnlyCollection<'T>
      TotalItems: Nullable<int64>
      TotalPages: Nullable<int64> }
