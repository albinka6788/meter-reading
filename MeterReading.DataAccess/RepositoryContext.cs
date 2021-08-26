using MeterReading.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MeterReading.DataAccess
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<AccountEntityModel> Accounts { get; set; }
        public DbSet<Models.MeterReadingEntityModel> MeterReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SetKeys(modelBuilder);
        }
         private void SetKeys(ModelBuilder builder)
        {
            builder.Entity<AccountEntityModel>().HasKey(r => new { r.Id, });
            builder.Entity<MeterReadingEntityModel>().HasKey(r => new { r.Id, });
        }
    }
}
