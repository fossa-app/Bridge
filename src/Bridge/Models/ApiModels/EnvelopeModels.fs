namespace Fossa.Bridge.Models.ApiModels


open System
open System.Collections.Generic
open Fossa.Bridge

[<CLIMutable>]
type LicenseResponseModel<'TEntitlementsModel> =
    { terms: LicenseTermsModel
      entitlements: 'TEntitlementsModel }

[<CLIMutable>]
type PagingResponseModel<'T> =
    { pageNumber: Nullable<int>
      pageSize: Nullable<int>
      items: IReadOnlyCollection<'T>
      totalItems: Nullable<ApproximateInt64>
      totalPages: Nullable<ApproximateInt64> }
