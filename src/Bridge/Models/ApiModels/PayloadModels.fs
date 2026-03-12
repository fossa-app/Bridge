namespace Fossa.Bridge.Models.ApiModels

open Fossa.Bridge

open System
open System.Collections.Generic

[<CLIMutable>]
type IdentityClientRetrievalModel =
    { ClientId: Nullable<Guid>
      ClientName: string | null
      TenantId: Nullable<Guid> }


[<CLIMutable>]
type BranchModificationModel =
    { Name: string | null
      TimeZoneId: string | null
      Address: AddressModel | null }

[<CLIMutable>]
type BranchQueryRequestModel =
    { Id: IReadOnlyList<int64> | null
      Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

[<CLIMutable>]
type BranchRetrievalModel =
    { Id: int64
      CompanyId: int64
      Name: string | null
      TimeZoneId: string | null
      Address: AddressModel | null }

[<CLIMutable>]
type CompanyEntitlementsModel =
    { CompanyId: int64
      MaximumBranchCount: int
      MaximumEmployeeCount: int
      MaximumDepartmentCount: int }

[<CLIMutable>]
type CompanyModificationModel =
    { Name: string | null
      CountryCode: string | null }

[<CLIMutable>]
type CompanyRetrievalModel =
    { Id: int64
      Name: string | null
      CountryCode: string | null }

[<CLIMutable>]
type CompanySettingsModificationModel = { ColorSchemeId: string | null }

[<CLIMutable>]
type CompanySettingsRetrievalModel =
    { Id: int64
      CompanyId: int64
      ColorSchemeId: string | null }

[<CLIMutable>]
type DepartmentModificationModel =
    { Name: string | null
      ParentDepartmentId: Nullable<int64>
      ManagerId: Nullable<int64> }

[<CLIMutable>]
type DepartmentQueryRequestModel =
    { Id: IReadOnlyList<int64> | null
      Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

[<CLIMutable>]
type DepartmentRetrievalModel =
    { Id: int64
      Name: string | null
      ParentDepartmentId: Nullable<int64>
      ManagerId: Nullable<int64> }

[<CLIMutable>]
type EmployeeManagementModel =
    { AssignedBranchId: Nullable<int64>
      AssignedDepartmentId: Nullable<int64>
      ReportsToId: Nullable<int64>
      JobTitle: string | null }

[<CLIMutable>]
type EmployeeModificationModel =
    { FirstName: string | null
      LastName: string | null
      FullName: string | null }

[<CLIMutable>]
type EmployeePagingRequestModel =
    { Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int> }

[<CLIMutable>]
type EmployeeQueryRequestModel =
    { Id: IReadOnlyList<int64> | null
      Search: string | null
      PageNumber: Nullable<int>
      PageSize: Nullable<int>
      ReportsToId: Nullable<int64>
      TopLevelOnly: Nullable<bool> }

[<CLIMutable>]
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

[<CLIMutable>]
type SystemEntitlementsModel =
    { EnvironmentName: string
      EnvironmentKind: string
      Countries: IReadOnlyList<CountryModel>
      TimeZones: IReadOnlyList<TimeZoneModel>
      MaximumCompanyCount: int }
