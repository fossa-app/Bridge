namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type IEmployeeClient =
    abstract GetEmployeesAsync:
        query: EmployeeQueryRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<EmployeeRetrievalModel>>>

    abstract GetEmployeesPagingAsync:
        query: EmployeePagingRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<EmployeeRetrievalModel>>>

    abstract GetEmployeeAsync:
        id: int64 * cancellationToken: CancellationToken -> Task<ClientResult<EmployeeRetrievalModel>>

    abstract GetCurrentEmployeeAsync: cancellationToken: CancellationToken -> Task<ClientResult<EmployeeRetrievalModel>>

    abstract CreateEmployeeAsync:
        model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract UpdateEmployeeAsync:
        id: int64 * model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract UpdateCurrentEmployeeAsync:
        model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract ManageEmployeeAsync:
        id: int64 * model: EmployeeManagementModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract DeleteEmployeeAsync: id: int64 * cancellationToken: CancellationToken -> Task<ClientUnitResult>
    abstract DeleteCurrentEmployeeAsync: cancellationToken: CancellationToken -> Task<ClientUnitResult>
