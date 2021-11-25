using TinyFp;
using TinyFpTest.Models;

namespace TinyFpTest.Services.Details
{
    public class DetailsRepository : IDetailsRepository
    {
        public Task<Option<ProductDetails>> GetByProductName(string productName)
            => throw new System.NotImplementedException();        
    }
}
