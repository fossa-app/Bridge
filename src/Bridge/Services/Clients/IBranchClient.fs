namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type IBranchClient =
    abstract getBranchesAsync:
        query: BranchQueryRequestModel * cancellationToken: CancellationToken ->
            Task<ClientResult<PagingResponseModel<BranchRetrievalModel>>>

    abstract getBranchAsync:
        id: int64 * cancellationToken: CancellationToken -> Task<ClientResult<BranchRetrievalModel>>

    abstract createBranchAsync:
        model: BranchModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract updateBranchAsync:
        id: int64 * model: BranchModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract deleteBranchAsync: id: int64 * cancellationToken: CancellationToken -> Task<ClientUnitResult>
