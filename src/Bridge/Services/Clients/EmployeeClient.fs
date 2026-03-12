namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type EmployeeClient(transport: IHttpTransport) =
    let buildUrl (queryParams: EmployeeQueryRequestModel) =
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
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart)
              if queryParams.ReportsToId.HasValue then
                  yield "ReportsToId", (queryParams.ReportsToId.Value: UrlPart)
              if queryParams.TopLevelOnly.HasValue then
                  yield "TopLevelOnly", (queryParams.TopLevelOnly.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.Employees ] parameters

    let buildPagingUrl (queryParams: EmployeePagingRequestModel) =
        let parameters =
            [ if not (String.IsNullOrEmpty(queryParams.Search)) then
                  yield "Search", (queryParams.Search: UrlPart)
              if queryParams.PageNumber.HasValue then
                  yield "PageNumber", (queryParams.PageNumber.Value: UrlPart)
              if queryParams.PageSize.HasValue then
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.Employees ] parameters

    member _.GetEmployeesAsync
        (query: EmployeeQueryRequestModel, cancellationToken: CancellationToken)
        : Task<PagingResponseModel<EmployeeRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(buildUrl query, cancellationToken)

    member _.GetEmployeesPagingAsync
        (query: EmployeePagingRequestModel, cancellationToken: CancellationToken)
        : Task<PagingResponseModel<EmployeeRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(buildPagingUrl query, cancellationToken)

    member _.GetEmployeeAsync(id: int64, cancellationToken: CancellationToken) : Task<EmployeeRetrievalModel> =
        transport.GetAsync<EmployeeRetrievalModel>(composeRelativeUrl [ Endpoints.Employees; id ] [], cancellationToken)

    member _.GetCurrentEmployeeAsync(cancellationToken: CancellationToken) : Task<EmployeeRetrievalModel> =
        transport.GetAsync<EmployeeRetrievalModel>(composeRelativeUrl [ Endpoints.Employee ] [], cancellationToken)

    member _.CreateEmployeeAsync(model: EmployeeModificationModel, cancellationToken: CancellationToken) : Task =
        transport.PostAsync<EmployeeModificationModel>(
            composeRelativeUrl [ Endpoints.Employee ] [],
            model,
            cancellationToken
        )

    member _.UpdateEmployeeAsync
        (id: int64, model: EmployeeModificationModel, cancellationToken: CancellationToken)
        : Task =
        transport.PutAsync<EmployeeModificationModel>(
            composeRelativeUrl [ Endpoints.Employee; id ] [],
            model,
            cancellationToken
        )

    member _.UpdateCurrentEmployeeAsync(model: EmployeeModificationModel, cancellationToken: CancellationToken) : Task =
        transport.PutAsync<EmployeeModificationModel>(
            composeRelativeUrl [ Endpoints.Employee ] [],
            model,
            cancellationToken
        )

    member _.ManageEmployeeAsync
        (id: int64, model: EmployeeManagementModel, cancellationToken: CancellationToken)
        : Task =
        transport.PutAsync<EmployeeManagementModel>(
            composeRelativeUrl [ Endpoints.Employees; id ] [],
            model,
            cancellationToken
        )

    member _.DeleteEmployeeAsync(id: int64, cancellationToken: CancellationToken) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.Employee; id ] [], cancellationToken)

    member _.DeleteCurrentEmployeeAsync(cancellationToken: CancellationToken) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.Employee ] [], cancellationToken)

    interface IEmployeeClient with
        member this.GetEmployeesAsync(query, cancellationToken) =
            this.GetEmployeesAsync(query, cancellationToken)

        member this.GetEmployeesPagingAsync(query, cancellationToken) =
            this.GetEmployeesPagingAsync(query, cancellationToken)

        member this.GetEmployeeAsync(id, cancellationToken) =
            this.GetEmployeeAsync(id, cancellationToken)

        member this.GetCurrentEmployeeAsync(cancellationToken) =
            this.GetCurrentEmployeeAsync(cancellationToken)

        member this.CreateEmployeeAsync(model, cancellationToken) =
            this.CreateEmployeeAsync(model, cancellationToken)

        member this.UpdateEmployeeAsync(id, model, cancellationToken) =
            this.UpdateEmployeeAsync(id, model, cancellationToken)

        member this.UpdateCurrentEmployeeAsync(model, cancellationToken) =
            this.UpdateCurrentEmployeeAsync(model, cancellationToken)

        member this.ManageEmployeeAsync(id, model, cancellationToken) =
            this.ManageEmployeeAsync(id, model, cancellationToken)

        member this.DeleteEmployeeAsync(id, cancellationToken) =
            this.DeleteEmployeeAsync(id, cancellationToken)

        member this.DeleteCurrentEmployeeAsync(cancellationToken) =
            this.DeleteCurrentEmployeeAsync(cancellationToken)
