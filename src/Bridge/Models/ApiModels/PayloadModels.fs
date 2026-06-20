namespace Fossa.Bridge.Models.ApiModels


open System
open System.Collections.Generic

[<CLIMutable>]
type IdentityClientRetrievalModel =
    { clientId: Nullable<Guid>
      clientName: string | null
      tenantId: Nullable<Guid> }


[<CLIMutable>]
type BranchModificationModel =
    { name: string | null
      timeZoneId: string | null
      address: AddressModel | null }

[<CLIMutable>]
type BranchQueryRequestModel =
    { id: IReadOnlyList<int64> | null
      search: string | null
      pageNumber: Nullable<int>
      pageSize: Nullable<int> }

[<CLIMutable>]
type BranchRetrievalModel =
    { id: int64
      companyId: int64
      name: string | null
      timeZoneId: string | null
      address: AddressModel | null }

[<CLIMutable>]
type CompanyEntitlementsModel =
    { companyId: int64
      maximumBranchCount: int
      maximumEmployeeCount: int
      maximumDepartmentCount: int }

[<CLIMutable>]
type CompanyModificationModel =
    { name: string | null
      countryCode: string | null }

[<CLIMutable>]
type CompanyRetrievalModel =
    { id: int64
      name: string | null
      countryCode: string | null }

[<CLIMutable>]
type CompanySettingsModificationModel = { colorSchemeId: string | null }

[<CLIMutable>]
type CompanySettingsRetrievalModel =
    { id: int64
      companyId: int64
      colorSchemeId: string | null }

[<CLIMutable>]
type DepartmentModificationModel =
    { name: string | null
      parentDepartmentId: Nullable<int64>
      managerId: Nullable<int64> }

[<CLIMutable>]
type DepartmentQueryRequestModel =
    { id: IReadOnlyList<int64> | null
      search: string | null
      pageNumber: Nullable<int>
      pageSize: Nullable<int> }

[<CLIMutable>]
type DepartmentRetrievalModel =
    { id: int64
      name: string | null
      parentDepartmentId: Nullable<int64>
      managerId: Nullable<int64> }

[<CLIMutable>]
type EmployeeManagementModel =
    { assignedBranchId: Nullable<int64>
      assignedDepartmentId: Nullable<int64>
      reportsToId: Nullable<int64>
      jobTitle: string | null }

[<CLIMutable>]
type EmployeeModificationModel =
    { firstName: string | null
      lastName: string | null
      fullName: string | null }

[<CLIMutable>]
type EmployeePagingRequestModel =
    { search: string | null
      pageNumber: Nullable<int>
      pageSize: Nullable<int> }

[<CLIMutable>]
type EmployeeQueryRequestModel =
    { id: IReadOnlyList<int64> | null
      search: string | null
      pageNumber: Nullable<int>
      pageSize: Nullable<int>
      reportsToId: Nullable<int64>
      topLevelOnly: Nullable<bool> }

[<CLIMutable>]
type EmployeeRetrievalModel =
    { id: int64
      companyId: int64
      assignedBranchId: Nullable<int64>
      assignedDepartmentId: Nullable<int64>
      reportsToId: Nullable<int64>
      jobTitle: string | null
      firstName: string | null
      lastName: string | null
      fullName: string | null }

[<CLIMutable>]
type SystemEntitlementsModel =
    { environmentName: string
      environmentKind: string
      countries: IReadOnlyList<CountryModel>
      timeZones: IReadOnlyList<TimeZoneModel>
      maximumCompanyCount: int }
