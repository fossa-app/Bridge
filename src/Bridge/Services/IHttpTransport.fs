namespace Fossa.Bridge.Services


open System.Threading
open System.Threading.Tasks

type IHttpTransport =
    abstract GetAsync<'TResponse when 'TResponse: not null> :
        endpointUrl: string * endpointSecurity: EndpointSecurity * cancellationToken: CancellationToken ->
            Task<'TResponse>

    abstract PostAsync<'TRequest when 'TRequest: not null> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task

    abstract PutAsync<'TRequest when 'TRequest: not null> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task

    abstract PatchAsync<'TRequest when 'TRequest: not null> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task

    abstract DeleteAsync:
        endpointUrl: string * endpointSecurity: EndpointSecurity * cancellationToken: CancellationToken -> Task
