namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type SystemLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync() : Task<LicenseResponseModel<SystemEntitlementsModel>> =
        transport.GetAsync<LicenseResponseModel<SystemEntitlementsModel>>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.SystemLicense ] []
        )

    interface ISystemLicenseClient with
        member this.GetLicenseAsync() = this.GetLicenseAsync()
