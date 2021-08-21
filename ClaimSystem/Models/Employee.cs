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

        [Required, MaxLength(100, ErrorMessage = "Maximo {1} caracteres"), Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo {1} caracteres"), Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required, StringLength(254)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password), Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefono invalido")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Telefono invalido."), Display(Name = "Telefono")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date), Display(Name = "Empleado desde")]
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Posicion")]
        public int PositionId { get; set; }
        [Display(Name = "Posicion")]
        public Position Position { get; set; }

        [Display(Name = "Direccion")]
        public int? AddressId { get; set; }
        [Display(Name = "Direccion")]
        public Address Address { get; set; }

        public IList<Claim> Claims { get; set; }


        [NotMapped]
        public string FullName => string.Format("{0} {1}", Name, LastName);
    }
}