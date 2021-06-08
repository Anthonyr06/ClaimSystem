using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    [Table("Employees")]
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(11)]
        public string Cedula { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Name { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string LastName { get; set; }
        [Required, StringLength(254)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefono invalido")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Telefono invalido.")]
        public string PhoneNumber { get; set; }
        public DateTime AdmissionDate { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IList<Claim> Claims { get; set; }
    }
}