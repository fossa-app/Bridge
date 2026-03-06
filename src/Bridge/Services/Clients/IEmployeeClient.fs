namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IEmployeeClient =
    abstract GetEmployeesAsync: query: EmployeeQueryRequestModel -> Task<PagingResponseModel<EmployeeRetrievalModel>>

    abstract GetEmployeesPagingAsync:
        query: EmployeePagingRequestModel -> Task<PagingResponseModel<EmployeeRetrievalModel>>

    abstract GetEmployeeAsync: id: int64 -> Task<EmployeeRetrievalModel>
    abstract CreateEmployeeAsync: model: EmployeeModificationModel -> Task
    abstract UpdateEmployeeAsync: id: int64 * model: EmployeeModificationModel -> Task
    abstract ManageEmployeeAsync: id: int64 * model: EmployeeManagementModel -> Task
    abstract DeleteEmployeeAsync: id: int64 -> Task
    abstract DeleteCurrentEmployeeAsync: unit -> Task
