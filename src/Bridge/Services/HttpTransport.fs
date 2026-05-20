namespace Fossa.Bridge.Services


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Models.ApiModels.Helpers
open Fossa.Bridge.Services.StatusCodeHelpers


type HttpTransport(sender: IHttpRequestSender, serializer: IJsonSerializer, tokenProvider: IAccessTokenProvider) =
    let getHeaders (security: EndpointSecurity) (cancellationToken: CancellationToken) =
        let defaultHeaders = [ "Content-Type", "application/json" ]

        let computation =
            match security with
            | Anonymous -> async.Return defaultHeaders
            | RequireToken ->
                async {
                    let! token = tokenProvider.GetTokenAsync(cancellationToken) |> AsyncHelpers.awaitTask
                    let headers = ("Authorization", $"Bearer {token}") :: defaultHeaders
                    return headers
                }

        AsyncHelpers.startAsTaskGeneric (computation, cancellationToken)

    let toClientUnitResult (response: HttpResponseMessage) =
        if isStatusCodeSuccess response.StatusCode then
            ClientUnitResultHelpers.success
        else
            response.Content
            |> serializer.Deserialize<ProblemDetailsModel>
            |> ClientUnitResultHelpers.problem

    interface IHttpTransport with
        member _.GetAsync<'TResponse when 'TResponse: not struct and 'TResponse: not null>
            (endpointUrl: string, endpointSecurity: EndpointSecurity, cancellationToken: CancellationToken)
            =
            let computation =
                async {
                    let! headers = getHeaders endpointSecurity cancellationToken |> AsyncHelpers.awaitTask

                    let req: HttpRequestMessage =
                        { Method = HttpMethod.Get
                          Uri = endpointUrl
                          Content = None
                          Headers = headers }

                    let! response = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask

                    if isStatusCodeSuccess response.StatusCode then
                        return
                            response.Content
                            |> serializer.Deserialize<'TResponse>
                            |> ClientResultHelpers.success
                    else
                        return
                            response.Content
                            |> serializer.Deserialize<ProblemDetailsModel>
                            |> ClientResultHelpers.problem
                }

            AsyncHelpers.startAsTaskGeneric (computation, cancellationToken)

        member _.PostAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                endpointSecurity: EndpointSecurity,
                request: 'TRequest,
                cancellationToken: CancellationToken
            ) =
            let computation =
                async {
                    let! headers = getHeaders endpointSecurity cancellationToken |> AsyncHelpers.awaitTask
                    let stringBody = serializer.Serialize(request)

                    let req: HttpRequestMessage =
                        { Method = HttpMethod.Post
                          Uri = endpointUrl
                          Content = Some stringBody
                          Headers = headers }

                    let! response = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return toClientUnitResult response
                }

            AsyncHelpers.startAsTaskGeneric (computation, cancellationToken)

        member _.PutAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                endpointSecurity: EndpointSecurity,
                request: 'TRequest,
                cancellationToken: CancellationToken
            ) =
            let computation =
                async {
                    let! headers = getHeaders endpointSecurity cancellationToken |> AsyncHelpers.awaitTask
                    let stringBody = serializer.Serialize(request)

                    let req: HttpRequestMessage =
                        { Method = HttpMethod.Put
                          Uri = endpointUrl
                          Content = Some stringBody
                          Headers = headers }

                    let! response = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return toClientUnitResult response
                }

            AsyncHelpers.startAsTaskGeneric (computation, cancellationToken)

        member _.PatchAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                endpointSecurity: EndpointSecurity,
                request: 'TRequest,
                cancellationToken: CancellationToken
            ) =
            let computation =
                async {
                    let! headers = getHeaders endpointSecurity cancellationToken |> AsyncHelpers.awaitTask
                    let stringBody = serializer.Serialize(request)

                    let req: HttpRequestMessage =
                        { Method = HttpMethod.Patch
                          Uri = endpointUrl
                          Content = Some stringBody
                          Headers = headers }

                    let! response = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return toClientUnitResult response
                }

            AsyncHelpers.startAsTaskGeneric (computation, cancellationToken)

        member _.DeleteAsync
            (endpointUrl: string, endpointSecurity: EndpointSecurity, cancellationToken: CancellationToken)
            =
            let computation =
                async {
                    let! headers = getHeaders endpointSecurity cancellationToken |> AsyncHelpers.awaitTask

                    let req: HttpRequestMessage =
                        { Method = HttpMethod.Delete
                          Uri = endpointUrl
                          Content = None
                          Headers = headers }

                    let! response = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return toClientUnitResult response
                }

            AsyncHelpers.startAsTaskGeneric (computation, cancellationToken)
