using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public abstract class BaseEntity //by setting it as abstract, we never can create a instance from this class
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } //Tell us when it was created

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString(); //Give the internal Id a id
            this.CreatedAt = DateTime.Now;
        }
    }
}
