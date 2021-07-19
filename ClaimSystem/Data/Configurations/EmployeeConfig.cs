using ClaimSystem.Models;
using System;
using System.Collections.Generic;
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
                        
            //HasRequired(e => e.Address)
            //    .WithOptional(a => a.Employees)
            //    .WillCascadeOnDelete(false);


        }
    }
}