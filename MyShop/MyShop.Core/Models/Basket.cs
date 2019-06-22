using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseEntity
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; }//By passing it as virtual entity framework will know whenever we try to load the basket from database, it will try to load the items as well
        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
