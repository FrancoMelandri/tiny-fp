using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details;

public class LoggedDetailsDrivenPort(
    IDetailsDrivenPort detailsDrivenPort,
    Serilog.ILogger logger)
    : IDetailsDrivenPort
{
    public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
        => detailsDrivenPort
            .GetDetailsAsync(productName)
            .BindLeftAsync(LogError);

    private Either<ApiError, ProductDetails> LogError(ApiError error)
        => error.Tee(_ => logger.Error($"{_.StatusCode}, {_.Code}, {_.Description}"));
}