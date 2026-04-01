namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type IdentityClient(transport: IHttpTransport) =
    member _.GetClientAsync(origin: string, cancellationToken: CancellationToken) : Task<IdentityClientRetrievalModel> =
        let endpointPath, securityRequirement = Endpoints.Client

        let endpointUrl, endpointSecurity =
            composeRelativeUrl endpointPath securityRequirement [] [ "origin", (origin: UrlPart) ]

        transport.GetAsync<IdentityClientRetrievalModel>(endpointUrl, endpointSecurity, cancellationToken)

    interface IIdentityClient with
        member this.GetClientAsync(origin, cancellationToken) =
            this.GetClientAsync(origin, cancellationToken)
