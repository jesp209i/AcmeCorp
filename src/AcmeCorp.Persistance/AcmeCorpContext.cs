using AcmeCorp.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeCorp.Persistance
{
    public class AcmeCorpContext : DbContext
    {
        public DbSet<Contestant> Contestants { get; set; }
        public AcmeCorpContext(DbContextOptions<AcmeCorpContext> options) : base(options)
        {

        }
    }
}
