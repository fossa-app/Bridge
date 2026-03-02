namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanySettingsClient(transport: IHttpTransport) =
    member _.GetCompanySettingsAsync() : Task<CompanySettingsRetrievalModel> =
        transport.GetAsync<CompanySettingsRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] []
        )

    member _.CreateCompanySettingsAsync(model: CompanySettingsModificationModel) : Task<CompanySettingsRetrievalModel> =
        transport.PostAsync<CompanySettingsModificationModel, CompanySettingsRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] [],
            model
        )

    member _.UpdateCompanySettingsAsync(model: CompanySettingsModificationModel) : Task<CompanySettingsRetrievalModel> =
        transport.PutAsync<CompanySettingsModificationModel, CompanySettingsRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] [],
            model
        )

    member _.DeleteCompanySettingsAsync() : Task<unit> =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.CompanySettings ] [])
