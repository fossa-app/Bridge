namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type BranchClient(transport: IHttpTransport) =
    let buildUrl (queryParams: BranchQueryRequestModel) =
        let parameters =
            [ match Option.ofObj queryParams.Id with
              | Some ids when ids.Count > 0 ->
                  for id in ids do
                      yield "Id", (id: UrlPart)
              | _ -> ()
              if not (String.IsNullOrEmpty(queryParams.Search)) then
                  yield "Search", (queryParams.Search: UrlPart)
              if queryParams.PageNumber.HasValue then
                  yield "PageNumber", (queryParams.PageNumber.Value: UrlPart)
              if queryParams.PageSize.HasValue then
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.BasePath; Endpoints.Branches ] parameters

    member _.GetBranchesAsync(query: BranchQueryRequestModel) : Task<PagingResponseModel<BranchRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<BranchRetrievalModel>>(buildUrl query)

    member _.GetBranchAsync(id: int64) : Task<BranchRetrievalModel> =
        transport.GetAsync<BranchRetrievalModel>(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Branches; id ] [])

    member _.CreateBranchAsync(model: BranchModificationModel) : Task<BranchRetrievalModel> =
        transport.PostAsync<BranchModificationModel, BranchRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Branches ] [],
            model
        )

    member _.UpdateBranchAsync(id: int64, model: BranchModificationModel) : Task<BranchRetrievalModel> =
        transport.PutAsync<BranchModificationModel, BranchRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Branches; id ] [],
            model
        )

    member _.DeleteBranchAsync(id: int64) : Task<unit> =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Branches; id ] [])

    interface IBranchClient with
        member this.GetBranchesAsync(query) = this.GetBranchesAsync(query)
        member this.GetBranchAsync(id) = this.GetBranchAsync(id)
        member this.CreateBranchAsync(model) = this.CreateBranchAsync(model)
        member this.UpdateBranchAsync(id, model) = this.UpdateBranchAsync(id, model)
        member this.DeleteBranchAsync(id) = this.DeleteBranchAsync(id)
