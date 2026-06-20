namespace Fossa.Bridge.Models.ApiModels


open System
open System.Collections.Generic

[<CLIMutable>]
type LicenseResponseModel<'TEntitlementsModel> =
    { terms: LicenseTermsModel
      entitlements: 'TEntitlementsModel }

[<CLIMutable>]
type PagingResponseModel<'T> =
    { pageNumber: Nullable<int>
      pageSize: Nullable<int>
      items: IReadOnlyCollection<'T>
      totalItems: Nullable<int64>
      totalPages: Nullable<int64> }
