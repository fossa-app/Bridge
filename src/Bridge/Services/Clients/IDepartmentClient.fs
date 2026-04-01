namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IDepartmentClient =
    abstract GetDepartmentsAsync:
        query: DepartmentQueryRequestModel * cancellationToken: CancellationToken ->
            Task<PagingResponseModel<DepartmentRetrievalModel>>

    abstract GetDepartmentAsync: id: int64 * cancellationToken: CancellationToken -> Task<DepartmentRetrievalModel>
    abstract CreateDepartmentAsync: model: DepartmentModificationModel * cancellationToken: CancellationToken -> Task

    abstract UpdateDepartmentAsync:
        id: int64 * model: DepartmentModificationModel * cancellationToken: CancellationToken -> Task

    abstract DeleteDepartmentAsync: id: int64 * cancellationToken: CancellationToken -> Task
