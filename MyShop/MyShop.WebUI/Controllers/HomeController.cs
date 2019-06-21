using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;  //Instance of productrepository
        IRepository<ProductCategory> productCategories; //so we can load product categories from the database

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext) //Construct to inicialize that repository
        {
            context = productContext;
            productCategories = productCategoryContext;
        }

        public ActionResult Index(string Category=null) //By using null, you can have a null item or if you don't pass anything in it will assume it is null
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList(); //convert into sql statement that will filter that list category for us rather sending everything back
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}