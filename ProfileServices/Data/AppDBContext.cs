using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;
using ProfileServices.Models.Entity;

namespace ProfileServices.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CandidateProfile> CandidateProfiles { get; set; }

        
        public DbConnection GetConnection()
        {
            return Database.GetDbConnection();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("ApplicationUser", schema: "ADANI_TALENT");

            modelBuilder.Entity<CandidateProfile>()
                .ToTable("CandidateProfile", schema: "ADANI_TALENT");
        }
    }
}
