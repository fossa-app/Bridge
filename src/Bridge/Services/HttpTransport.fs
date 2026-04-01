namespace Fossa.Bridge.Services


open System.Threading
open System.Threading.Tasks
open Fable.Core


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

    interface IHttpTransport with
        member _.GetAsync<'TResponse>
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

                    let! stringResponse = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return serializer.Deserialize<'TResponse>(stringResponse)
                }

            AsyncHelpers.startAsTaskGeneric (computation, cancellationToken)

        member _.PostAsync<'TRequest>
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

                    let! _ = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return ()
                }

            AsyncHelpers.startAsTaskUnit (computation, cancellationToken)

        member _.PutAsync<'TRequest>
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

                    let! _ = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return ()
                }

            AsyncHelpers.startAsTaskUnit (computation, cancellationToken)

        member _.PatchAsync<'TRequest>
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

                    let! _ = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return ()
                }

            AsyncHelpers.startAsTaskUnit (computation, cancellationToken)

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

                    let! _ = sender.SendAsync(req, cancellationToken) |> AsyncHelpers.awaitTask
                    return ()
                }

            AsyncHelpers.startAsTaskUnit (computation, cancellationToken)
