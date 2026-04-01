namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type SystemLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync
        (cancellationToken: CancellationToken)
        : Task<LicenseResponseModel<SystemEntitlementsModel>> =
        let endpointPath, securityRequirement = Endpoints.SystemLicense

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<LicenseResponseModel<SystemEntitlementsModel>>(
            endpointUrl,
            endpointSecurity,
            cancellationToken
        )

    interface ISystemLicenseClient with
        member this.GetLicenseAsync(cancellationToken) = this.GetLicenseAsync(cancellationToken)
