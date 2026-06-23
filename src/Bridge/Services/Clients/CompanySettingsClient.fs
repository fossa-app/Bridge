namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanySettingsClient(transport: IHttpTransport) =
    member _.getCompanySettingsAsync
        (cancellationToken: CancellationToken)
        : Task<ClientResult<CompanySettingsRetrievalModel>> =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<CompanySettingsRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.createCompanySettingsAsync
        (model: CompanySettingsModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<CompanySettingsModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.updateCompanySettingsAsync
        (model: CompanySettingsModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PutAsync<CompanySettingsModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.deleteCompanySettingsAsync(cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface ICompanySettingsClient with
        member this.getCompanySettingsAsync(cancellationToken) =
            this.getCompanySettingsAsync (cancellationToken)

        member this.createCompanySettingsAsync(model, cancellationToken) =
            this.createCompanySettingsAsync (model, cancellationToken)

        member this.updateCompanySettingsAsync(model, cancellationToken) =
            this.updateCompanySettingsAsync (model, cancellationToken)

        member this.deleteCompanySettingsAsync(cancellationToken) =
            this.deleteCompanySettingsAsync (cancellationToken)
