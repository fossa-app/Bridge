namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type IdentityClient(transport: IHttpTransport) =
    member _.GetClientAsync(origin: string) : Task<IdentityClientRetrievalModel> =
        transport.GetAsync<IdentityClientRetrievalModel>(
            composeRelativeUrl [ Endpoints.BasePath; Endpoints.Client ] [ "origin", (origin: UrlPart) ]
        )
