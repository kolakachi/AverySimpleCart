using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ASimpleShoppingCart.Models
{
    public class ProductModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        [Required]
        [Display(Name = "Product Quantity")]
        public decimal ProductQuantity { get; set; }
    }
}