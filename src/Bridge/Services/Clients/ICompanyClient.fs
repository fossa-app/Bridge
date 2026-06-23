namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type ICompanyClient =
    abstract getCompanyAsync: cancellationToken: CancellationToken -> Task<ClientResult<CompanyRetrievalModel>>

    abstract createCompanyAsync:
        model: CompanyModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract updateCompanyAsync:
        model: CompanyModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract deleteCompanyAsync: cancellationToken: CancellationToken -> Task<ClientUnitResult>
