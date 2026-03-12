namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type SystemLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync
        (cancellationToken: CancellationToken)
        : Task<LicenseResponseModel<SystemEntitlementsModel>> =
        transport.GetAsync<LicenseResponseModel<SystemEntitlementsModel>>(
            composeRelativeUrl [ Endpoints.SystemLicense ] [],
            cancellationToken
        )

    interface ISystemLicenseClient with
        member this.GetLicenseAsync(cancellationToken) = this.GetLicenseAsync(cancellationToken)
