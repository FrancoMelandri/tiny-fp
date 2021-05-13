using Serilog;
using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details
{
    public class LoggedDetailsDrivenPort : IDetailsDrivenPort
    {
        private readonly IDetailsDrivenPort _detailsDrivenPort;
        private readonly ILogger _logger;

        public LoggedDetailsDrivenPort(IDetailsDrivenPort detailsDrivenPort,
                                       ILogger logger)
        {
            _detailsDrivenPort = detailsDrivenPort;
            _logger = logger;
        }

        public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
           => _detailsDrivenPort.GetDetailsAsync(productName);
    }
}
