namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync
        (cancellationToken: CancellationToken)
        : Task<LicenseResponseModel<CompanyEntitlementsModel>> =
        let endpointPath, securityRequirement = Endpoints.CompanyLicense

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<LicenseResponseModel<CompanyEntitlementsModel>>(
            endpointUrl,
            endpointSecurity,
            cancellationToken
        )

    member _.CreateLicenseAsync(model: string, cancellationToken: CancellationToken) : Task =
        let endpointPath, securityRequirement = Endpoints.CompanyLicense

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<string>(endpointUrl, endpointSecurity, model, cancellationToken)

    interface ICompanyLicenseClient with
        member this.GetLicenseAsync(cancellationToken) = this.GetLicenseAsync(cancellationToken)

        member this.CreateLicenseAsync(model, cancellationToken) =
            this.CreateLicenseAsync(model, cancellationToken)
