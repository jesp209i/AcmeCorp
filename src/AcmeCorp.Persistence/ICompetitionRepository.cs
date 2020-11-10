using AcmeCorp.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorp.Persistence
{
    public interface ICompetitionRepository
    {
        Task<bool> IsSerialNumberEligible(string serialNumber);
        Task AddContestant(Contestant contestant);
        Task<List<Contestant>> GetSubmissionsPage(int page);
        Task<int> MaxPageCount();
    }
}
