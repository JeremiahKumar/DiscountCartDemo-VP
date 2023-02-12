using System;
using System.Collections.Generic;
using DiscountCart.Calculators;
using DiscountCart.Constants;
using DiscountCart.Discounts;
using DiscountCart.Models;

namespace DiscountCart
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var appleProduct = new Product(ApplicationConstants.Apple, 10.0);
            var orangeProduct = new Product(ApplicationConstants.Orange, 10.0);
            
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