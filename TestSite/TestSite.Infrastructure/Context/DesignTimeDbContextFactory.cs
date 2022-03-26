using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TestSite.Infrastructure.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestSiteContext>
    {
        public TestSiteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestSiteContext>();

            string connectionString = args.Length == 0
                ? "Data Source =.; Initial Catalog = TestSite; Integrated Security = True"
                : args[0];

            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new TestSiteContext(optionsBuilder.Options);
        }

    }
}
