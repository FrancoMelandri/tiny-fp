using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details
{
    public class ValidationDetailsDrivenPort : IDetailsDrivenPort
    {
        private readonly IDetailsDrivenPort _detailsDrivenPort;

        public ValidationDetailsDrivenPort(IDetailsDrivenPort detailsDrivenPort)
        {
            _detailsDrivenPort = detailsDrivenPort;
        }

        public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
           => _detailsDrivenPort.GetDetailsAsync(productName);
    }
}
