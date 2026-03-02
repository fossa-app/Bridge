namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyClient(transport: IHttpTransport) =
    member _.GetCompanyAsync() : Task<CompanyRetrievalModel> =
        transport.GetAsync<CompanyRetrievalModel>(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [])

    member _.CreateCompanyAsync(model: CompanyModificationModel) : Task<CompanyRetrievalModel> =
        transport.PostAsync<CompanyModificationModel, CompanyRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [],
            model
        )

    member _.UpdateCompanyAsync(model: CompanyModificationModel) : Task<CompanyRetrievalModel> =
        transport.PutAsync<CompanyModificationModel, CompanyRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [],
            model
        )

    member _.DeleteCompanyAsync() : Task<unit> =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [])
