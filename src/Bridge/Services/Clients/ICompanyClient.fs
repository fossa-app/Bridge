namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanyClient =
    abstract GetCompanyAsync: cancellationToken: CancellationToken -> Task<CompanyRetrievalModel>

    abstract CreateCompanyAsync: model: CompanyModificationModel * cancellationToken: CancellationToken -> Task

    abstract UpdateCompanyAsync: model: CompanyModificationModel * cancellationToken: CancellationToken -> Task

    abstract DeleteCompanyAsync: cancellationToken: CancellationToken -> Task
