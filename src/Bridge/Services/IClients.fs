namespace Fossa.Bridge.Services

open Fossa.Bridge.Services.Clients

type IClients =
    abstract BranchClient: IBranchClient
    abstract CompanyClient: ICompanyClient
    abstract CompanyLicenseClient: ICompanyLicenseClient
    abstract CompanySettingsClient: ICompanySettingsClient
    abstract DepartmentClient: IDepartmentClient
    abstract EmployeeClient: IEmployeeClient
    abstract IdentityClient: IIdentityClient
    abstract SystemLicenseClient: ISystemLicenseClient
