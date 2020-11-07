using AcmeCorp.Persistence;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Tests
{
    public class TestFixture : IDisposable
    {
        public AcmeCorpContext DbContext { get; set; }

        public TestFixture()
        {
            var options = new DbContextOptionsBuilder<AcmeCorpContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            DbContext = new AcmeCorpContext(options);
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
