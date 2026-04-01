namespace Fossa.Bridge.Services

open System.Threading
open System.Threading.Tasks

type IAccessTokenProvider =
    abstract GetTokenAsync: cancellationToken: CancellationToken -> Task<string>
