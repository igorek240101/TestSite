using TestSite.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestSite.Infrastructure.Context
{
    public class TestSiteContext : DbContext
    {

        public TestSiteContext(DbContextOptions<TestSiteContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Worker>(eb =>
            {
                eb.Property(c => c.Id);
                eb.Property(c => c.Name);
                eb.Property(c => c.BirthDate).HasColumnType("date");
                eb.Property(c => c.StartWorkDate).HasColumnType("date");
                eb.Property(c => c.Wage);
                eb.Property(c => c.DepartamentId);
            });

            modelBuilder.Entity<Worker>()
                .HasKey(o => new { o.Id });

            modelBuilder.Entity<Departament>(eb =>
            {
                eb.Property(c => c.Id);
                eb.Property(c => c.Name);
            });
            
            modelBuilder.Entity<Departament>()
                .HasKey(o => new { o.Id });

            

            // Seeding
            modelBuilder.Seed();
        }

        internal DbSet<Worker> Worker { get; set; }
        internal DbSet<Departament> Departament { get; set; }
    }
}
