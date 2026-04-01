namespace Fossa.Bridge.Services


open System.Threading
open System.Threading.Tasks

type IHttpTransport =
    abstract GetAsync<'TResponse> :
        endpointUrl: string * endpointSecurity: EndpointSecurity * cancellationToken: CancellationToken ->
            Task<'TResponse>

    abstract PostAsync<'TRequest> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task

    abstract PutAsync<'TRequest> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task

    abstract PatchAsync<'TRequest> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task

    abstract DeleteAsync:
        endpointUrl: string * endpointSecurity: EndpointSecurity * cancellationToken: CancellationToken -> Task
