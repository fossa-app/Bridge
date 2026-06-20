namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type DepartmentClient(transport: IHttpTransport) =
    let buildUrl (queryParams: DepartmentQueryRequestModel) =
        let parameters =
            [ match Option.ofObj queryParams.id with
              | Some ids when not (Seq.isEmpty ids) ->
                  for id in ids do
                      yield "id", (id: UrlPart)
              | _ -> ()
              if not (String.IsNullOrEmpty(queryParams.search)) then
                  yield "search", (queryParams.search: UrlPart)
              if queryParams.pageNumber.HasValue then
                  yield "pageNumber", (queryParams.pageNumber.Value: UrlPart)
              if queryParams.pageSize.HasValue then
                  yield "pageSize", (queryParams.pageSize.Value: UrlPart) ]

        let endpointPath, securityRequirement = Endpoints.Departments
        composeRelativeUrl endpointPath securityRequirement [] parameters

    member _.GetDepartmentsAsync
        (query: DepartmentQueryRequestModel, cancellationToken: CancellationToken)
        : Task<ClientResult<PagingResponseModel<DepartmentRetrievalModel>>> =
        let endpointUrl, endpointSecurity = buildUrl query

        transport.GetAsync<PagingResponseModel<DepartmentRetrievalModel>>(
            endpointUrl,
            endpointSecurity,
            cancellationToken
        )

    member _.GetDepartmentAsync
        (id: int64, cancellationToken: CancellationToken)
        : Task<ClientResult<DepartmentRetrievalModel>> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.GetAsync<DepartmentRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.CreateDepartmentAsync
        (model: DepartmentModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<DepartmentModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.UpdateDepartmentAsync
        (id: int64, model: DepartmentModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.PutAsync<DepartmentModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.DeleteDepartmentAsync(id: int64, cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface IDepartmentClient with
        member this.GetDepartmentsAsync(query, cancellationToken) =
            this.GetDepartmentsAsync(query, cancellationToken)

        member this.GetDepartmentAsync(id, cancellationToken) =
            this.GetDepartmentAsync(id, cancellationToken)

        member this.CreateDepartmentAsync(model, cancellationToken) =
            this.CreateDepartmentAsync(model, cancellationToken)

        member this.UpdateDepartmentAsync(id, model, cancellationToken) =
            this.UpdateDepartmentAsync(id, model, cancellationToken)

        member this.DeleteDepartmentAsync(id, cancellationToken) =
            this.DeleteDepartmentAsync(id, cancellationToken)
