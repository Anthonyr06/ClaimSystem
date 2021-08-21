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
        [Required, MaxLength(100, ErrorMessage = "Maximo 100 caracteres"), Display(Name = "Plato/Bebida")]
        public string Dish { get; set; }
        [Required, MaxLength(500, ErrorMessage = "Maximo 500 caracteres"),Display(Name = "Descripcion")]
        public string Desc { get; set; }
        [Required]
        [DataType(DataType.Date), Display(Name = "Fecha de inicio")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), Display(Name = "Fecha de solucion")]
        public DateTime? SolutionDate { get; set; }
        [MaxLength(200, ErrorMessage = "Maximo 200 caracteres"), Display(Name = "Solucion")]
        public string Solution { get; set; }
        
        public bool? Liked { get; set; }
        [Display(Name = "Tipo de reclamacion")]
        public int ClaimTypeId { get; set; }
        [Display(Name = "Tipo de reclamacion")]
        public ClaimType ClaimType { get; set; }

        [Display(Name = "Estado")]
        public int ClaimStateId { get; set; }
        [Display(Name = "Estado")]
        public ClaimState ClaimState { get; set; }

        [Display(Name = "Prioridad")]
        public int ClaimPriorityId { get; set; }
        [Display(Name = "Prioridad")]
        public ClaimPriority ClaimPriority { get; set; }

        [Display(Name = "Empleado")]
        public int? EmployeeId { get; set; }
        [Display(Name = "Empleado")]
        public Employee Employee { get; set; }

        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }
        [Display(Name = "Cliente")]
        public Customer Customer { get; set; }

    }
}