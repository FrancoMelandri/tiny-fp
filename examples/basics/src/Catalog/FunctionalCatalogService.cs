using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.Examples.Basics.Catalog
{
    public class FunctionalCatalogService : ICatalogService
    {
        private readonly IApiClient _apiClient;

        public FunctionalCatalogService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Either<string, Catalog> Get()
            => Try(() => _apiClient.Get())
                .ToEither()
                .MapLeft(ex => ex.Message);
    }
}
