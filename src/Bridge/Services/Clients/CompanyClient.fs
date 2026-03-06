namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyClient(transport: IHttpTransport) =
    member _.GetCompanyAsync() : Task<CompanyRetrievalModel> =
        transport.GetAsync<CompanyRetrievalModel>(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [])

    member _.CreateCompanyAsync(model: CompanyModificationModel) : Task =
        transport.PostAsync<CompanyModificationModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [],
            model
        )

    member _.UpdateCompanyAsync(model: CompanyModificationModel) : Task =
        transport.PutAsync<CompanyModificationModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [],
            model
        )

    member _.DeleteCompanyAsync() : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Company ] [])

    interface ICompanyClient with
        member this.GetCompanyAsync() = this.GetCompanyAsync()
        member this.CreateCompanyAsync(model) = this.CreateCompanyAsync(model)
        member this.UpdateCompanyAsync(model) = this.UpdateCompanyAsync(model)
        member this.DeleteCompanyAsync() = this.DeleteCompanyAsync()
