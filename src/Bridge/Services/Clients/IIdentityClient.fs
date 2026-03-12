namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IIdentityClient =
    abstract GetClientAsync: origin: string * cancellationToken: CancellationToken -> Task<IdentityClientRetrievalModel>
