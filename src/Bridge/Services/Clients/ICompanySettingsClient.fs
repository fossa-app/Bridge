namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type ICompanySettingsClient =
    abstract GetCompanySettingsAsync:
        cancellationToken: CancellationToken -> Task<ClientResult<CompanySettingsRetrievalModel>>

    abstract CreateCompanySettingsAsync:
        model: CompanySettingsModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract UpdateCompanySettingsAsync:
        model: CompanySettingsModificationModel * cancellationToken: CancellationToken -> Task<ClientUnitResult>

    abstract DeleteCompanySettingsAsync: cancellationToken: CancellationToken -> Task<ClientUnitResult>
