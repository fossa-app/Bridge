namespace Fossa.Bridge.Services.Clients

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ISystemLicenseClient =
    abstract GetLicenseAsync:
        cancellationToken: CancellationToken -> Task<LicenseResponseModel<SystemEntitlementsModel>>
