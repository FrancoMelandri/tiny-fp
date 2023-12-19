namespace TinyFpTest.Examples.Basics.Catalog;

public class ImperativeCatalogService(
    IApiClient apiClient,
    ICache cache,
    ILogger logger)
{
    public Catalog Get()
    {
        try
        {
            var catalog = cache.Get();
            if (catalog == null)
            {
                catalog = apiClient.Get();
                cache.Set(catalog);
            }
            return catalog;
        }
        catch (Exception ex)
        {
            logger.Log(ex.Message);
            return null;
        }
    }
}