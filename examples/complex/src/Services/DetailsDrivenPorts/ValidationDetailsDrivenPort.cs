using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFp.Prelude;
using static TinyFpTest.Constants.Errors;
using static TinyFpTest.Constants.Validation;

namespace TinyFpTest.Services.Details
{
    public class ValidationDetailsDrivenPort : IDetailsDrivenPort
    {
        private readonly IDetailsDrivenPort _detailsDrivenPort;

        public ValidationDetailsDrivenPort(IDetailsDrivenPort detailsDrivenPort)
        {
            _detailsDrivenPort = detailsDrivenPort;
        }

        private static Validation<ApiError, Unit> ValidateSpaces(string productName)
            => productName
                .Contains(BLANK_SPACE) ?
                    Fail<ApiError, Unit>(InvalidInput) :
                    Success<ApiError, Unit>(Unit.Default);

        private static Validation<ApiError, Unit> ValidateEmptyBlankOrNull(string forName)
            => string.IsNullOrWhiteSpace(forName) ?
                Fail<ApiError, Unit>(InvalidInput) :
                Success<ApiError, Unit>(Unit.Default);

        public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
           => ValidateEmptyBlankOrNull(productName)
                .Bind(_ => ValidateSpaces(productName))
                .MatchAsync(_ => _detailsDrivenPort.GetDetailsAsync(productName),
                            _ => Task.FromResult((Either<ApiError, ProductDetails>)_));
    }
}
