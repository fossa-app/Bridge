namespace Fossa.Bridge.Services

type EndpointSecurity =
    | Anonymous
    | RequireToken

module Endpoints =
    let BasePath = "api/1.0"
    let Client = "Identity/Client", Anonymous
    let SystemLicense = "License/System", Anonymous
    let CompanyLicense = "License/Company", RequireToken
    let Company = "Company", RequireToken
    let CompanySettings = "CompanySettings", RequireToken
    let Branches = "Branches", RequireToken
    let Departments = "Departments", RequireToken
    let Employee = "Employee", RequireToken
    let Employees = "Employees", RequireToken
