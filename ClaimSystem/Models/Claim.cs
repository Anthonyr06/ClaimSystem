using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    [Table("Claims")]
    public class Claim
    {
        public int ClaimId { get; set; }
        [Required, MaxLength(500, ErrorMessage = "Maximo 500 caracteres")]
        public string Desc { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime SolutionDate { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Solution { get; set; }

        public int ClaimTypeId { get; set; }
        public ClaimType ClaimType { get; set; }

        public int ClaimStateId { get; set; }
        public ClaimState ClaimState { get; set; }

        public int ClaimPriorityId { get; set; }
        public ClaimPriority ClaimPriority { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}