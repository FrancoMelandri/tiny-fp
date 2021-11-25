namespace TinyFpTest.Examples.Basics.Catalog
{
    public class ImperativeCatalogService
    {
        private readonly IApiClient _apiClient;
        private readonly ICache _cache;
        private readonly ILogger _logger;

        public ImperativeCatalogService(IApiClient apiClient,
                                        ICache cache,
                                        ILogger logger)
        {
            _apiClient = apiClient;
            _cache = cache;
            _logger = logger;
        }

        public Catalog Get()
        {
            try
            {
                var catalog = _cache.Get();
                if (catalog == null)
                {
                    catalog = _apiClient.Get();
                    _cache.Set(catalog);
                }
                return catalog;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
                return null;
            }
        }
    }
}
