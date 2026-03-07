namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync() : Task<LicenseResponseModel<CompanyEntitlementsModel>> =
        transport.GetAsync<LicenseResponseModel<CompanyEntitlementsModel>>(
            composeRelativeUrl [ Endpoints.CompanyLicense ] []
        )

    member _.CreateLicenseAsync(model: string) : Task =
        transport.PostAsync<string>(composeRelativeUrl [ Endpoints.CompanyLicense ] [], model)

    interface ICompanyLicenseClient with
        member this.GetLicenseAsync() = this.GetLicenseAsync()
        member this.CreateLicenseAsync(model) = this.CreateLicenseAsync(model)
