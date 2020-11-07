using System;

namespace AcmeCorp.Infrastructure
{
    public interface IProductService
    {
        bool IsSerialNumberValid(string serialNumber);
    }
}
