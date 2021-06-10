
using Microsoft.EntityFrameworkCore;
using Polly;
using System;

namespace Contract.Infra.Data
{
    public class ContractDBContext : DbContext
    {
        public ContractDBContext(DbContextOptions<ContractDBContext> options) : base(options)
        {

        }

        public DbSet<Model.Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Model.Customer>().HasKey(m => m.CustomerNumber);
            builder.Entity<Model.Contract>().HasKey(m => m.ContractNumber);
            base.OnModelCreating(builder);
        }

        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }
    }
}