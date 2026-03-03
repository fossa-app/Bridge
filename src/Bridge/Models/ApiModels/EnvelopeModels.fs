namespace Fossa.Bridge.Models.ApiModels

open System
open System.Collections.Generic

type LicenseResponseModel<'TEntitlementsModel> =
    { Terms: LicenseTermsModel | null
      Entitlements: 'TEntitlementsModel }

type PagingResponseModel<'T> =
    { PageNumber: Nullable<int>
      PageSize: Nullable<int>
      Items: IReadOnlyCollection<'T> | null
      TotalItems: Nullable<int64>
      TotalPages: Nullable<int64> }
