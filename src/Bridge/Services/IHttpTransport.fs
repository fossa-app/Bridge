namespace Fossa.Bridge.Services

open System.Threading.Tasks

type IHttpTransport =
    abstract GetAsync<'TResponse> : requestUri: string -> Task<'TResponse>
    abstract PostAsync<'TRequest> : requestUri: string * request: 'TRequest -> Task
    abstract PutAsync<'TRequest> : requestUri: string * request: 'TRequest -> Task
    abstract PatchAsync<'TRequest> : requestUri: string * request: 'TRequest -> Task
    abstract DeleteAsync: requestUri: string -> Task
