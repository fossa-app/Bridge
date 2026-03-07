namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type IdentityClient(transport: IHttpTransport) =
    member _.GetClientAsync(origin: string) : Task<IdentityClientRetrievalModel> =
        transport.GetAsync<IdentityClientRetrievalModel>(
            composeRelativeUrl [ Endpoints.Client ] [ "origin", (origin: UrlPart) ]
        )

    interface IIdentityClient with
        member this.GetClientAsync(origin) = this.GetClientAsync(origin)
