namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open System
open Fossa.Bridge.Services.UrlHelpers

type DepartmentClient(transport: IHttpTransport) =
    let buildUrl (queryParams: DepartmentQueryRequestModel) =
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
                  yield "PageSize", (queryParams.PageSize.Value: UrlPart) ]

        composeRelativeUrl [ Endpoints.BasePath; Endpoints.Departments ] parameters

    member _.GetDepartmentsAsync
        (query: DepartmentQueryRequestModel)
        : Task<PagingResponseModel<DepartmentRetrievalModel>> =
        transport.GetAsync<PagingResponseModel<DepartmentRetrievalModel>>(buildUrl query)

    member _.GetDepartmentAsync(id: int64) : Task<DepartmentRetrievalModel> =
        transport.GetAsync<DepartmentRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Departments; id ] []
        )

    member _.CreateDepartmentAsync(model: DepartmentModificationModel) : Task<DepartmentRetrievalModel> =
        transport.PostAsync<DepartmentModificationModel, DepartmentRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Departments ] [],
            model
        )

    member _.UpdateDepartmentAsync(id: int64, model: DepartmentModificationModel) : Task<DepartmentRetrievalModel> =
        transport.PutAsync<DepartmentModificationModel, DepartmentRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Departments; id ] [],
            model
        )

    member _.DeleteDepartmentAsync(id: int64) : Task =
        transport.DeleteAsync(composeRelativeUrl [ Endpoints.BasePath; Endpoints.Departments; id ] [])

    interface IDepartmentClient with
        member this.GetDepartmentsAsync(query) = this.GetDepartmentsAsync(query)
        member this.GetDepartmentAsync(id) = this.GetDepartmentAsync(id)
        member this.CreateDepartmentAsync(model) = this.CreateDepartmentAsync(model)
        member this.UpdateDepartmentAsync(id, model) = this.UpdateDepartmentAsync(id, model)
        member this.DeleteDepartmentAsync(id) = this.DeleteDepartmentAsync(id)
