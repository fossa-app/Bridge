namespace Fossa.Bridge.Models.ApiModels

open System
open System.Collections.Generic

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
    { Id: int64
      CompanyId: int64
      Name: string | null
      TimeZoneId: string | null
      Address: AddressModel | null }

type CompanyEntitlementsModel =
    { CompanyId: int64
      MaximumBranchCount: int
      MaximumEmployeeCount: int
      MaximumDepartmentCount: int }

type CompanyModificationModel =
    { Name: string | null
      CountryCode: string | null }

type CompanyRetrievalModel =
    { Id: int64
      Name: string | null
      CountryCode: string | null }

type CompanySettingsModificationModel = { ColorSchemeId: string | null }

type CompanySettingsRetrievalModel =
    { Id: int64
      CompanyId: int64
      ColorSchemeId: string | null }

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
    { Id: int64
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
    { Id: int64
      CompanyId: int64
      AssignedBranchId: Nullable<int64>
      AssignedDepartmentId: Nullable<int64>
      ReportsToId: Nullable<int64>
      JobTitle: string | null
      FirstName: string | null
      LastName: string | null
      FullName: string | null }

type SystemEntitlementsModel =
    { EnvironmentName: string
      EnvironmentKind: string
      Countries: IReadOnlyList<CountryModel>
      TimeZones: IReadOnlyList<TimeZoneModel>
      MaximumCompanyCount: int }
