using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimSystem.Models
{
    [Table("Addresses")]
    public class Address
    {
        public int AddressId { get; set; }
        public AddressType AddressType { get; set; }
        [Required]
        public int Number { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Neighborhood { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string City { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Country { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee  { get; set; }

        [NotMapped]
        public string FullAddress => string.Format("{0} #{1}, {2}, {3}, {4} ", AddressType.ToString(), Number, Neighborhood, City, Country);

    }

    public enum AddressType
    {
        [Display(Name = "Casa")]
        house = 0,
        [Display(Name = "Apartamento")]
        depto = 1 
    }
}