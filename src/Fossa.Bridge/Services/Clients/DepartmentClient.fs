namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services
open System

type DepartmentClient(transport: IHttpTransport) =
    let buildUrl (queryParams: DepartmentQueryRequestModel) =
        let mutable url = Endpoints.BasePath + "/" + Endpoints.Departments + "?"

        if queryParams.Id <> null && queryParams.Id.Count > 0 then
            let ids = queryParams.Id |> Seq.map (fun id -> $"Id={id}") |> String.concat "&"
            url <- url + ids + "&"

        if not (String.IsNullOrEmpty(queryParams.Search)) then
            url <- url + $"Search={Uri.EscapeDataString(queryParams.Search)}&"

        if queryParams.PageNumber.HasValue then
            url <- url + $"PageNumber={queryParams.PageNumber.Value}&"

        if queryParams.PageSize.HasValue then
            url <- url + $"PageSize={queryParams.PageSize.Value}&"

        url.TrimEnd('&')

    member _.GetDepartmentsAsync
        (query: DepartmentQueryRequestModel)
        : Task<PagingResponseModel<DepartmentRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<DepartmentRetrievalModel>>(buildUrl query)

    member _.GetDepartmentAsync(id: int64) : Task<DepartmentRetrievalModel> =
        transport.GetAsync<DepartmentRetrievalModel>(Endpoints.BasePath + "/" + Endpoints.Departments + $"/{id}")

    member _.CreateDepartmentAsync(model: DepartmentModificationModel) : Task<DepartmentRetrievalModel> =
        transport.PostAsync<DepartmentModificationModel, DepartmentRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.Departments,
            model
        )

    member _.UpdateDepartmentAsync(id: int64, model: DepartmentModificationModel) : Task<DepartmentRetrievalModel> =
        transport.PutAsync<DepartmentModificationModel, DepartmentRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.Departments + $"/{id}",
            model
        )

    member _.DeleteDepartmentAsync(id: int64) : Task<unit> =
        transport.DeleteAsync(Endpoints.BasePath + "/" + Endpoints.Departments + $"/{id}")
