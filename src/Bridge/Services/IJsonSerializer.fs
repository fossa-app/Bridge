namespace Fossa.Bridge.Services

type IJsonSerializer =
    abstract Serialize<'T> : value: 'T -> string
    abstract Deserialize<'T> : json: string -> 'T
