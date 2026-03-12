namespace Fossa.Bridge.Services.Clients

open Fossa.Bridge

open System.Threading
open System.Threading.Tasks
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.UrlHelpers

type CompanyLicenseClient(transport: IHttpTransport) =
    member _.GetLicenseAsync
        (cancellationToken: CancellationToken)
        : Task<LicenseResponseModel<CompanyEntitlementsModel>> =
        transport.GetAsync<LicenseResponseModel<CompanyEntitlementsModel>>(
            composeRelativeUrl [ Endpoints.CompanyLicense ] [],
            cancellationToken
        )

    member _.CreateLicenseAsync(model: string, cancellationToken: CancellationToken) : Task =
        transport.PostAsync<string>(composeRelativeUrl [ Endpoints.CompanyLicense ] [], model, cancellationToken)

    interface ICompanyLicenseClient with
        member this.GetLicenseAsync(cancellationToken) = this.GetLicenseAsync(cancellationToken)

        member this.CreateLicenseAsync(model, cancellationToken) =
            this.CreateLicenseAsync(model, cancellationToken)
