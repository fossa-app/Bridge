namespace Fossa.Bridge.Services.Clients

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanySettingsClient =
    abstract GetCompanySettingsAsync: cancellationToken: CancellationToken -> Task<CompanySettingsRetrievalModel>

    abstract CreateCompanySettingsAsync:
        model: CompanySettingsModificationModel * cancellationToken: CancellationToken -> Task

    abstract UpdateCompanySettingsAsync:
        model: CompanySettingsModificationModel * cancellationToken: CancellationToken -> Task

    abstract DeleteCompanySettingsAsync: cancellationToken: CancellationToken -> Task
