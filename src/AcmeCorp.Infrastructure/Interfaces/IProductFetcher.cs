using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorp.Infrastructure.Interfaces
{
    public interface IProductFetcher
    {
        Task<Product> GetProduct(string serialNumber);
        Task<ICollection<Product>> GetProducts();
    }
}
