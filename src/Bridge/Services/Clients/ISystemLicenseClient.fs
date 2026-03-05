namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ISystemLicenseClient =
    abstract GetLicenseAsync: unit -> Task<LicenseResponseModel<SystemEntitlementsModel>>
