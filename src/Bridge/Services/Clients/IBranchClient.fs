namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IBranchClient =
    abstract GetBranchesAsync:
        query: BranchQueryRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<BranchRetrievalModel>>>

    abstract GetBranchAsync:
        id: int64 * cancellationToken: CancellationToken -> Task<ClientResult<BranchRetrievalModel>>

    abstract CreateBranchAsync:
        model: BranchModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract UpdateBranchAsync:
        id: int64 * model: BranchModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract DeleteBranchAsync: id: int64 * cancellationToken: CancellationToken -> Task<ClientUnitResult>
