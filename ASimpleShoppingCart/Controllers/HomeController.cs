using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASimpleShoppingCart.Models;

namespace ASimpleShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        public static ShoppingCartModel Cart = new ShoppingCartModel();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            AddViewData();
            return View();
        }

        [HttpGet]
        public PartialViewResult AddToCart(string name, decimal quantity)
        {
            string Name = name;
            decimal Quantity = quantity;
            return AddSelectedItemToCart(name, quantity);
        }

        [HttpPost]
        public PartialViewResult AddToCart(ProductModel Item)
        {
            string Name = (string)Item.ProductName;
            decimal Quantity = (decimal)Item.ProductQuantity;
            decimal Price = (decimal)Item.ProductPrice;
            if (Cart.ItemsInCart.ContainsKey(Name))
            {
                decimal OldQuantity = Cart.ItemsInCart[Name]["Quantity"];
                if (Quantity > OldQuantity)
                {
                    Quantity -= OldQuantity;
                }
                else
                {
                    Quantity = OldQuantity - Quantity;
                }
            }
            Cart.AddItemToCart(Name, Price, Quantity);
            AddViewData();


            return PartialView("_CartDetails");
        }

        public PartialViewResult RemoveFromCart(String Item)
        {
            if (Cart.ItemsInCart.ContainsKey(Item))
            {
               return RemoveSelectedItemFromCart(Item);
            }
            
            return PartialView("_CartDetails");
        }

        public PartialViewResult AddItemToCart(String Item)
        {
            if (Cart.ItemsInCart.ContainsKey(Item))
            {
               return  AddSelectedItemToCart(Item);
            }
            return PartialView("_CartDetails");
        }

        public PartialViewResult RemoveAll(String Item)
        {
            if (Cart.ItemsInCart.ContainsKey(Item))
            {
                RemoveAllItemsInCart(Item);   
            }
            return PartialView("_CartDetails");
        }

        public void AddViewData()
        {
            ViewBag.Error = (Cart.Error == "") ? "" : Cart.Error;
            ViewBag.Message = (Cart.Message == "") ? "" : Cart.Message;
            Cart.Error = "";
            Cart.Message = "";
            ViewBag.Itemz = Cart.ItemsInCart;
            ViewBag.Totalprice = Cart.TotalPriceOfItems;
        }

        public PartialViewResult AddSelectedItemToCart(string Item, decimal quantity = 1)
        {
            string Name = (string)Cart.ItemsInCart[Item]["Name"];
            decimal Quantity = 1;
            decimal Price = (decimal)Cart.ItemsInCart[Item]["Price"];
            Cart.AddItemToCart(Name, Price, Quantity);
            AddViewData();
            return PartialView("_CartDetails");
        }
        public PartialViewResult RemoveSelectedItemFromCart(string Item)
        {
            string Name = (string)Cart.ItemsInCart[Item]["Name"];
            decimal Quantity = 1;
            decimal Price = (decimal)Cart.ItemsInCart[Item]["Price"];
            Cart.RemoveItemFromCart(Name, Price, Quantity);
            AddViewData();
            return PartialView("_CartDetails");
         }

        public PartialViewResult RemoveAllItemsInCart(String Item)
        {
            string Name = (string)Cart.ItemsInCart[Item]["Name"];
            decimal Quantity = (decimal)Cart.ItemsInCart[Item]["Quantity"];
            decimal Price = (decimal)Cart.ItemsInCart[Item]["Price"];
            Cart.ClearCart(Name, Price, Quantity);
            AddViewData();
            return PartialView("_CartDetails");
        }
	}
}