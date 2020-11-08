using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeCorp.Infrastructure.Interfaces;

namespace AcmeCorp.Infrastructure
{
    public class ProductService : IProductService
    {
        private readonly IProductFetcher _fetcher;

        public ProductService(IProductFetcher fetcher)
        {
            _fetcher = fetcher;
        }
        public async Task<bool> IsSerialNumberValid(string serialNumber)
        {
            var product = await _fetcher.GetProduct(serialNumber);
            return !(product is null);
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            return await _fetcher.GetProducts();
        }
    }
}