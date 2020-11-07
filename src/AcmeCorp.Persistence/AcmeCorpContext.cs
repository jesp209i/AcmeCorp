using AcmeCorp.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeCorp.Persistence
{
    public class AcmeCorpContext : DbContext
    {
        public DbSet<Contestant> Contestants { get; set; }
        public AcmeCorpContext(DbContextOptions<AcmeCorpContext> options) : base(options)
        {

        }
    }
}
