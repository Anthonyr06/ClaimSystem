using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    [Table("Departments")]
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Room { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string ProcessingPlant{ get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Workshop { get; set; }

        public IList<Position> Positions { get; set; }
    }
}