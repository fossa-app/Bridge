namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type ICompanyClient =
    abstract GetCompanyAsync: cancellationToken: CancellationToken -> Task<ClientResult<CompanyRetrievalModel>>

    abstract CreateCompanyAsync:
        model: CompanyModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract UpdateCompanyAsync:
        model: CompanyModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract DeleteCompanyAsync: cancellationToken: CancellationToken -> Task<ClientUnitResult>
