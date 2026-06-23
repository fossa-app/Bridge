namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyLicenseClient(transport: IHttpTransport) =
    member _.getLicenseAsync
        (cancellationToken: CancellationToken)
        : Task<ClientResult<LicenseResponseModel<CompanyEntitlementsModel>>> =
        let endpointPath, securityRequirement = Endpoints.CompanyLicense

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<LicenseResponseModel<CompanyEntitlementsModel>>(
            endpointUrl,
            endpointSecurity,
            cancellationToken
        )

    member _.createLicenseAsync(model: string, cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.CompanyLicense

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<string>(endpointUrl, endpointSecurity, model, cancellationToken)

    interface ICompanyLicenseClient with
        member this.getLicenseAsync(cancellationToken) =
            this.getLicenseAsync (cancellationToken)

        member this.createLicenseAsync(model, cancellationToken) =
            this.createLicenseAsync (model, cancellationToken)
