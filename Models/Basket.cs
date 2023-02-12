using System;
using System.Collections.Generic;
using DiscountCart.Constants;

namespace DiscountCart.Models
{
    public class Basket
    {
        public readonly List<Product> Items = new List<Product>();
        public double TotalPrice;

        public void AddProduct(Product product)
        {
            if (product.Quantity > 1)
            {
                throw new ArgumentException(ExceptionMessages.MoreThanOneQuantityException);
            }
            
            if (Items.Contains(product))
            {
                var item = Items.Find(p => p.Name == product.Name);
                item.Quantity++;
            }
            else
            {
                Items.Add(product);
            }
            
            TotalPrice += product.Price;
        }

        public void RemoveProduct(Product product)
        {
            if (product.Quantity > 1)
            {
                throw new ArgumentException(ExceptionMessages.MoreThanOneQuantityException);
            }
            
            if (!Items.Contains(product)) return;

            var item = Items.Find(p => p.Name == product.Name);
            TotalPrice -= item.Price;
            
            if (--item.Quantity == 0)
            {
                Items.Remove(product);
            }
        }
    }
}