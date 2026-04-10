namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge.Services

open Fossa.Bridge.Services.Clients

type Clients(transport: IHttpTransport) =
    let branchClient: IBranchClient = BranchClient(transport) :> IBranchClient
    let companyClient: ICompanyClient = CompanyClient(transport) :> ICompanyClient

    let companyLicenseClient: ICompanyLicenseClient =
        CompanyLicenseClient(transport) :> ICompanyLicenseClient

    let companySettingsClient: ICompanySettingsClient =
        CompanySettingsClient(transport) :> ICompanySettingsClient

    let departmentClient: IDepartmentClient =
        DepartmentClient(transport) :> IDepartmentClient

    let employeeClient: IEmployeeClient = EmployeeClient(transport) :> IEmployeeClient
    let identityClient: IIdentityClient = IdentityClient(transport) :> IIdentityClient

    let systemLicenseClient: ISystemLicenseClient =
        SystemLicenseClient(transport) :> ISystemLicenseClient

    interface IClients with
        member _.BranchClient = branchClient
        member _.CompanyClient = companyClient
        member _.CompanyLicenseClient = companyLicenseClient
        member _.CompanySettingsClient = companySettingsClient
        member _.DepartmentClient = departmentClient
        member _.EmployeeClient = employeeClient
        member _.IdentityClient = identityClient
        member _.SystemLicenseClient = systemLicenseClient
