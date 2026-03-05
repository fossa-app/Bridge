namespace Fossa.Bridge.Services.Clients

open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels

type ICompanyLicenseClient =
    abstract GetLicenseAsync: unit -> Task<LicenseResponseModel<CompanyEntitlementsModel>>
    abstract CreateLicenseAsync: model: string -> Task<LicenseResponseModel<CompanyEntitlementsModel>>
