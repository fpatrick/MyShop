using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        [StringLength(20)] //Maximum caracters name will have
        [DisplayName("Product Name")] //To display name in scaffolding
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 1000)]  //Range to the price to not add a nonsense price
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public Product() //Construct to automatic generate a Id when instance a product (some database do it by itself, but coding may give more flexbility)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
