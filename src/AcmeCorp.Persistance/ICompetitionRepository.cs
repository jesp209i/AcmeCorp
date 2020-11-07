using System;
using System.Threading.Tasks;

namespace AcmeCorp.Persistance
{
    public interface ICompetitionRepository
    {
        Task<bool> IsSerialNumberEligible(string serialNumber);
    }
}
