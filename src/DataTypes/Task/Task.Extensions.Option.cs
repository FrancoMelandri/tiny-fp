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
    }
}
