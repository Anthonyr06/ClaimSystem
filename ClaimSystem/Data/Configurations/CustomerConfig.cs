using ClaimSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

            //Property(c => c.CustomerId)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasRequired(c => c.Address)
            //    .WithOptional(a => a.Customers)
            //    .WillCascadeOnDelete(false);
        }
    }
}