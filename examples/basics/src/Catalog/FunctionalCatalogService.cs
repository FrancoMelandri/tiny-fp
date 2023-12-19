using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.Examples.Basics.Catalog;

public class FunctionalCatalogService(IApiClient apiClient) : ICatalogService
{
    public Either<string, Catalog> Get()
        => Try(apiClient.Get)
            .ToEither()
            .MapLeft(ex => ex.Message);
}