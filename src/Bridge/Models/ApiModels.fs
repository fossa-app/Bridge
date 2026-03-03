namespace Fossa.Bridge.Models

open System
open System.Collections.Generic

type AddressModel =
    { Line1: string | null
      Line2: string | null
      City: string | null
      Subdivision: string | null
      PostalCode: string | null
      CountryCode: string | null }

type IdentityClientRetrievalModel =
    { ClientId: Nullable<Guid>
      ClientName: string | null
      TenantId: Nullable<Guid> }


type BranchModificationModel =
    { Name: string | null
      TimeZoneId: string | null
      Address: AddressModel | null }

type BranchQueryRequestModel =
    { Id: IReadOnlyList<int64> | null
      Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

type BranchRetrievalModel =
    { Id: Nullable<int64>
      CompanyId: Nullable<int64>
      Name: string | null
      TimeZoneId: string | null
      Address: AddressModel | null }

type CompanyEntitlementsModel =
    { CompanyId: Nullable<int64>
      MaximumBranchCount: Nullable<int>
      MaximumEmployeeCount: Nullable<int>
      MaximumDepartmentCount: Nullable<int> }

type CompanyModificationModel =
    { Name: string | null
      CountryCode: string | null }

type CompanyRetrievalModel =
    { Id: Nullable<int64>
      Name: string | null
      CountryCode: string | null }

type CompanySettingsModificationModel = { ColorSchemeId: string | null }

type CompanySettingsRetrievalModel =
    { Id: Nullable<int64>
      CompanyId: Nullable<int64>
      ColorSchemeId: string | null }

type CountryModel =
    { Name: string | null
      Code: string | null }

type DepartmentModificationModel =
    { Name: string | null
      ParentDepartmentId: Nullable<int64>
      ManagerId: Nullable<int64> }

type DepartmentQueryRequestModel =
    { Id: IReadOnlyList<int64> | null
      Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

type DepartmentRetrievalModel =
    { Id: Nullable<int64>
      Name: string | null
      ParentDepartmentId: Nullable<int64>
      ManagerId: Nullable<int64> }

type EmployeeManagementModel =
    { AssignedBranchId: Nullable<int64>
      AssignedDepartmentId: Nullable<int64>
      ReportsToId: Nullable<int64>
      JobTitle: string | null }

type EmployeeModificationModel =
    { FirstName: string | null
      LastName: string | null
      FullName: string | null }

type EmployeePagingRequestModel =
    { Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

type EmployeeQueryRequestModel =
    { Id: IReadOnlyList<int64> | null
      Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int>
      ReportsToId: Nullable<int64>
      TopLevelOnly: Nullable<bool> }

type EmployeeRetrievalModel =
    { Id: Nullable<int64>
      CompanyId: Nullable<int64>
      AssignedBranchId: Nullable<int64>
      AssignedDepartmentId: Nullable<int64>
      ReportsToId: Nullable<int64>
      JobTitle: string | null
      FirstName: string | null
      LastName: string | null
      FullName: string | null }

type TimeZoneModel =
    { Id: string | null
      Name: string | null
      CountryCode: string | null
      CurrentOffset: Nullable<TimeSpan> }

type PartyModel =
    { LongName: string | null
      ShortName: string | null }

type LicenseTermsModel =
    { Licensor: PartyModel | null
      Licensee: PartyModel | null
      NotBefore: Nullable<DateTimeOffset>
      NotAfter: Nullable<DateTimeOffset> }

type LicenseResponseModel<'TEntitlementsModel> =
    { Terms: LicenseTermsModel | null
      Entitlements: 'TEntitlementsModel }

type PagingResponseModel<'T> =
    { PageNumber: Nullable<int>
      PageSize: Nullable<int>
      Items: IReadOnlyCollection<'T> | null
      TotalItems: Nullable<int64>
      TotalPages: Nullable<int64> }

type SystemEntitlementsModel =
    { EnvironmentName: string | null
      EnvironmentKind: string | null
      Countries: IReadOnlyList<CountryModel> | null
      TimeZones: IReadOnlyList<TimeZoneModel> | null
      MaximumCompanyCount: Nullable<int> }
