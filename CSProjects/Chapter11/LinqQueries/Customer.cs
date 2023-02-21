using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema; // [Column]
using System.ComponentModel.DataAnnotations; // [Required]

namespace Packt.Shared
{
    public class Customer
    {
        [Required]
        public string CustomerId { get; set; } = null!;

        [Required]
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        
        [Column("Phone")]
        public string? PhoneNumber { get; set; }

        [Column("Fax")]
        public string? FaxNumber { get; set; }
    }
}