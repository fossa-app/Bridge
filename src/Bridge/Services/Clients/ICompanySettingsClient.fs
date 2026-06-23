namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type ICompanySettingsClient =
    abstract getCompanySettingsAsync:
        cancellationToken: CancellationToken -> Task<ClientResult<CompanySettingsRetrievalModel>>

    abstract createCompanySettingsAsync:
        model: CompanySettingsModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract updateCompanySettingsAsync:
        model: CompanySettingsModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract deleteCompanySettingsAsync: cancellationToken: CancellationToken -> Task<ClientUnitResult>
