namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type IEmployeeClient =
    abstract getEmployeesAsync:
        query: EmployeeQueryRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<EmployeeRetrievalModel>>>

    abstract getEmployeesPagingAsync:
        query: EmployeePagingRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<EmployeeRetrievalModel>>>

    abstract getEmployeeAsync:
        id: int64 * cancellationToken: CancellationToken -> Task<ClientResult<EmployeeRetrievalModel>>

    abstract getCurrentEmployeeAsync: cancellationToken: CancellationToken -> Task<ClientResult<EmployeeRetrievalModel>>

    abstract createEmployeeAsync:
        model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract updateEmployeeAsync:
        id: int64 * model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract updateCurrentEmployeeAsync:
        model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract manageEmployeeAsync:
        id: int64 * model: EmployeeManagementModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract deleteEmployeeAsync: id: int64 * cancellationToken: CancellationToken -> Task<ClientUnitResult>
    abstract deleteCurrentEmployeeAsync: cancellationToken: CancellationToken -> Task<ClientUnitResult>
