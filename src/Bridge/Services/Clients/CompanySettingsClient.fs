namespace Fossa.Bridge.Services.Clients

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanySettingsClient(transport: IHttpTransport) =
    member _.GetCompanySettingsAsync(cancellationToken: CancellationToken) : Task<CompanySettingsRetrievalModel> =
        transport.GetAsync<CompanySettingsRetrievalModel>(
            composeRelativeUrl [ Endpoints.CompanySettings ] [],
            cancellationToken
        )

    member _.CreateCompanySettingsAsync
        (model: CompanySettingsModificationModel, cancellationToken: CancellationToken)
        : Task =
        transport.PostAsync<CompanySettingsModificationModel>(
            composeRelativeUrl [ Endpoints.CompanySettings ] [],
            model,
            cancellationToken
        )

    member _.UpdateCompanySettingsAsync
        (model: CompanySettingsModificationModel, cancellationToken: CancellationToken)
        : Task =
        transport.PutAsync<CompanySettingsModificationModel>(
            composeRelativeUrl [ Endpoints.CompanySettings ] [],
            model,
            cancellationToken
        )

    member _.DeleteCompanySettingsAsync(cancellationToken: CancellationToken) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.CompanySettings ] [], cancellationToken)

    interface ICompanySettingsClient with
        member this.GetCompanySettingsAsync(cancellationToken) =
            this.GetCompanySettingsAsync(cancellationToken)

        member this.CreateCompanySettingsAsync(model, cancellationToken) =
            this.CreateCompanySettingsAsync(model, cancellationToken)

        member this.UpdateCompanySettingsAsync(model, cancellationToken) =
            this.UpdateCompanySettingsAsync(model, cancellationToken)

        member this.DeleteCompanySettingsAsync(cancellationToken) =
            this.DeleteCompanySettingsAsync(cancellationToken)
