namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services
open System

type EmployeeClient(transport: IHttpTransport) =
    let buildUrl (queryParams: EmployeeQueryRequestModel) =
        let mutable url = Endpoints.BasePath + "/" + Endpoints.Employees + "?"

        if queryParams.Id.Count > 0 then
            let ids = queryParams.Id |> Seq.map (fun id -> $"Id={id}") |> String.concat "&"
            url <- url + ids + "&"

        if not (String.IsNullOrEmpty(queryParams.Search)) then
            url <- url + $"Search={Uri.EscapeDataString(queryParams.Search)}&"

        if queryParams.PageNumber.HasValue then
            url <- url + $"PageNumber={queryParams.PageNumber.Value}&"

        if queryParams.PageSize.HasValue then
            url <- url + $"PageSize={queryParams.PageSize.Value}&"

        if queryParams.ReportsToId.HasValue then
            url <- url + $"ReportsToId={queryParams.ReportsToId.Value}&"

        if queryParams.TopLevelOnly.HasValue then
            url <- url + $"TopLevelOnly={queryParams.TopLevelOnly.Value}&"

        url.TrimEnd('&')

    let buildPagingUrl (queryParams: EmployeePagingRequestModel) =
        let mutable url = Endpoints.BasePath + "/" + Endpoints.Employees + "?"

        if not (String.IsNullOrEmpty(queryParams.Search)) then
            url <- url + $"Search={Uri.EscapeDataString(queryParams.Search)}&"

        if queryParams.PageNumber.HasValue then
            url <- url + $"PageNumber={queryParams.PageNumber.Value}&"

        if queryParams.PageSize.HasValue then
            url <- url + $"PageSize={queryParams.PageSize.Value}&"

        url.TrimEnd('&')

    member _.GetEmployeesAsync(query: EmployeeQueryRequestModel) : Task<PagingResponseModel<EmployeeRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(buildUrl query)

    member _.GetEmployeesPagingAsync
        (query: EmployeePagingRequestModel)
        : Task<PagingResponseModel<EmployeeRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(buildPagingUrl query)

    member _.GetEmployeeAsync(id: int64) : Task<EmployeeRetrievalModel> =
        transport.GetAsync<EmployeeRetrievalModel>(Endpoints.BasePath + "/" + Endpoints.Employee + $"/{id}")

    member _.CreateEmployeeAsync(model: EmployeeModificationModel) : Task<EmployeeRetrievalModel> =
        transport.PostAsync<EmployeeModificationModel, EmployeeRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.Employee,
            model
        )

    member _.UpdateEmployeeAsync(id: int64, model: EmployeeModificationModel) : Task<EmployeeRetrievalModel> =
        transport.PutAsync<EmployeeModificationModel, EmployeeRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.Employee + $"/{id}",
            model
        )

    member _.DeleteEmployeeAsync(id: int64) : Task<unit> =
        transport.DeleteAsync(Endpoints.BasePath + "/" + Endpoints.Employee + $"/{id}")
