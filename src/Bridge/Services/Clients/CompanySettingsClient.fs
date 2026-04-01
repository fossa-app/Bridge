namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanySettingsClient(transport: IHttpTransport) =
    member _.GetCompanySettingsAsync(cancellationToken: CancellationToken) : Task<CompanySettingsRetrievalModel> =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<CompanySettingsRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.CreateCompanySettingsAsync
        (model: CompanySettingsModificationModel, cancellationToken: CancellationToken)
        : Task =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<CompanySettingsModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.UpdateCompanySettingsAsync
        (model: CompanySettingsModificationModel, cancellationToken: CancellationToken)
        : Task =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PutAsync<CompanySettingsModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.DeleteCompanySettingsAsync(cancellationToken: CancellationToken) : Task =
        let endpointPath, securityRequirement = Endpoints.CompanySettings

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface ICompanySettingsClient with
        member this.GetCompanySettingsAsync(cancellationToken) =
            this.GetCompanySettingsAsync(cancellationToken)

        member this.CreateCompanySettingsAsync(model, cancellationToken) =
            this.CreateCompanySettingsAsync(model, cancellationToken)

        member this.UpdateCompanySettingsAsync(model, cancellationToken) =
            this.UpdateCompanySettingsAsync(model, cancellationToken)

        member this.DeleteCompanySettingsAsync(cancellationToken) =
            this.DeleteCompanySettingsAsync(cancellationToken)
