using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//First of all: Right click on Myshop.Services > add > reference > assemblies tab > check System.Web
namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket"; //To identify the cookie

        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext)
        {
            this.basketContext = BasketContext;
            this.productContext = ProductContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull) //force the consuming service to sent httpcontext in as part of the core /  Also createifnull because something we want basket to be create if dont exists and sometimes we wont
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);  //we try to read the cookie
            Basket basket = new Basket();

            if (cookie != null) //if cookie exist, user visited before
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId)) //if it isnt null or empty string
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull) //check if we want to create a basket
                    {
                        basket = CreateNewBasket(httpContext); //uses a method to create a new basket
                    }
                }
            }
            else
            {
                if (createIfNull) //check if we want to create a basket
                {
                    basket = CreateNewBasket(httpContext); //uses a method to create a new basket
                }
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket); //insert into the database
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName); //write a cookie to the user machine
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1); //expire within 1 day
            httpContext.Response.Cookies.Add(cookie); //Add the cookie to the http session response
            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);  //because we are inserting a item we always need to make sure basket is created 
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                basket.BasketItems.Add(item);
            }
            else //if item is known, increment that property
            {
                item.Quantity = item.Quantity + 1;
            }
            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false); //Because we are only getting items, if the basket dont exists, we dont want to create

            if (basket != null)
            {
                var results =
                    (
                        from b in basket.BasketItems //from b (just inline variable) "IN" than we tell what is the first table we wish to query
                        join p in productContext.Collection() //join p and another list, this case produtctcontext that owen products in our database
                        on b.Id equals p.Id  //we than tell what we want to join the 2 tables on
                        select new BasketItemViewModel() //create new object and say what items will come from where
                        {
                            Id = b.Id,
                            Quantity = b.Quantity,
                            ProductName = p.Name,
                            Image = p.Image,
                            Price = p.Price
                        }
                    ).ToList();
                return results;
            }
            else
            {
                return new List<BasketItemViewModel>(); //if there is no basket we return a new empty list of basket items
            }
            
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0); //0,0 to start the construct, 0 items and 0 price
            if (basket != null) //if there is a basket, we have to do some calculation, first is how many items is in the basket
            {
                int? basketCount =//the ? means we can store a null value in here
                    (
                        from item in basket.BasketItems  //we are querying the basket items
                        select item.Quantity  //we select the quantity which is a integer
                    ).Sum(); //count the quantity value of every item in our basket item table, if there is none, it will return null
                decimal? basketTotal =
                    (
                        from item in basket.BasketItems
                        join p in productContext.Collection()
                        on item.ProductId equals p.Id
                        select item.Quantity * p.Price
                    ).Sum();

                model.BasketCount = basketCount ?? 0; //?? means, if there is a value in basketCount, return it, else return 0
                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return model;
            }
        }
    }
}
