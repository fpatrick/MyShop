using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess
{
    public class DataContext : DbContext //DbContext (from entity) includes all the base methods to our datacontext to work
    {
        public DataContext()    //Construct to capture and passes the database string DbContext  that expect
            : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }  //Tells which models will be stored in tables, because we dont want to store our view models in the database
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //Next we go to View > Other Windows > Package Manager Console.   Change default projetc to DataAccess.SQL
        //Right click WebUI and click Set as Startup Project
        //On console 1: Enable-Migrations; 2: Add-Migration Initial (can be any name); 3: Update-Database
    }
}
