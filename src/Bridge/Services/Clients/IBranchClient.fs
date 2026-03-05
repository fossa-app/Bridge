namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IBranchClient =
    abstract member GetBranchesAsync: query: BranchQueryRequestModel -> Task<PagingResponseModel<BranchRetrievalModel>>
    abstract member GetBranchAsync: id: int64 -> Task<BranchRetrievalModel>
    abstract member CreateBranchAsync: model: BranchModificationModel -> Task<BranchRetrievalModel>
    abstract member UpdateBranchAsync: id: int64 * model: BranchModificationModel -> Task<BranchRetrievalModel>
    abstract member DeleteBranchAsync: id: int64 -> Task
