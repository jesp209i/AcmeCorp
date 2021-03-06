﻿using AcmeCorp.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
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
        // return 10 results per page
        public async Task<List<Contestant>> GetSubmissionsPage(int page)
        {
            int maxResultsPrPage = 10;
            int realPage = page - 1;
            var result = await _dbcontext.Contestants.OrderBy(x => x.Id).Skip(realPage * maxResultsPrPage).Take(maxResultsPrPage).ToListAsync();
            return result;
        }

        public async Task<bool> IsSerialNumberEligible(string serialNumber)
        {
            bool usedSerialNumber = await _dbcontext.Contestants.AnyAsync(x => x.SerialNumber == serialNumber);
            return !usedSerialNumber;
        }

        public async Task<int> MaxPageCount()
        {
            var entries = await _dbcontext.Contestants.CountAsync();
            int m = entries / 10;
            if (entries % 10 > 0) { m++; }
            return m;
        }
    }
}