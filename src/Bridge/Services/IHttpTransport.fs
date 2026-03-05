namespace Fossa.Bridge.Services

open System.Threading.Tasks

type IHttpTransport =
    abstract GetAsync<'TResponse> : requestUri: string -> Task<'TResponse>
    abstract PostAsync<'TRequest, 'TResponse> : requestUri: string * request: 'TRequest -> Task<'TResponse>
    abstract PutAsync<'TRequest, 'TResponse> : requestUri: string * request: 'TRequest -> Task<'TResponse>
    abstract DeleteAsync: requestUri: string -> Task
