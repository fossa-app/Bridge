namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services

type IdentityClient(transport: IHttpTransport) =
    member _.GetClientAsync(origin: string) : Task<IdentityClientRetrievalModel> =
        transport.GetAsync<IdentityClientRetrievalModel>(
            Endpoints.BasePath + "/" + Endpoints.Client + $"?origin={origin}"
        )
