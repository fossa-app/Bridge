namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IBranchClient =
    abstract GetBranchesAsync: query: BranchQueryRequestModel -> Task<PagingResponseModel<BranchRetrievalModel>>
    abstract GetBranchAsync: id: int64 -> Task<BranchRetrievalModel>
    abstract CreateBranchAsync: model: BranchModificationModel -> Task<BranchRetrievalModel>
    abstract UpdateBranchAsync: id: int64 * model: BranchModificationModel -> Task<BranchRetrievalModel>
    abstract DeleteBranchAsync: id: int64 -> Task
