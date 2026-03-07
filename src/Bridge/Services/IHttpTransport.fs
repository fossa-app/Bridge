namespace Fossa.Bridge.Services

open System.Threading
open System.Threading.Tasks

type IHttpTransport =
    abstract GetAsync<'TResponse> : requestUri: string * cancellationToken: CancellationToken -> Task<'TResponse>

    abstract PostAsync<'TRequest> :
        requestUri: string * request: 'TRequest * cancellationToken: CancellationToken -> Task

    abstract PutAsync<'TRequest> :
        requestUri: string * request: 'TRequest * cancellationToken: CancellationToken -> Task

    abstract PatchAsync<'TRequest> :
        requestUri: string * request: 'TRequest * cancellationToken: CancellationToken -> Task

    abstract DeleteAsync: requestUri: string * cancellationToken: CancellationToken -> Task
