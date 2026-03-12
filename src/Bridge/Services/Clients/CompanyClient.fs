namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyClient(transport: IHttpTransport) =
    member _.GetCompanyAsync(cancellationToken: CancellationToken) : Task<CompanyRetrievalModel> =
        transport.GetAsync<CompanyRetrievalModel>(composeRelativeUrl [ Endpoints.Company ] [], cancellationToken)

    member _.CreateCompanyAsync(model: CompanyModificationModel, cancellationToken: CancellationToken) : Task =
        transport.PostAsync<CompanyModificationModel>(
            composeRelativeUrl [ Endpoints.Company ] [],
            model,
            cancellationToken
        )

    member _.UpdateCompanyAsync(model: CompanyModificationModel, cancellationToken: CancellationToken) : Task =
        transport.PutAsync<CompanyModificationModel>(
            composeRelativeUrl [ Endpoints.Company ] [],
            model,
            cancellationToken
        )

    member _.DeleteCompanyAsync(cancellationToken: CancellationToken) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.Company ] [], cancellationToken)

    interface ICompanyClient with
        member this.GetCompanyAsync(cancellationToken) = this.GetCompanyAsync(cancellationToken)

        member this.CreateCompanyAsync(model, cancellationToken) =
            this.CreateCompanyAsync(model, cancellationToken)

        member this.UpdateCompanyAsync(model, cancellationToken) =
            this.UpdateCompanyAsync(model, cancellationToken)

        member this.DeleteCompanyAsync(cancellationToken) =
            this.DeleteCompanyAsync(cancellationToken)
