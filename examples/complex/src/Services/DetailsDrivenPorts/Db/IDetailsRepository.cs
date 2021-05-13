using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;

namespace TinyFpTest.Services.Details
{
    public interface IDetailsRepository
    {
        Task<Option<ProductDetails>> GetByProductName(string productName);
    }
}
