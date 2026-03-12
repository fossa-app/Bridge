namespace Fossa.Bridge

#if FABLE_COMPILER
open Fable.Core

[<Global>]
type AbortSignal = interface end
#endif

namespace System.Collections.Generic

#if FABLE_COMPILER
type IReadOnlyList<'T> = 'T array
type IReadOnlyCollection<'T> = 'T array
#endif

namespace System.Threading

#if FABLE_COMPILER
type CancellationToken = Fossa.Bridge.AbortSignal
#endif

namespace System.Threading.Tasks

#if FABLE_COMPILER
type Task<'T> = Fable.Core.JS.Promise<'T>
type Task = Fable.Core.JS.Promise<unit>
#endif
