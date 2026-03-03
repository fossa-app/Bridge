namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ISystemLicenseClient =
    abstract member GetLicenseAsync: unit -> Task<LicenseResponseModel<SystemEntitlementsModel>>
