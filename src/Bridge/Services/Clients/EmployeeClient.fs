namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type EmployeeClient(transport: IHttpTransport) =
    let buildUrl (queryParams: EmployeeQueryRequestModel) =
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
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart)
              if queryParams.ReportsToId.HasValue then
                  yield "ReportsToId", (queryParams.ReportsToId.Value: UrlPart)
              if queryParams.TopLevelOnly.HasValue then
                  yield "TopLevelOnly", (queryParams.TopLevelOnly.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.BasePath; Endpoints.Employees ] parameters

    let buildPagingUrl (queryParams: EmployeePagingRequestModel) =
        let parameters =
            [ if not (String.IsNullOrEmpty(queryParams.Search)) then
                  yield "Search", (queryParams.Search: UrlPart)
              if queryParams.PageNumber.HasValue then
                  yield "PageNumber", (queryParams.PageNumber.Value: UrlPart)
              if queryParams.PageSize.HasValue then
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.BasePath; Endpoints.Employees ] parameters

    member _.GetEmployeesAsync(query: EmployeeQueryRequestModel) : Task<PagingResponseModel<EmployeeRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(buildUrl query)

    member _.GetEmployeesPagingAsync
        (query: EmployeePagingRequestModel)
        : Task<PagingResponseModel<EmployeeRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(buildPagingUrl query)

    member _.GetEmployeeAsync(id: int64) : Task<EmployeeRetrievalModel> =
        transport.GetAsync<EmployeeRetrievalModel>(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Employee; id ] [])

    member _.CreateEmployeeAsync(model: EmployeeModificationModel) : Task<EmployeeRetrievalModel> =
        transport.PostAsync<EmployeeModificationModel, EmployeeRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Employee ] [],
            model
        )

    member _.UpdateEmployeeAsync(id: int64, model: EmployeeModificationModel) : Task<EmployeeRetrievalModel> =
        transport.PutAsync<EmployeeModificationModel, EmployeeRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Employee; id ] [],
            model
        )

    member _.DeleteEmployeeAsync(id: int64) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Employee; id ] [])

    interface IEmployeeClient with
        member this.GetEmployeesAsync(query) = this.GetEmployeesAsync(query)
        member this.GetEmployeesPagingAsync(query) = this.GetEmployeesPagingAsync(query)
        member this.GetEmployeeAsync(id) = this.GetEmployeeAsync(id)
        member this.CreateEmployeeAsync(model) = this.CreateEmployeeAsync(model)
        member this.UpdateEmployeeAsync(id, model) = this.UpdateEmployeeAsync(id, model)
        member this.DeleteEmployeeAsync(id) = this.DeleteEmployeeAsync(id)
