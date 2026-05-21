namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type BranchClient(transport: IHttpTransport) =
    let buildUrl (queryParams: BranchQueryRequestModel) =
        let parameters =
            [ match Option.ofObj queryParams.Id with
              | Some ids when not (Seq.isEmpty ids) ->
                  for id in ids do
                      yield "id", (id: UrlPart)
              | _ -> ()
              if not (String.IsNullOrEmpty(queryParams.Search)) then
                  yield "search", (queryParams.Search: UrlPart)
              if queryParams.PageNumber.HasValue then
                  yield "pageNumber", (queryParams.PageNumber.Value: UrlPart)
              if queryParams.PageSize.HasValue then
                  yield "pageSize", (queryParams.PageSize.Value: UrlPart) ]

        let endpointPath, securityRequirement = Endpoints.Branches
        composeRelativeUrl endpointPath securityRequirement [] parameters

    member _.GetBranchesAsync
        (query: BranchQueryRequestModel, cancellationToken: CancellationToken)
        : Task<ClientResult<PagingResponseModel<BranchRetrievalModel>>> =
        let endpointUrl, endpointSecurity = buildUrl query
        transport.GetAsync<PagingResponseModel<BranchRetrievalModel>>(endpointUrl, endpointSecurity, cancellationToken)

    member _.GetBranchAsync
        (id: int64, cancellationToken: CancellationToken)
        : Task<ClientResult<BranchRetrievalModel>> =
        let endpointPath, securityRequirement = Endpoints.Branches

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.GetAsync<BranchRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.CreateBranchAsync
        (model: BranchModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Branches

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<BranchModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.UpdateBranchAsync
        (id: int64, model: BranchModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Branches

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.PutAsync<BranchModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.DeleteBranchAsync(id: int64, cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Branches

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface IBranchClient with
        member this.GetBranchesAsync(query, cancellationToken) =
            this.GetBranchesAsync(query, cancellationToken)

        member this.GetBranchAsync(id, cancellationToken) =
            this.GetBranchAsync(id, cancellationToken)

        member this.CreateBranchAsync(model, cancellationToken) =
            this.CreateBranchAsync(model, cancellationToken)

        member this.UpdateBranchAsync(id, model, cancellationToken) =
            this.UpdateBranchAsync(id, model, cancellationToken)

        member this.DeleteBranchAsync(id, cancellationToken) =
            this.DeleteBranchAsync(id, cancellationToken)
