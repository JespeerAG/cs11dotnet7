using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema; // [Column]

namespace Packt.Shared
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } // virtual allows lazy loading

        public Category()
        {
            Products = new HashSet<Product>();
        }
    }
}