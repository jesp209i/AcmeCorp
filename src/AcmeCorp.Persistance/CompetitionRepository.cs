using AcmeCorp.Persistance.Models;
using System.Threading.Tasks;

namespace AcmeCorp.Persistance
{
    public class CompetitionRepository : ICompetitionRepository
    {
        public Task AddContestant(Contestant contestant)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsSerialNumberEligible(string serialNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}