using MyShop.Core.Contracts;
using MyShop.Core.Models;
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

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList(); //take a list of products
            return View(products);
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