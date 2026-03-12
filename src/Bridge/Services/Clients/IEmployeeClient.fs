namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IEmployeeClient =
    abstract GetEmployeesAsync:
        query: EmployeeQueryRequestModel * cancellationToken: CancellationToken ->
            Task<PagingResponseModel<EmployeeRetrievalModel>>

    abstract GetEmployeesPagingAsync:
        query: EmployeePagingRequestModel * cancellationToken: CancellationToken ->
            Task<PagingResponseModel<EmployeeRetrievalModel>>

    abstract GetEmployeeAsync: id: int64 * cancellationToken: CancellationToken -> Task<EmployeeRetrievalModel>
    abstract GetCurrentEmployeeAsync: cancellationToken: CancellationToken -> Task<EmployeeRetrievalModel>
    abstract CreateEmployeeAsync: model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task

    abstract UpdateEmployeeAsync:
        id: int64 * model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task

    abstract UpdateCurrentEmployeeAsync: model: EmployeeModificationModel * cancellationToken: CancellationToken -> Task

    abstract ManageEmployeeAsync:
        id: int64 * model: EmployeeManagementModel * cancellationToken: CancellationToken -> Task

    abstract DeleteEmployeeAsync: id: int64 * cancellationToken: CancellationToken -> Task
    abstract DeleteCurrentEmployeeAsync: cancellationToken: CancellationToken -> Task
