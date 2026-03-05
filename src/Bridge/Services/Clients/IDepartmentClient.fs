namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IDepartmentClient =
    abstract GetDepartmentsAsync:
        query: DepartmentQueryRequestModel -> Task<PagingResponseModel<DepartmentRetrievalModel>>

    abstract GetDepartmentAsync: id: int64 -> Task<DepartmentRetrievalModel>
    abstract CreateDepartmentAsync: model: DepartmentModificationModel -> Task<DepartmentRetrievalModel>

    abstract UpdateDepartmentAsync: id: int64 * model: DepartmentModificationModel -> Task<DepartmentRetrievalModel>

    abstract DeleteDepartmentAsync: id: int64 -> Task
