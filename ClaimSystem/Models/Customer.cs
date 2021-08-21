using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required, StringLength(11)]
        public string Cedula { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Maximo 100 caracteres"), Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres"), Display(Name = "Apellido")]
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
        [Display(Name = "Direccion")]
        public int? AddressId { get; set; }
        [Display(Name = "Direccion")]
        public Address Address { get; set; }

        [DataType(DataType.Date), Display(Name = "Cliente desde")]
        public DateTime EnrollmentDate { get; set; }

        public IList<Claim> Claims { get; set; }

        [NotMapped]
        public string FullName => string.Format("{0} {1}", Name, LastName);

    }
}