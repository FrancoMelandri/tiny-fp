using System;

namespace TinyFp.Extensions
{
    public static partial class FunctionalExtension
    {
        public static Unit Using(IDisposable disposable, Action action)
        {
            using (disposable)
                action();
            return Unit.Default;
        }

        public static T Using<T>(IDisposable disposable, Func<T> func)
        {
            using (disposable)
                return func();
        }

        public static T Using<T, V>(V disposable, Func<V, T> action)
            where V : IDisposable
        {
            using (disposable)
                return action(disposable);
        }

        public static Unit Using(IDisposable disposable, Action<IDisposable> action)
        {
            using (disposable)
                action(disposable);
            return Unit.Default;
        }
    }
}
