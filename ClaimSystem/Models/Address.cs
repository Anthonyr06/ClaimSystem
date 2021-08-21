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
        [Required, Display(Name = "No.")]
        public int Number { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres"), Display(Name = "Calle")]
        public string Street { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres"), Display(Name = "Urbanizacion")]
        public string Neighborhood { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres"), Display(Name = "Ciudad")]
        public string City { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Maximo 200 caracteres"), Display(Name = "Pais")]
        public string Country { get; set; }

        public IList<Customer> Customers { get; set; }
        public IList<Employee> Employees  { get; set; }

        [NotMapped]
        public string FullAddress => string.Format("C/{5} {0} #{1}, {2}, {3}, {4} ", AddressType.ToString(), Number, Neighborhood, City, Country, Street);

    }

    public enum AddressType
    {
        [Display(Name = "Casa")]
        Casa = 0,
        [Display(Name = "Apartamento")]
        Depto = 1 
    }
}