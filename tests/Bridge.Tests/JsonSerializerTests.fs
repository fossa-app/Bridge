module JsonSerializerTests

open System
open Expecto
open Fossa.Bridge.Models.ApiModels
open Fossa.Bridge.Services

type TestModel = { Id: int64; Name: string }

type NullableTestModel =
    { RequiredName: string
      OptionalName: string | null }

type ComplexModel =
    { Id: int64
      Guid: Guid
      CreatedAt: DateTimeOffset
      Count: int }

[<Tests>]
let tests =
    testList
        "JsonSerializerTests"
        [ testCase "Serialize and Deserialize int64 with precision"
          <| fun _ ->
              let serializer = JsonSerializer() :> IJsonSerializer
              let originalId = 9223372036854775807L // Int64.MaxValue
              let model = { Id = originalId; Name = "Test" }

              let json = serializer.Serialize(model)
              // Verify that the JSON contains the raw number without quotes
              Expect.isTrue (json.Contains(":9223372036854775807")) "JSON should contain raw numeric int64"

              let deserialized = serializer.Deserialize<TestModel>(json)
              Expect.equal deserialized.Id originalId "Deserialized ID should match original"
              Expect.equal deserialized.Name "Test" "Deserialized Name should match original"

          testCase "Deserialize raw numeric int64 from string"
          <| fun _ ->
              let serializer = JsonSerializer() :> IJsonSerializer
              let json = "{\"Id\":9223372036854775807,\"Name\":\"Test\"}"

              let deserialized = serializer.Deserialize<TestModel>(json)
              Expect.equal deserialized.Id 9223372036854775807L "Should correctly parse large raw number"

          testCase "Complex model with Guid and DateTimeOffset"
          <| fun _ ->
              let serializer = JsonSerializer() :> IJsonSerializer

              let model =
                  { Id = 123456789012345L
                    Guid = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479")
                    CreatedAt = DateTimeOffset.Parse("2023-01-01T12:00:00+00:00")
                    Count = 42 }

              let json = serializer.Serialize(model)
              let deserialized = serializer.Deserialize<ComplexModel>(json)

              Expect.equal deserialized.Id 123456789012345L "ID conversion"
              Expect.equal deserialized.Guid model.Guid "Guid conversion"
              Expect.equal deserialized.CreatedAt model.CreatedAt "DateTimeOffset conversion"
              Expect.equal deserialized.Count 42 "Int conversion"

          testCase "Serializer applies shared camelCase policy"
          <| fun _ ->
              let serializer = JsonSerializer() :> IJsonSerializer

              let json =
                  serializer.Serialize(
                      { RequiredName = "Required"
                        OptionalName = null }
                  )

              Expect.equal
                  json
                  "{\"requiredName\":\"Required\",\"optionalName\":null}"
                  "Serializer should apply common API JSON policy"

          testCase "ProblemDetailsModel serializes with RFC field names"
          <| fun _ ->
              let serializer = JsonSerializer() :> IJsonSerializer

              let model =
                  { Type = "https://example.com/problems/conflict"
                    Title = "Conflict"
                    Status = Nullable 409
                    Detail = "The requested change conflicts with current state."
                    Instance = "/companies/42" }

              let json = serializer.Serialize(model)

              Expect.isTrue (json.Contains("\"type\"")) "JSON should contain lowercase type"
              Expect.isTrue (json.Contains("\"title\"")) "JSON should contain lowercase title"
              Expect.isTrue (json.Contains("\"status\"")) "JSON should contain lowercase status"
              Expect.isTrue (json.Contains("\"detail\"")) "JSON should contain lowercase detail"
              Expect.isTrue (json.Contains("\"instance\"")) "JSON should contain lowercase instance"
              Expect.isFalse (json.Contains("\"Type\"")) "JSON should not contain PascalCase Type"

          testCase "ProblemDetailsModel serializes null core fields"
          <| fun _ ->
              let serializer = JsonSerializer() :> IJsonSerializer

              let model =
                  { Type = null
                    Title = null
                    Status = Nullable 404
                    Detail = null
                    Instance = null }

              let json = serializer.Serialize(model)

              Expect.equal
                  json
                  "{\"type\":null,\"title\":null,\"status\":404,\"detail\":null,\"instance\":null}"
                  "Null core fields should be serialized"

          testCase "ProblemDetailsModel deserializes known fields"
          <| fun _ ->
              let serializer = JsonSerializer() :> IJsonSerializer

              let json =
                  "{\"type\":\"https://example.com/problems/not-found\",\"title\":\"Not Found\",\"status\":404,\"detail\":\"Missing.\",\"instance\":\"/companies/99\"}"

              let model = serializer.Deserialize<ProblemDetailsModel>(json)

              Expect.equal model.Type "https://example.com/problems/not-found" "Type should deserialize"
              Expect.equal model.Title "Not Found" "Title should deserialize"
              Expect.equal model.Status (Nullable 404) "Status should deserialize"
              Expect.equal model.Detail "Missing." "Detail should deserialize"
              Expect.equal model.Instance "/companies/99" "Instance should deserialize" ]
