namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanySettingsClient(transport: IHttpTransport) =
    member _.GetCompanySettingsAsync() : Task<CompanySettingsRetrievalModel> =
        transport.GetAsync<CompanySettingsRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] []
        )

    member _.CreateCompanySettingsAsync(model: CompanySettingsModificationModel) : Task =
        transport.PostAsync<CompanySettingsModificationModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] [],
            model
        )

    member _.UpdateCompanySettingsAsync(model: CompanySettingsModificationModel) : Task =
        transport.PutAsync<CompanySettingsModificationModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] [],
            model
        )

    member _.DeleteCompanySettingsAsync() : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] [])

    interface ICompanySettingsClient with
        member this.GetCompanySettingsAsync() = this.GetCompanySettingsAsync()
        member this.CreateCompanySettingsAsync(model) = this.CreateCompanySettingsAsync(model)
        member this.UpdateCompanySettingsAsync(model) = this.UpdateCompanySettingsAsync(model)
        member this.DeleteCompanySettingsAsync() = this.DeleteCompanySettingsAsync()
