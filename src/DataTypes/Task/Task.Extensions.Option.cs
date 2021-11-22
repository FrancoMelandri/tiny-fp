using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System;

namespace TinyFp
{
    public static partial class TaskExtensions
    {
        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this,
                                                     Func<A, B> onSome,
                                                     Func<B> onNone)
            => (await @this).Match(onSome, onNone);

        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this, 
                                                     Func<A, B> onSome, 
                                                     Func<Task<B>> onNone)
            => await (await @this).MatchAsync(onSome, onNone);

        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this,
                                                     Func<A, Task<B>> onSome,
                                                     Func<B> onNone)
            => await (await @this).MatchAsync(onSome, onNone);

        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this, 
                                                     Func<A, Task<B>> onSome, 
                                                     Func<Task<B>> onNone)
            => await (await @this).MatchAsync(onSome, onNone);

        [Pure]
        public static async Task<Option<B>> BindAsync<A, B>(this Task<Option<A>> @this,
                                                            Func<A, Option<B>> onBind)
            => (await @this).Bind(onBind);

        [Pure]
        public static async Task<Option<B>> MapAsync<A, B>(this Task<Option<A>> @this,
                                                            Func<A, B> onMap)
            => (await @this).Map(onMap);

        [Pure]
        public static async Task<Option<B>> GuardMapAsync<A, B>(this Task<Option<A>> @this, Func<A, B> delegateIfDefault, params (Func<A, bool>, Func<A, B>)[] guards)
            => (await @this).GuardMap(delegateIfDefault, guards);

        [Pure]
        public static async Task<Option<B>> GuardMapAsync<A, B>(this Task<Option<A>> @this, Func<A, Task<B>> delegateIfDefault, params (Func<A, bool>, Func<A, Task<B>>)[] guards)
            => await (await @this).GuardMapAsync(delegateIfDefault, guards);

        [Pure]
        public static async Task<Option<B>> GuardBindAsync<A, B>(this Task<Option<A>> @this, Func<A, Option<B>> delegateIfDefault, params (Func<A, bool>, Func<A, Option<B>>)[] guards)
            => (await @this).GuardBind(delegateIfDefault, guards);

        [Pure]
        public static async Task<Option<B>> GuardBindAsync<A, B>(this Task<Option<A>> @this, Func<A, Task<Option<B>>> delegateIfDefault, params (Func<A, bool>, Func<A, Task<Option<B>>>)[] guards)
            => await (await @this).GuardBindAsync(delegateIfDefault, guards);
    }
}
