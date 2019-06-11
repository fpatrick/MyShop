using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching; //[1*]
using MyShop.Core.Models; //[2*]
//[1*] Before using this project: Right click on references > Add Reference > (Make sure you are under assemblies tab) > Tick System.Runtime.Caching
//[2*] Under Project tab > Tick MyShop.Core.  We need to do it in order to know where our models are.
namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()  //Construct with some standard inicializations
        {
            products = cache["products"] as List<Product>; //Whenever launch this, will trying look in the cache see  if there is a cache called products
            if (products == null)  //If not found that cache, we create a new list of product
            {
                products = new List<Product>();
            }
        }

        public void Commit() //We can use it to not save straightway, so when we ready to save our product we store it on our cache
        {
            cache["products"] = products;
        }

        public void Insert (Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);  //Whintin the products list I find the product I need by Id and store in productToUpdate variable

            if (productToUpdate != null) //If received a product to update
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id); 

            if (product != null) //If find the product in the database, return it
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
  
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id); 

            if (productToDelete != null) //if found, remove from wour product list, the product
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
