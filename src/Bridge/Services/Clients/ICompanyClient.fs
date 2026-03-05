namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanyClient =
    abstract GetCompanyAsync: unit -> Task<CompanyRetrievalModel>
    abstract CreateCompanyAsync: model: CompanyModificationModel -> Task<CompanyRetrievalModel>
    abstract UpdateCompanyAsync: model: CompanyModificationModel -> Task<CompanyRetrievalModel>
    abstract DeleteCompanyAsync: unit -> Task
