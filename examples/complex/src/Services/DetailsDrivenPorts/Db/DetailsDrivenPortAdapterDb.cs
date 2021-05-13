using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFpTest.Constants.Errors;

namespace TinyFpTest.Services.Details
{
    public class DetailsDrivenPortAdapterDb : IDetailsDrivenPort
    {
        private readonly IDetailsRepository _detailsRepository;

        public DetailsDrivenPortAdapterDb(IDetailsRepository detailsRepository)
        {
            _detailsRepository = detailsRepository;
        }

        public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
            => _detailsRepository
                .GetByProductName(productName)
                .MatchAsync(
                    _ => Either<ApiError, ProductDetails>.Right(_),
                    () => NotFoundOnDb);
    }
}
