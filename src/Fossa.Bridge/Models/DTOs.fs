namespace Fossa.Bridge.Models

open System

[<CLIMutable>]
type UserProfile =
    { Id: Guid
      Username: string
      Email: string
      FirstName: string
      LastName: string
      DateOfBirth: Nullable<DateTimeOffset>
      IsActive: bool }

[<CLIMutable>]
type AuthenticationResponse =
    { Token: string
      RefreshToken: string
      Expiration: DateTimeOffset
      User: UserProfile }
