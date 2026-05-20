module HttpTransportTests

open System.Threading
open System.Threading.Tasks
open Expecto
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services

type private StubSender(response: HttpResponseMessage) =
    interface IHttpRequestSender with
        member _.SendAsync(_request, _cancellationToken) = Task.FromResult response

type private StubTokenProvider() =
    interface IAccessTokenProvider with
        member _.GetTokenAsync(_cancellationToken) = Task.FromResult "token"

let private serializer = JsonSerializer() :> IJsonSerializer

let private createTransport response =
    HttpTransport(StubSender(response), serializer, StubTokenProvider()) :> IHttpTransport

let private problemJson =
    "{\"type\":\"https://example.com/problems/not-found\",\"title\":\"Not Found\",\"status\":404,\"detail\":\"Missing.\",\"instance\":\"/companies/99\"}"

[<Tests>]
let tests =
    testList
        "HttpTransportTests"
        [ testCase "GetAsync returns success for 2xx response"
          <| fun _ ->
              let transport =
                  createTransport
                      { StatusCode = 200
                        Content = "{\"type\":null,\"title\":\"OK\",\"status\":200,\"detail\":null,\"instance\":null}" }

              let result =
                  transport
                      .GetAsync<ProblemDetailsModel>("/test", Anonymous, CancellationToken.None)
                      .GetAwaiter()
                      .GetResult()

              match result with
              | ClientResult.Success value ->
                  Expect.equal value.Status 200 "Status should deserialize"
                  Expect.equal value.Title "OK" "Title should deserialize"
              | ClientResult.Problem problem -> failtestf "Expected success, got problem %A" problem

          testCase "GetAsync returns problem for non-2xx response"
          <| fun _ ->
              let transport =
                  createTransport
                      { StatusCode = 404
                        Content = problemJson }

              let result =
                  transport
                      .GetAsync<ProblemDetailsModel>("/test", Anonymous, CancellationToken.None)
                      .GetAwaiter()
                      .GetResult()

              match result with
              | ClientResult.Success value -> failtestf "Expected problem, got success %A" value
              | ClientResult.Problem problem ->
                  Expect.equal problem.Status 404 "Status should deserialize"
                  Expect.equal problem.Title "Not Found" "Title should deserialize"

          testCase "PostAsync returns unit success for 2xx response"
          <| fun _ ->
              let transport = createTransport { StatusCode = 204; Content = "" }

              let result =
                  transport
                      .PostAsync<string>("/test", Anonymous, "payload", CancellationToken.None)
                      .GetAwaiter()
                      .GetResult()

              Expect.equal result ClientUnitResult.Success "No-body success should return unit success"

          testCase "PostAsync returns unit problem for non-2xx response"
          <| fun _ ->
              let transport =
                  createTransport
                      { StatusCode = 404
                        Content = problemJson }

              let result =
                  transport
                      .PostAsync<string>("/test", Anonymous, "payload", CancellationToken.None)
                      .GetAwaiter()
                      .GetResult()

              match result with
              | ClientUnitResult.Success -> failtest "Expected problem, got success"
              | ClientUnitResult.Problem problem ->
                  Expect.equal problem.Status 404 "Status should deserialize"
                  Expect.equal problem.Title "Not Found" "Title should deserialize" ]
