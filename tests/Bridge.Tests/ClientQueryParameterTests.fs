module ClientQueryParameterTests

open System
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks
open Expecto
open Fossa.Bridge.Models
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services
open Fossa.Bridge.Services.Clients

type private CapturingTransport() =
    let mutable lastUrl: string | null = null

    member _.LastUrl = lastUrl

    interface IHttpTransport with
        member _.GetAsync<'TResponse when 'TResponse: not struct and 'TResponse: not null>
            (endpointUrl: string, _endpointSecurity: EndpointSecurity, _cancellationToken: CancellationToken)
            : Task<ClientResult<'TResponse>> =
            lastUrl <- endpointUrl

            Task.FromResult(ClientResult.Success Unchecked.defaultof<'TResponse>)

        member _.PostAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                _endpointSecurity: EndpointSecurity,
                _request: 'TRequest,
                _cancellationToken: CancellationToken
            ) : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult(ClientUnitResult.Success)

        member _.PutAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                _endpointSecurity: EndpointSecurity,
                _request: 'TRequest,
                _cancellationToken: CancellationToken
            ) : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult(ClientUnitResult.Success)

        member _.PatchAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                _endpointSecurity: EndpointSecurity,
                _request: 'TRequest,
                _cancellationToken: CancellationToken
            ) : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult(ClientUnitResult.Success)

        member _.DeleteAsync
            (endpointUrl: string, _endpointSecurity: EndpointSecurity, _cancellationToken: CancellationToken)
            : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult(ClientUnitResult.Success)

let private idList (values: int64 seq) =
    ResizeArray<int64>(values) :> IReadOnlyList<int64>

let private capturedUrl (transport: CapturingTransport) =
    match transport.LastUrl with
    | null -> failtest "Client should send a request through the transport"
    | url -> url

let private queryPart (url: string) =
    let queryStart = url.IndexOf("?")
    Expect.isGreaterThanOrEqual queryStart 0 "URL should include query parameters"
    url.Substring(queryStart + 1)

let private expectNoPascalCaseQueryNames (url: string) =
    let queryKeys =
        (queryPart url).Split('&')
        |> Array.map (fun parameter -> parameter.Split('=')[0])

    [ "Id"; "Search"; "PageNumber"; "PageSize"; "ReportsToId"; "TopLevelOnly" ]
    |> List.iter (fun key -> Expect.isFalse (Array.contains key queryKeys) $"Query should not contain {key}")

[<Tests>]
let tests =
    testList
        "ClientQueryParameterTests"
        [ testCase "Branch query uses camelCase query parameter names"
          <| fun _ ->
              let transport = CapturingTransport()
              let client = BranchClient(transport)

              client
                  .getBranchesAsync(
                      { id = idList [ 1L; 2L ]
                        search = "north"
                        pageNumber = Nullable 3
                        pageSize = Nullable 25 },
                      CancellationToken.None
                  )
                  .GetAwaiter()
                  .GetResult()
              |> ignore

              let url = capturedUrl transport

              Expect.equal
                  url
                  "api/1.0/Branches?id=1&id=2&search=north&pageNumber=3&pageSize=25"
                  "Branch query should use camelCase wire names"

              expectNoPascalCaseQueryNames url

          testCase "Department query uses camelCase query parameter names"
          <| fun _ ->
              let transport = CapturingTransport()
              let client = DepartmentClient(transport)

              client
                  .getDepartmentsAsync(
                      { id = idList [ 10L; 20L ]
                        search = "ops"
                        pageNumber = Nullable 2
                        pageSize = Nullable 50 },
                      CancellationToken.None
                  )
                  .GetAwaiter()
                  .GetResult()
              |> ignore

              let url = capturedUrl transport

              Expect.equal
                  url
                  "api/1.0/Departments?id=10&id=20&search=ops&pageNumber=2&pageSize=50"
                  "Department query should use camelCase wire names"

              expectNoPascalCaseQueryNames url

          testCase "Employee query uses camelCase query parameter names"
          <| fun _ ->
              let transport = CapturingTransport()
              let client = EmployeeClient(transport)

              client
                  .getEmployeesAsync(
                      { id = idList [ 99L ]
                        search = "casey"
                        pageNumber = Nullable 4
                        pageSize = Nullable 15
                        reportsToId = Nullable 7L
                        topLevelOnly = Nullable true },
                      CancellationToken.None
                  )
                  .GetAwaiter()
                  .GetResult()
              |> ignore

              let url = capturedUrl transport

              Expect.equal
                  url
                  "api/1.0/Employees?id=99&search=casey&pageNumber=4&pageSize=15&reportsToId=7&topLevelOnly=true"
                  "Employee query should use camelCase wire names"

              expectNoPascalCaseQueryNames url

          testCase "Employee paging query uses camelCase query parameter names"
          <| fun _ ->
              let transport = CapturingTransport()
              let client = EmployeeClient(transport)

              client
                  .getEmployeesPagingAsync(
                      { search = "casey"
                        pageNumber = Nullable 5
                        pageSize = Nullable 30 },
                      CancellationToken.None
                  )
                  .GetAwaiter()
                  .GetResult()
              |> ignore

              let url = capturedUrl transport

              Expect.equal
                  url
                  "api/1.0/Employees?search=casey&pageNumber=5&pageSize=30"
                  "Employee paging query should use camelCase wire names"

              expectNoPascalCaseQueryNames url ]
