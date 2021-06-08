using ClaimSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ClaimSystem.Data.Configurations
{
    public class PositionConfig : EntityTypeConfiguration<Position>
    {
        public PositionConfig()
        {
            HasIndex(p => p.Name).IsUnique();
        }
    }
}