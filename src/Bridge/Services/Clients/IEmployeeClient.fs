namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IEmployeeClient =
    abstract member GetEmployeesAsync:
        query: EmployeeQueryRequestModel -> Task<PagingResponseModel<EmployeeRetrievalModel>>

    abstract member GetEmployeesPagingAsync:
        query: EmployeePagingRequestModel -> Task<PagingResponseModel<EmployeeRetrievalModel>>

    abstract member GetEmployeeAsync: id: int64 -> Task<EmployeeRetrievalModel>
    abstract member CreateEmployeeAsync: model: EmployeeModificationModel -> Task<EmployeeRetrievalModel>
    abstract member UpdateEmployeeAsync: id: int64 * model: EmployeeModificationModel -> Task<EmployeeRetrievalModel>
    abstract member DeleteEmployeeAsync: id: int64 -> Task
