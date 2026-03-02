namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services

type CompanyLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync() : Task<LicenseResponseModel<CompanyEntitlementsModel>> =
        transport.GetAsync<LicenseResponseModel<CompanyEntitlementsModel>>(
            Endpoints.BasePath + "/" + Endpoints.CompanyLicense
        )

    member _.CreateLicenseAsync(model: string) : Task<LicenseResponseModel<CompanyEntitlementsModel>> =
        transport.PostAsync<string, LicenseResponseModel<CompanyEntitlementsModel>>(
            Endpoints.BasePath + "/" + Endpoints.CompanyLicense,
            model
        )
