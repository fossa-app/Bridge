namespace Fossa.Bridge.Services


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type IHttpTransport =
    abstract GetAsync<'TResponse when 'TResponse: not null> :
        endpointUrl: string * endpointSecurity: EndpointSecurity * cancellationToken: CancellationToken ->
            Task<ClientResult<'TResponse>>

    abstract PostAsync<'TRequest when 'TRequest: not null> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task<ClientUnitResult>

    abstract PutAsync<'TRequest when 'TRequest: not null> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task<ClientUnitResult>

    abstract PatchAsync<'TRequest when 'TRequest: not null> :
        endpointUrl: string *
        endpointSecurity: EndpointSecurity *
        request: 'TRequest *
        cancellationToken: CancellationToken ->
            Task<ClientUnitResult>

    abstract DeleteAsync:
        endpointUrl: string * endpointSecurity: EndpointSecurity * cancellationToken: CancellationToken ->
            Task<ClientUnitResult>
