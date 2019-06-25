using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingOnline.Models
{
    public class CartItem
    {
        public Product product { get; set; }
        public int quantity { get; set; }

        public double totalprice { get { return product.price * quantity; } }

        public CartItem() { }
        
        public CartItem(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }
    }
}