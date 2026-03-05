namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanyClient =
    abstract member GetCompanyAsync: unit -> Task<CompanyRetrievalModel>
    abstract member CreateCompanyAsync: model: CompanyModificationModel -> Task<CompanyRetrievalModel>
    abstract member UpdateCompanyAsync: model: CompanyModificationModel -> Task<CompanyRetrievalModel>
    abstract member DeleteCompanyAsync: unit -> Task
