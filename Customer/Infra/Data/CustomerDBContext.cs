
using Microsoft.EntityFrameworkCore;
using Polly;
using System;

namespace Customer.Infra.Data
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
        {

        }

        public DbSet<Model.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Model.Customer>().HasKey(m => m.CustomerNumber);
            builder.Entity<Model.Contract>().HasKey(m => m.ContractNumber);
            base.OnModelCreating(builder);
        }
    }
}