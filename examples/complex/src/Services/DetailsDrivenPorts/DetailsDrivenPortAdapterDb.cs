using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details
{
    public class DetailsDrivenPortAdapterDb : IDetailsDrivenPort
    {
        public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
            => throw new System.Exception("error");
    }    
}
