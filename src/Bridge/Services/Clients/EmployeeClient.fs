namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type EmployeeClient(transport: IHttpTransport) =
    let buildUrl (queryParams: EmployeeQueryRequestModel) =
        let parameters =
            [ match Option.ofObj queryParams.id with
              | Some ids when not (Seq.isEmpty ids) ->
                  for id in ids do
                      yield "id", (id: UrlPart)
              | _ -> ()
              match Option.ofObj queryParams.search with
              | Some search when not (String.IsNullOrEmpty(search)) -> yield "search", (search: UrlPart)
              | _ -> ()
              if queryParams.pageNumber.HasValue then
                  yield "pageNumber", (queryParams.pageNumber.Value: UrlPart)
              if queryParams.pageSize.HasValue then
                  yield "pageSize", (queryParams.pageSize.Value: UrlPart)
              if queryParams.reportsToId.HasValue then
                  yield "reportsToId", (queryParams.reportsToId.Value: UrlPart)
              if queryParams.topLevelOnly.HasValue then
                  yield "topLevelOnly", (queryParams.topLevelOnly.Value: UrlPart) ]

        let endpointPath, securityRequirement = Endpoints.Employees
        composeRelativeUrl endpointPath securityRequirement [] parameters

    let buildPagingUrl (queryParams: EmployeePagingRequestModel) =
        let parameters =
            [ match Option.ofObj queryParams.search with
              | Some search when not (String.IsNullOrEmpty(search)) -> yield "search", (search: UrlPart)
              | _ -> ()
              if queryParams.pageNumber.HasValue then
                  yield "pageNumber", (queryParams.pageNumber.Value: UrlPart)
              if queryParams.pageSize.HasValue then
                  yield "pageSize", (queryParams.pageSize.Value: UrlPart) ]

        let endpointPath, securityRequirement = Endpoints.Employees
        composeRelativeUrl endpointPath securityRequirement [] parameters

    member _.getEmployeesAsync
        (query: EmployeeQueryRequestModel, cancellationToken: CancellationToken)
        : Task<ClientResult<PagingResponseModel<EmployeeRetrievalModel>>> =
        let endpointUrl, endpointSecurity = buildUrl query

        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(
            endpointUrl,
            endpointSecurity,
            cancellationToken
        )

    member _.getEmployeesPagingAsync
        (query: EmployeePagingRequestModel, cancellationToken: CancellationToken)
        : Task<ClientResult<PagingResponseModel<EmployeeRetrievalModel>>> =
        let endpointUrl, endpointSecurity = buildPagingUrl query

        transport.GetAsync<PagingResponseModel<EmployeeRetrievalModel>>(
            endpointUrl,
            endpointSecurity,
            cancellationToken
        )

    member _.getEmployeeAsync
        (id: int64, cancellationToken: CancellationToken)
        : Task<ClientResult<EmployeeRetrievalModel>> =
        let endpointPath, securityRequirement = Endpoints.Employees

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.GetAsync<EmployeeRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.getCurrentEmployeeAsync
        (cancellationToken: CancellationToken)
        : Task<ClientResult<EmployeeRetrievalModel>> =
        let endpointPath, securityRequirement = Endpoints.Employee

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.GetAsync<EmployeeRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.createEmployeeAsync
        (model: EmployeeModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Employee

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<EmployeeModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.updateEmployeeAsync
        (id: int64, model: EmployeeModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Employee

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.PutAsync<EmployeeModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.updateCurrentEmployeeAsync
        (model: EmployeeModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Employee

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PutAsync<EmployeeModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.manageEmployeeAsync
        (id: int64, model: EmployeeManagementModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Employees

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.PutAsync<EmployeeManagementModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.deleteEmployeeAsync(id: int64, cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Employee

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    member _.deleteCurrentEmployeeAsync(cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Employee

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface IEmployeeClient with
        member this.getEmployeesAsync(query, cancellationToken) =
            this.getEmployeesAsync (query, cancellationToken)

        member this.getEmployeesPagingAsync(query, cancellationToken) =
            this.getEmployeesPagingAsync (query, cancellationToken)

        member this.getEmployeeAsync(id, cancellationToken) =
            this.getEmployeeAsync (id, cancellationToken)

        member this.getCurrentEmployeeAsync(cancellationToken) =
            this.getCurrentEmployeeAsync (cancellationToken)

        member this.createEmployeeAsync(model, cancellationToken) =
            this.createEmployeeAsync (model, cancellationToken)

        member this.updateEmployeeAsync(id, model, cancellationToken) =
            this.updateEmployeeAsync (id, model, cancellationToken)

        member this.updateCurrentEmployeeAsync(model, cancellationToken) =
            this.updateCurrentEmployeeAsync (model, cancellationToken)

        member this.manageEmployeeAsync(id, model, cancellationToken) =
            this.manageEmployeeAsync (id, model, cancellationToken)

        member this.deleteEmployeeAsync(id, cancellationToken) =
            this.deleteEmployeeAsync (id, cancellationToken)

        member this.deleteCurrentEmployeeAsync(cancellationToken) =
            this.deleteCurrentEmployeeAsync (cancellationToken)
