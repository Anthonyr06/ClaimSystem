﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    [Table("ClaimPriorities")]
    public class ClaimPriority
    {
        public int ClaimPriorityId { get; set; }
        [Required, MaxLength(500, ErrorMessage = "Maximo 500 caracteres")]
        public string Desc { get; set; }

        public IList<Claim> Claims { get; set; }
    }
}