namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanySettingsClient =
    abstract GetCompanySettingsAsync: unit -> Task<CompanySettingsRetrievalModel>

    abstract CreateCompanySettingsAsync: model: CompanySettingsModificationModel -> Task

    abstract UpdateCompanySettingsAsync: model: CompanySettingsModificationModel -> Task

    abstract DeleteCompanySettingsAsync: unit -> Task
