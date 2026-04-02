module JsonSerializerTests

open System
open Expecto
open Fossa.Bridge.Services

type TestModel = { Id: int64; Name: string }

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
              Expect.equal deserialized.Count 42 "Int conversion" ]
