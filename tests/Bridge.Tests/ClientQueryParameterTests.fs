module ClientQueryParameterTests

open System
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks
open Expecto
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

            Task.FromResult(
                { Succeeded = true
                  Value = Unchecked.defaultof<'TResponse>
                  Problem = null }
            )

        member _.PostAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                _endpointSecurity: EndpointSecurity,
                _request: 'TRequest,
                _cancellationToken: CancellationToken
            ) : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult({ Succeeded = true; Problem = null })

        member _.PutAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                _endpointSecurity: EndpointSecurity,
                _request: 'TRequest,
                _cancellationToken: CancellationToken
            ) : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult({ Succeeded = true; Problem = null })

        member _.PatchAsync<'TRequest when 'TRequest: not null>
            (
                endpointUrl: string,
                _endpointSecurity: EndpointSecurity,
                _request: 'TRequest,
                _cancellationToken: CancellationToken
            ) : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult({ Succeeded = true; Problem = null })

        member _.DeleteAsync
            (endpointUrl: string, _endpointSecurity: EndpointSecurity, _cancellationToken: CancellationToken)
            : Task<ClientUnitResult> =
            lastUrl <- endpointUrl
            Task.FromResult({ Succeeded = true; Problem = null })

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
                  .GetBranchesAsync(
                      { Id = idList [ 1L; 2L ]
                        Search = "north"
                        PageNumber = Nullable 3
                        PageSize = Nullable 25 },
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
                  .GetDepartmentsAsync(
                      { Id = idList [ 10L; 20L ]
                        Search = "ops"
                        PageNumber = Nullable 2
                        PageSize = Nullable 50 },
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
                  .GetEmployeesAsync(
                      { Id = idList [ 99L ]
                        Search = "casey"
                        PageNumber = Nullable 4
                        PageSize = Nullable 15
                        ReportsToId = Nullable 7L
                        TopLevelOnly = Nullable true },
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
                  .GetEmployeesPagingAsync(
                      { Search = "casey"
                        PageNumber = Nullable 5
                        PageSize = Nullable 30 },
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
