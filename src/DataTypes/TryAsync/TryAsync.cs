using System.Threading.Tasks;
using TinyFp.Common;

namespace TinyFp
{
    public delegate Task<Result<A>> TryAsync<A>();
}
