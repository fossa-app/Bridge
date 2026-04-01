namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyClient(transport: IHttpTransport) =
    member _.GetCompanyAsync(cancellationToken: CancellationToken) : Task<CompanyRetrievalModel> =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<CompanyRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.CreateCompanyAsync(model: CompanyModificationModel, cancellationToken: CancellationToken) : Task =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<CompanyModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.UpdateCompanyAsync(model: CompanyModificationModel, cancellationToken: CancellationToken) : Task =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PutAsync<CompanyModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.DeleteCompanyAsync(cancellationToken: CancellationToken) : Task =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface ICompanyClient with
        member this.GetCompanyAsync(cancellationToken) = this.GetCompanyAsync(cancellationToken)

        member this.CreateCompanyAsync(model, cancellationToken) =
            this.CreateCompanyAsync(model, cancellationToken)

        member this.UpdateCompanyAsync(model, cancellationToken) =
            this.UpdateCompanyAsync(model, cancellationToken)

        member this.DeleteCompanyAsync(cancellationToken) =
            this.DeleteCompanyAsync(cancellationToken)
