using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.WebUI;
using MyShop.WebUI.Controllers;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new Mocks.MockContext<Product>();   //instead of a new inmemory or sql
            IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();

            productContext.Insert(new Product());

            HomeController controller = new HomeController(productContext, productCategoryContext);

            var result = controller.Index() as ViewResult; //now we can call the index method on the controller to get the results
            var viewModel = (ProductListViewModel)result.ViewData.Model; //AS parte of view result it will contain a product list viewmodel

            Assert.AreEqual(1, viewModel.Products.Count());
        }

    }
}
