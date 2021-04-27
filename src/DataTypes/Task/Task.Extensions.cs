using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace TinyFp
{
    public static class TaskExtensions
    {
        [Pure]
        public static Task<T> AsTask<T>(this T self) 
            => Task.FromResult(self);
    }
}
