using System;
using System.Collections.Generic;
using DiscountCart.Discounts;
using DiscountCart.Helpers;
using DiscountCart.Models;

namespace DiscountCart
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var appleProduct = new Product("apple", 10.0);
            var orangeProduct = new Product("orange", 10.0);
            
            var priceCalculator = new PriceCalculator(new List<IDiscount>
            {
                new TwoForOneDiscount()
            });
            
            var basket = new Basket();
            
            basket.AddProduct(appleProduct);
            basket.AddProduct(appleProduct); 
            basket.AddProduct(orangeProduct);
            basket.AddProduct(orangeProduct);

            var basketPrice = priceCalculator.CalculatePrice(basket);
            Console.WriteLine($"Calculate price for basket is {basketPrice}");
        }
    }
}