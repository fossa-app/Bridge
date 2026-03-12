namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type DepartmentClient(transport: IHttpTransport) =
    let buildUrl (queryParams: DepartmentQueryRequestModel) =
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
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.Departments ] parameters

    member _.GetDepartmentsAsync
        (query: DepartmentQueryRequestModel, cancellationToken: CancellationToken)
        : Task<PagingResponseModel<DepartmentRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<DepartmentRetrievalModel>>(buildUrl query, cancellationToken)

    member _.GetDepartmentAsync(id: int64, cancellationToken: CancellationToken) : Task<DepartmentRetrievalModel> =
        transport.GetAsync<DepartmentRetrievalModel>(
            composeRelativeUrl [ Endpoints.Departments; id ] [],
            cancellationToken
        )

    member _.CreateDepartmentAsync(model: DepartmentModificationModel, cancellationToken: CancellationToken) : Task =
        transport.PostAsync<DepartmentModificationModel>(
            composeRelativeUrl [ Endpoints.Departments ] [],
            model,
            cancellationToken
        )

    member _.UpdateDepartmentAsync
        (id: int64, model: DepartmentModificationModel, cancellationToken: CancellationToken)
        : Task =
        transport.PutAsync<DepartmentModificationModel>(
            composeRelativeUrl [ Endpoints.Departments; id ] [],
            model,
            cancellationToken
        )

    member _.DeleteDepartmentAsync(id: int64, cancellationToken: CancellationToken) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.Departments; id ] [], cancellationToken)

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
