using ClaimSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ClaimSystem.Data.Configurations
{
    public class EmployeeConfig : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfig()
        {
            HasIndex(e => e.Cedula).IsUnique();

            //Property(e => e.EmployeeId)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //HasRequired(e => e.Address)
            //    .WithOptional(a => a.Employees)
            //    .WillCascadeOnDelete(false);


        }
    }
}