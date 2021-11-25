using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details
{
    public class LoggedDetailsDrivenPort : IDetailsDrivenPort
    {
        private readonly IDetailsDrivenPort _detailsDrivenPort;
        private readonly Serilog.ILogger _logger;

        public LoggedDetailsDrivenPort(IDetailsDrivenPort detailsDrivenPort,
                                       Serilog.ILogger logger)
        {
            _detailsDrivenPort = detailsDrivenPort;
            _logger = logger;
        }

        public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
           => _detailsDrivenPort
                .GetDetailsAsync(productName)
                .BindLeftAsync(LogError);

        private Either<ApiError, ProductDetails> LogError(ApiError error)
            => error.Tee(_ => _logger.Error($"{_.StatusCode}, {_.Code}, {_.Description}"));
    }
}
