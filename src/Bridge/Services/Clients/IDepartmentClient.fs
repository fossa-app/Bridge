namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IDepartmentClient =
    abstract member GetDepartmentsAsync:
        query: DepartmentQueryRequestModel -> Task<PagingResponseModel<DepartmentRetrievalModel>>

    abstract member GetDepartmentAsync: id: int64 -> Task<DepartmentRetrievalModel>
    abstract member CreateDepartmentAsync: model: DepartmentModificationModel -> Task<DepartmentRetrievalModel>

    abstract member UpdateDepartmentAsync:
        id: int64 * model: DepartmentModificationModel -> Task<DepartmentRetrievalModel>

    abstract member DeleteDepartmentAsync: id: int64 -> Task
