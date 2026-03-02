namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services

type SystemLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync() : Task<LicenseResponseModel<SystemEntitlementsModel>> =
        transport.GetAsync<LicenseResponseModel<SystemEntitlementsModel>>(
            Endpoints.BasePath + "/" + Endpoints.SystemLicense
        )
