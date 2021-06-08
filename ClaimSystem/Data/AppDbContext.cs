using ClaimSystem.Data.Configurations;
using ClaimSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClaimSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("ClaimSystemDb")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimPriority> ClaimPriorities { get; set; }
        public DbSet<ClaimState> ClaimStates { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new EmployeeConfig());
            modelBuilder.Configurations.Add(new PositionConfig());
        }
    }
}
