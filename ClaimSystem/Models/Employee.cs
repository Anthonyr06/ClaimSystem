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
        [Key]
        public int EmployeeId { get; set; }
        [Required, MaxLength(11, ErrorMessage = "Maximo {1} caracteres. Sin guiones")]
        public string Cedula { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Maximo {1} caracteres")]
        public string Name { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo {1} caracteres")]
        public string LastName { get; set; }
        [Required, StringLength(254)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefono invalido")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Telefono invalido.")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime AdmissionDate { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public IList<Claim> Claims { get; set; }


        [NotMapped]
        public string FullName => string.Format("{0} {1}", Name, LastName);
    }
}