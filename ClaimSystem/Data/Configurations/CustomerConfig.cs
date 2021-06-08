using ClaimSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ClaimSystem.Data.Configurations
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            HasIndex(c => c.Cedula).IsUnique();

            HasRequired(c => c.Address)
                .WithOptional(a => a.Customer)
                .WillCascadeOnDelete(false);
        }
    }
}