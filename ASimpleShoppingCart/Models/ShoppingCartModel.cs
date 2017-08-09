using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASimpleShoppingCart.Models
{
    public class ShoppingCartModel
    {
        public decimal TotalPriceOfItems = 0;
        public Dictionary<string, Dictionary<string, dynamic>> ItemsInCart = new Dictionary<string, Dictionary<string, dynamic>>();
        
        public string Error = "";
        public string Message = "";

        //method to add item to the cart
        public void AddItemToCart(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            if (this.InputCheck(ItemName, PriceOfItem, QuantityOfItem))//validate input values
            {
                if ( ItemsInCart.ContainsKey(ItemName)) { UpdateCart(ItemName, PriceOfItem, QuantityOfItem); }

                else { AddANewItem(ItemName, PriceOfItem, QuantityOfItem); }
            }
            else{ Error = "Invalid Input";}
        }

        //method to remove item from cart
        public void RemoveItemFromCart(string ItemName,decimal PriceOfItem, decimal QuantityOfItem = 1)
        {
            bool InputIsCorrect = (this.InputCheck(ItemName, PriceOfItem, QuantityOfItem))
                                   == (ItemsInCart.ContainsKey(ItemName));
          
            if (InputIsCorrect){ RemoveItem(ItemName, PriceOfItem, QuantityOfItem); } else{ Error = "Invalid Input"; }
        }

        //method that takes in cash paid and displays the balance
        public void CheckOut(decimal cashPaid) {

            if (TotalPriceOfItems > cashPaid)
          {
              Error = "Cash paid is not enough";
        
          }
      
          else {

              decimal balance = cashPaid - TotalPriceOfItems;
              Message = "your balance is : "+balance;
        

          }
        }

        public void ClearCart(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            this.TotalPriceOfItems -= (QuantityOfItem * PriceOfItem);
            this.ItemsInCart.Remove(ItemName);
        }

        public void RemoveFromCart(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            this.TotalPriceOfItems -= (QuantityOfItem * PriceOfItem);
            this.ItemsInCart[ItemName]["Quantity"] -= QuantityOfItem;
            this.ItemsInCart[ItemName]["Subtotal"] -= (QuantityOfItem * PriceOfItem);
        }

        public bool QuantityIsMoreThanAvailability(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            bool Amount = (QuantityOfItem >= ItemsInCart[ItemName]["Quantity"]);
            return Amount;
        }

        public void RemoveItem(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            if (QuantityIsMoreThanAvailability(ItemName, PriceOfItem, QuantityOfItem))
            {
                ClearCart(ItemName, PriceOfItem, QuantityOfItem);
            }

            else
            {
                RemoveFromCart(ItemName, PriceOfItem, QuantityOfItem);
            }
        }

        public void UpdateCart(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            this.TotalPriceOfItems += (QuantityOfItem * PriceOfItem);

            this.ItemsInCart[ItemName]["Quantity"] += QuantityOfItem;
            this.ItemsInCart[ItemName]["Subtotal"] += (QuantityOfItem * PriceOfItem);
        }

        public void AddANewItem(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            this.TotalPriceOfItems += (QuantityOfItem * PriceOfItem);
            Dictionary<string, dynamic> SaleDetails = new Dictionary<string, dynamic>();
            SaleDetails.Add("Name", ItemName);
            SaleDetails.Add("Quantity", QuantityOfItem);
            SaleDetails.Add("Price", PriceOfItem);
            SaleDetails.Add("Subtotal", (QuantityOfItem * PriceOfItem));
            this.ItemsInCart.Add(ItemName, SaleDetails);
        }


        //method to check user input
        public bool InputCheck(string ItemName, decimal PriceOfItem, decimal QuantityOfItem)
        {
            bool ItemType = (ItemName.GetType() == typeof(string));
            bool QuantityOfItemType = (QuantityOfItem.GetType() == typeof(decimal));
            bool priceType = (QuantityOfItem.GetType() == typeof(decimal));
            bool validAmount = (QuantityOfItem > 0 && PriceOfItem > 0);

            if (ItemType && QuantityOfItemType && priceType && validAmount) { return true; } else { return false; }
        }

        


    }
}