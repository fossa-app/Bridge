namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type IIdentityClient =
    abstract getClientAsync:
        origin: string * cancellationToken: CancellationToken -> Task<ClientResult<IdentityClientRetrievalModel>>
