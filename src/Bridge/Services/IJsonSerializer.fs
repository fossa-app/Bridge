namespace Fossa.Bridge.Services

type IJsonSerializer =
    abstract Serialize<'T when 'T: not null> : value: 'T -> string
    abstract Deserialize<'T when 'T: not null> : json: string -> 'T
