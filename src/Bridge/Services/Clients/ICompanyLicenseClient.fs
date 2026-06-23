namespace Fossa.Bridge.Services.Clients


open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels

type ICompanyLicenseClient =
    abstract getLicenseAsync:
        cancellationToken: CancellationToken -> Task<ClientResult<LicenseResponseModel<CompanyEntitlementsModel>>>

    abstract createLicenseAsync: model: string * cancellationToken: CancellationToken -> Task<ClientUnitResult>
