namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyClient(transport: IHttpTransport) =
    member _.getCompanyAsync(cancellationToken: CancellationToken) : Task<ClientResult<CompanyRetrievalModel>> =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<CompanyRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.createCompanyAsync
        (model: CompanyModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<CompanyModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.updateCompanyAsync
        (model: CompanyModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PutAsync<CompanyModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.deleteCompanyAsync(cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Company

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface ICompanyClient with
        member this.getCompanyAsync(cancellationToken) =
            this.getCompanyAsync (cancellationToken)

        member this.createCompanyAsync(model, cancellationToken) =
            this.createCompanyAsync (model, cancellationToken)

        member this.updateCompanyAsync(model, cancellationToken) =
            this.updateCompanyAsync (model, cancellationToken)

        member this.deleteCompanyAsync(cancellationToken) =
            this.deleteCompanyAsync (cancellationToken)
