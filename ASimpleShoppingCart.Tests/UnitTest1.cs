using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASimpleShoppingCart.Models;

namespace ASimpleShoppingCart.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public static ShoppingCartModel ShCart = new ShoppingCartModel();
        public ProductModel Item = new ProductModel();
        [TestMethod]
        public void AddItemTest()
        {
            SetItems("Test",10,10);
            string Name = (string)Item.ProductName;
            decimal Quantity = (decimal)Item.ProductQuantity;
            decimal Price = (decimal)Item.ProductPrice;
            ShCart.AddItemToCart(Name, Price, Quantity);
            ShCart.AddItemToCart(Name, Price, Quantity);
            decimal TestPrice = ShCart.TotalPriceOfItems;
            
            

            Assert.AreEqual(200, ShCart.TotalPriceOfItems);
            
        }
        public void SetItems(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            Item = new ProductModel();
            Item.ProductName = ItemName;
            Item.ProductPrice = PriceOfItem;
            Item.ProductQuantity = QuantityOfItem;

        }

        [TestMethod]
        public void RemoveItemTest()
        {
            SetItems("Test", 10, 10);
            string Name = (string)Item.ProductName;
            decimal Quantity = (decimal)Item.ProductQuantity;
            decimal Price = (decimal)Item.ProductPrice;
            ShCart.TotalPriceOfItems = 0;
            ShCart.AddItemToCart(Name, Price, Quantity);
            ShCart.AddItemToCart(Name, Price, Quantity);
            ShCart.AddItemToCart("Name", 10, 10);
            ShCart.RemoveItemFromCart(Name, Price, Quantity);
            ShCart.RemoveItemFromCart(Name, Price, Quantity);
           

            Assert.AreEqual(0, ShCart.TotalPriceOfItems);

        }
    }
}
