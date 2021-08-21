using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    [Table("Positions")]
    public class Position
    {
        public int PositionId { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Maximo 100 caracteres"), Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required, MaxLength(500, ErrorMessage = "Maximo 500 caracteres"), Display(Name = "Descripcion")]
        public string Desc { get; set; }
        [Display(Name = "Salario")]
        public int Salary { get; set; }

        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; }
        [Display(Name = "Departamento")]
        public Department Department { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}