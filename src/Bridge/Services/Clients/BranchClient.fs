namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

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
                      yield "Id", (id: UrlPart)
              | _ -> ()
              if not (String.IsNullOrEmpty(queryParams.Search)) then
                  yield "Search", (queryParams.Search: UrlPart)
              if queryParams.PageNumber.HasValue then
                  yield "PageNumber", (queryParams.PageNumber.Value: UrlPart)
              if queryParams.PageSize.HasValue then
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.Branches ] parameters

    member _.GetBranchesAsync
        (query: BranchQueryRequestModel, cancellationToken: CancellationToken)
        : Task<PagingResponseModel<BranchRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<BranchRetrievalModel>>(buildUrl query, cancellationToken)

    member _.GetBranchAsync(id: int64, cancellationToken: CancellationToken) : Task<BranchRetrievalModel> =
        transport.GetAsync<BranchRetrievalModel>(composeRelativeUrl [ Endpoints.Branches; id ] [], cancellationToken)

    member _.CreateBranchAsync(model: BranchModificationModel, cancellationToken: CancellationToken) : Task =
        transport.PostAsync<BranchModificationModel>(
            composeRelativeUrl [ Endpoints.Branches ] [],
            model,
            cancellationToken
        )

    member _.UpdateBranchAsync(id: int64, model: BranchModificationModel, cancellationToken: CancellationToken) : Task =
        transport.PutAsync<BranchModificationModel>(
            composeRelativeUrl [ Endpoints.Branches; id ] [],
            model,
            cancellationToken
        )

    member _.DeleteBranchAsync(id: int64, cancellationToken: CancellationToken) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.Branches; id ] [], cancellationToken)

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
