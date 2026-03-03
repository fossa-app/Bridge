namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanyLicenseClient =
    abstract member GetLicenseAsync: unit -> Task<LicenseResponseModel<CompanyEntitlementsModel>>
    abstract member CreateLicenseAsync: model: string -> Task<LicenseResponseModel<CompanyEntitlementsModel>>
