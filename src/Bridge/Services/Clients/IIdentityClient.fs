namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IIdentityClient =
    abstract GetClientAsync: origin: string -> Task<IdentityClientRetrievalModel>
