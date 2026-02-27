namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services

type IdentityClient(transport: IHttpTransport) =
    member _.GetClientAsync(origin: string) : Task<ClientRetrievalModel> =
        transport.GetAsync<ClientRetrievalModel>(Endpoints.BasePath + "/" + Endpoints.Client + $"?origin={origin}")
