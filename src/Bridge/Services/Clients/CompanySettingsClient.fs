namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services

type CompanySettingsClient(transport: IHttpTransport) =
    member _.GetCompanySettingsAsync() : Task<CompanySettingsRetrievalModel> =
        transport.GetAsync<CompanySettingsRetrievalModel>(Endpoints.BasePath + "/" + Endpoints.CompanySettings)

    member _.CreateCompanySettingsAsync(model: CompanySettingsModificationModel) : Task<CompanySettingsRetrievalModel> =
        transport.PostAsync<CompanySettingsModificationModel, CompanySettingsRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.CompanySettings,
            model
        )

    member _.UpdateCompanySettingsAsync(model: CompanySettingsModificationModel) : Task<CompanySettingsRetrievalModel> =
        transport.PutAsync<CompanySettingsModificationModel, CompanySettingsRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.CompanySettings,
            model
        )

    member _.DeleteCompanySettingsAsync() : Task<unit> =
        transport.DeleteAsync(Endpoints.BasePath + "/" + Endpoints.CompanySettings)
