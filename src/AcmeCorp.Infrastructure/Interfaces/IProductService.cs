using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Infrastructure.Interfaces
{
    public interface IProductService
    {
        Task<bool> IsSerialNumberValid(string serialNumber);
        Task<ICollection<Product>> GetProducts();
    }
}
