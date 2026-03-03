namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync() : Task<LicenseResponseModel<CompanyEntitlementsModel>> =
        transport.GetAsync<LicenseResponseModel<CompanyEntitlementsModel>>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanyLicense ] []
        )

    member _.CreateLicenseAsync(model: string) : Task<LicenseResponseModel<CompanyEntitlementsModel>> =
        transport.PostAsync<string, LicenseResponseModel<CompanyEntitlementsModel>>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanyLicense ] [],
            model
        )
