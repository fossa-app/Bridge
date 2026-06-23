namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type IDepartmentClient =
    abstract getDepartmentsAsync:
        query: DepartmentQueryRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<DepartmentRetrievalModel>>>

    abstract getDepartmentAsync:
        id: int64 * cancellationToken: CancellationToken -> Task<ClientResult<DepartmentRetrievalModel>>

    abstract createDepartmentAsync:
        model: DepartmentModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract updateDepartmentAsync:
        id: int64 * model: DepartmentModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract deleteDepartmentAsync: id: int64 * cancellationToken: CancellationToken -> Task<ClientUnitResult>
