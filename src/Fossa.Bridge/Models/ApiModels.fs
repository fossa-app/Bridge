namespace Fossa.Bridge.Models

open System
open System.Collections.Generic

type AddressModel =
    { Line1: string
      Line2: string
      City: string
      Subdivision: string
      PostalCode: string
      CountryCode: string }

type BranchModificationModel =
    { Name: string
      TimeZoneId: string
      Address: AddressModel }

type BranchQueryRequestModel =
    { Id: IReadOnlyList<int64>
      Search: string
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

type BranchRetrievalModel =
    { Id: int64
      CompanyId: int64
      Name: string
      TimeZoneId: string
      Address: AddressModel }

type CompanyEntitlementsModel =
    { CompanyId: int64
      MaximumBranchCount: int
      MaximumEmployeeCount: int
      MaximumDepartmentCount: int }

type CompanyModificationModel = { Name: string; CountryCode: string }

type CompanyRetrievalModel =
    { Id: int64
      Name: string
      CountryCode: string }

type CompanySettingsModificationModel = { ColorSchemeId: string }

type CompanySettingsRetrievalModel =
    { Id: int64
      CompanyId: int64
      ColorSchemeId: string }

type CountryModel = { Name: string; Code: string }

type DepartmentModificationModel =
    { Name: string
      ParentDepartmentId: Nullable<int64>
      ManagerId: Nullable<int64> }

type DepartmentQueryRequestModel =
    { Id: IReadOnlyList<int64>
      Search: string
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

type DepartmentRetrievalModel =
    { Id: int64
      Name: string
      ParentDepartmentId: Nullable<int64>
      ManagerId: int64 }

type EmployeeManagementModel =
    { AssignedBranchId: Nullable<int64>
      AssignedDepartmentId: Nullable<int64>
      ReportsToId: Nullable<int64>
      JobTitle: string }

type EmployeeModificationModel =
    { FirstName: string
      LastName: string
      FullName: string }

type EmployeePagingRequestModel =
    { Search: string
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

type EmployeeQueryRequestModel =
    { Id: IReadOnlyList<int64>
      Search: string
      PageNumber: Nullable<int>
      PageSize: Nullable<int>
      ReportsToId: Nullable<int64>
      TopLevelOnly: Nullable<bool> }

type EmployeeRetrievalModel =
    { Id: int64
      CompanyId: int64
      AssignedBranchId: Nullable<int64>
      AssignedDepartmentId: Nullable<int64>
      ReportsToId: Nullable<int64>
      JobTitle: string
      FirstName: string
      LastName: string
      FullName: string }

type TimeZoneModel =
    { Id: string
      Name: string
      CountryCode: string
      CurrentOffset: TimeSpan }

type PartyModel = { LongName: string; ShortName: string }

type LicenseTermsModel =
    { Licensor: PartyModel
      Licensee: PartyModel
      NotBefore: DateTimeOffset
      NotAfter: DateTimeOffset }

type LicenseResponseModel<'TEntitlementsModel> =
    { Terms: LicenseTermsModel
      Entitlements: 'TEntitlementsModel }

type PagingResponseModel<'T> =
    { PageNumber: Nullable<int>
      PageSize: Nullable<int>
      Items: IReadOnlyCollection<'T>
      TotalItems: Nullable<int64>
      TotalPages: Nullable<int64> }

type SystemEntitlementsModel =
    { EnvironmentName: string
      EnvironmentKind: string
      Countries: IReadOnlyList<CountryModel>
      TimeZones: IReadOnlyList<TimeZoneModel>
      MaximumCompanyCount: int }
