using TinyFp;
using TinyFp.Extensions;

namespace TinyFpTest.Examples.Basics.Catalog;

public class LoggedCatalogService(
    ICatalogService catalog,
    ILogger logger) : ICatalogService
{
    public Either<string, Catalog> Get()
        => catalog
            .Get()
            .Tee(_ => _.OnLeft(logger.Log));        
}