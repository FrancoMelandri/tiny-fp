using TinyFp;
using TinyFp.Extensions;

namespace TinyFpTest.Examples.Basics.Catalog
{
    public class LoggedCatalogService : ICatalogService
    {
        private readonly ILogger _logger;
        private readonly ICatalogService _catalog;

        public LoggedCatalogService(ICatalogService catalog,
                                    ILogger logger)
        {
            _catalog = catalog;
            _logger = logger;
        }

        public Either<string, Catalog> Get()
            => _catalog
                .Get()
                .Tee(_ => _.OnLeft(_ => _logger.Log(_)));        
    }
}
