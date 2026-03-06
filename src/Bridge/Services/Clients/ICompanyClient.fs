namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanyClient =
    abstract GetCompanyAsync: unit -> Task<CompanyRetrievalModel>
    abstract CreateCompanyAsync: model: CompanyModificationModel -> Task
    abstract UpdateCompanyAsync: model: CompanyModificationModel -> Task
    abstract DeleteCompanyAsync: unit -> Task
