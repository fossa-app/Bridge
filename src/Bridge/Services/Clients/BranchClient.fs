namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services
open System

type BranchClient(transport: IHttpTransport) =
    let buildUrl (queryParams: BranchQueryRequestModel) =
        let mutable url = Endpoints.BasePath + "/" + Endpoints.Branches + "?"

        if queryParams.Id.Count > 0 then
            let ids = queryParams.Id |> Seq.map (fun id -> $"Id={id}") |> String.concat "&"
            url <- url + ids + "&"

        if not (String.IsNullOrEmpty(queryParams.Search)) then
            url <- url + $"Search={Uri.EscapeDataString(queryParams.Search)}&"

        if queryParams.PageNumber.HasValue then
            url <- url + $"PageNumber={queryParams.PageNumber.Value}&"

        if queryParams.PageSize.HasValue then
            url <- url + $"PageSize={queryParams.PageSize.Value}&"

        url.TrimEnd('&')

    member _.GetBranchesAsync(query: BranchQueryRequestModel) : Task<PagingResponseModel<BranchRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<BranchRetrievalModel>>(buildUrl query)

    member _.GetBranchAsync(id: int64) : Task<BranchRetrievalModel> =
        transport.GetAsync<BranchRetrievalModel>(Endpoints.BasePath + "/" + Endpoints.Branches + $"/{id}")

    member _.CreateBranchAsync(model: BranchModificationModel) : Task<BranchRetrievalModel> =
        transport.PostAsync<BranchModificationModel, BranchRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.Branches,
            model
        )

    member _.UpdateBranchAsync(id: int64, model: BranchModificationModel) : Task<BranchRetrievalModel> =
        transport.PutAsync<BranchModificationModel, BranchRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.Branches + $"/{id}",
            model
        )

    member _.DeleteBranchAsync(id: int64) : Task<unit> =
        transport.DeleteAsync(Endpoints.BasePath + "/" + Endpoints.Branches + $"/{id}")
