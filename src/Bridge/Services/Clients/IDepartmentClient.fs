namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type IDepartmentClient =
    abstract GetDepartmentsAsync:
        query: DepartmentQueryRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<DepartmentRetrievalModel>>>

    abstract GetDepartmentAsync:
        id: int64 * cancellationToken: CancellationToken -> Task<ClientResult<DepartmentRetrievalModel>>

    abstract CreateDepartmentAsync:
        model: DepartmentModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract UpdateDepartmentAsync:
        id: int64 * model: DepartmentModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract DeleteDepartmentAsync: id: int64 * cancellationToken: CancellationToken -> Task<ClientUnitResult>
