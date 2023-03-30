using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using webBangHangOnline.Models.EF;


namespace webBangHangOnline.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> items { get; set; }

        public ShoppingCart()
        {
            this.items = new List<ShoppingCartItem>();
        }

        public void AddToCart(ShoppingCartItem item, int quantity)
        {
            var checkExits = items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExits != null)
            {
                checkExits.Quantity += quantity;
                checkExits.PriceTotal += checkExits.Price * checkExits.Quantity;
            }
            else
            {
                items.Add(item);
            }
        }

        public void Remove(int id)
        {
            var checkExits = items.FirstOrDefault(x=>x.ProductId== id);
            if (checkExits != null)
            {
                items.Remove(checkExits);
            }
        }

        public void updateQuantity(int id, int quantity)
        {
            var checkExits = items.FirstOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                checkExits.Quantity = quantity;
                checkExits.PriceTotal += checkExits.Price * checkExits.Quantity;
            }
        }
        

        public decimal GetTotalPrice()
        {
            return items.Sum(x => x.PriceTotal);
        }

        public decimal GetTotalQuantity()
        {
            return items.Sum(x => x.Quantity);
        }

        public void clearCart()
        {
            items.Clear();
        }
    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public string Alias { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set;}
    }
}