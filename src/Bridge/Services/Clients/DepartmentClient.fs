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
              match Option.ofObj queryParams.search with
              | Some search when not (String.IsNullOrEmpty(search)) -> yield "search", (search: UrlPart)
              | _ -> ()
              if queryParams.pageNumber.HasValue then
                  yield "pageNumber", (queryParams.pageNumber.Value: UrlPart)
              if queryParams.pageSize.HasValue then
                  yield "pageSize", (queryParams.pageSize.Value: UrlPart) ]

        let endpointPath, securityRequirement = Endpoints.Departments
        composeRelativeUrl endpointPath securityRequirement [] parameters

    member _.getDepartmentsAsync
        (query: DepartmentQueryRequestModel, cancellationToken: CancellationToken)
        : Task<ClientResult<PagingResponseModel<DepartmentRetrievalModel>>> =
        let endpointUrl, endpointSecurity = buildUrl query

        transport.GetAsync<PagingResponseModel<DepartmentRetrievalModel>>(
            endpointUrl,
            endpointSecurity,
            cancellationToken
        )

    member _.getDepartmentAsync
        (id: int64, cancellationToken: CancellationToken)
        : Task<ClientResult<DepartmentRetrievalModel>> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.GetAsync<DepartmentRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    member _.createDepartmentAsync
        (model: DepartmentModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] []

        transport.PostAsync<DepartmentModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.updateDepartmentAsync
        (id: int64, model: DepartmentModificationModel, cancellationToken: CancellationToken)
        : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.PutAsync<DepartmentModificationModel>(endpointUrl, endpointSecurity, model, cancellationToken)

    member _.deleteDepartmentAsync(id: int64, cancellationToken: CancellationToken) : Task<ClientUnitResult> =
        let endpointPath, securityRequirement = Endpoints.Departments

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [ UrlPart(string id) ] []

        transport.DeleteAsync(endpointUrl, endpointSecurity, cancellationToken)

    interface IDepartmentClient with
        member this.getDepartmentsAsync(query, cancellationToken) =
            this.getDepartmentsAsync (query, cancellationToken)

        member this.getDepartmentAsync(id, cancellationToken) =
            this.getDepartmentAsync (id, cancellationToken)

        member this.createDepartmentAsync(model, cancellationToken) =
            this.createDepartmentAsync (model, cancellationToken)

        member this.updateDepartmentAsync(id, model, cancellationToken) =
            this.updateDepartmentAsync (id, model, cancellationToken)

        member this.deleteDepartmentAsync(id, cancellationToken) =
            this.deleteDepartmentAsync (id, cancellationToken)
