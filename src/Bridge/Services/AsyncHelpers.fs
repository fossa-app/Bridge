namespace Fossa.Bridge.Services

open System.Threading
open System.Threading.Tasks
#if FABLE_COMPILER
open Fable.Core
#endif

[<AutoOpen>]
module internal AsyncHelpers =
    let startAsTaskGeneric<'T> (computation: Async<'T>, cancellationToken: CancellationToken) : Task<'T> =
#if FABLE_COMPILER
        Async.StartAsPromise(computation)
#else
        Async.StartAsTask(computation, cancellationToken = cancellationToken)
#endif

    let startAsTaskUnit (computation: Async<unit>, cancellationToken: CancellationToken) : Task =
#if FABLE_COMPILER
        Async.StartAsPromise(computation)
#else
        Async.StartAsTask(computation, cancellationToken = cancellationToken) :> Task
#endif

    let awaitTask (t: Task<'T>) : Async<'T> =
#if FABLE_COMPILER
        Async.AwaitPromise(t)
#else
        Async.AwaitTask(t)
#endif
