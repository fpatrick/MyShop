using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        public string Category { get; set; }

        //public ProductCategory() //Construct to generate a Id whenever a new model is created
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}
    }
}
