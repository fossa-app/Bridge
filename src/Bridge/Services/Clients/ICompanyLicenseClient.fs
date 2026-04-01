namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanyLicenseClient =
    abstract GetLicenseAsync:
        cancellationToken: CancellationToken -> Task<LicenseResponseModel<CompanyEntitlementsModel>>

    abstract CreateLicenseAsync: model: string * cancellationToken: CancellationToken -> Task
