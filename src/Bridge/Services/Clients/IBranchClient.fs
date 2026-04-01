namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IBranchClient =
    abstract GetBranchesAsync:
        query: BranchQueryRequestModel * cancellationToken: CancellationToken ->
            Task<PagingResponseModel<BranchRetrievalModel>>

    abstract GetBranchAsync: id: int64 * cancellationToken: CancellationToken -> Task<BranchRetrievalModel>
    abstract CreateBranchAsync: model: BranchModificationModel * cancellationToken: CancellationToken -> Task

    abstract UpdateBranchAsync:
        id: int64 * model: BranchModificationModel * cancellationToken: CancellationToken -> Task

    abstract DeleteBranchAsync: id: int64 * cancellationToken: CancellationToken -> Task
