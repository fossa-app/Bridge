namespace Fossa.Bridge.Services

open System.Threading.Tasks
open Fossa.Bridge.Models

type IHttpTransport =
    abstract member GetAsync<'TResponse> : string -> Task<'TResponse>
    abstract member PostAsync<'TRequest, 'TResponse> : string * 'TRequest -> Task<'TResponse>
    abstract member PutAsync<'TRequest, 'TResponse> : string * 'TRequest -> Task<'TResponse>
    abstract member DeleteAsync: string -> Task<unit>
