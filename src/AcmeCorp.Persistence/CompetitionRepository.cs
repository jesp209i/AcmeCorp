using AcmeCorp.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorp.Persistence
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly AcmeCorpContext _dbcontext;

        public CompetitionRepository(AcmeCorpContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task AddContestant(Contestant contestant)
        {
            _dbcontext.Contestants.Add(contestant);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> IsSerialNumberEligible(string serialNumber)
        {
            bool usedSerialNumber = await _dbcontext.Contestants.AnyAsync(x => x.SerialNumber == serialNumber);
            return !usedSerialNumber;
        }
    }
}