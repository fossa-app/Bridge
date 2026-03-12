namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type IdentityClient(transport: IHttpTransport) =
    member _.GetClientAsync(origin: string, cancellationToken: CancellationToken) : Task<IdentityClientRetrievalModel> =
        transport.GetAsync<IdentityClientRetrievalModel>(
            composeRelativeUrl [ Endpoints.Client ] [ "origin", (origin: UrlPart) ],
            cancellationToken
        )

    interface IIdentityClient with
        member this.GetClientAsync(origin, cancellationToken) =
            this.GetClientAsync(origin, cancellationToken)
