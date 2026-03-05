namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanySettingsClient =
    abstract member GetCompanySettingsAsync: unit -> Task<CompanySettingsRetrievalModel>

    abstract member CreateCompanySettingsAsync:
        model: CompanySettingsModificationModel -> Task<CompanySettingsRetrievalModel>

    abstract member UpdateCompanySettingsAsync:
        model: CompanySettingsModificationModel -> Task<CompanySettingsRetrievalModel>

    abstract member DeleteCompanySettingsAsync: unit -> Task
