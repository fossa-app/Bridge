namespace Fossa.Bridge.Services

open System.Threading
open System.Threading.Tasks

type HttpMethod =
    | Get
    | Post
    | Put
    | Patch
    | Delete

type HttpRequestMessage =
    { Method: HttpMethod
      Uri: string
      Content: string option
      Headers: (string * string) list }

type IHttpRequestSender =
    abstract SendAsync: request: HttpRequestMessage * cancellationToken: CancellationToken -> Task<string>
