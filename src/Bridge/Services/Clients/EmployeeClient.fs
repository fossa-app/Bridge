namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type EmployeeClient(transport: IHttpTransport) =
    let buildUrl (queryParams: EmployeeQueryRequestModel) =
        let parameters =
            [ if queryParams.Id.Count > 0 then
                  for id in queryParams.Id do
                      yield "Id", (id: UrlPart)
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

    member _.DeleteEmployeeAsync(id: int64) : Task<unit> =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Employee; id ] [])
