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
            var allDiscounts = new List<IDiscount>()
            {
                new TwoForOneDiscount()
            };
            
            var priceCalculator = new PriceCalculator(allDiscounts);
            
            var basket = new Basket();
            
            basket.AddProduct(new Product(ApplicationConstants.Apple, 10.0));
            basket.AddProduct(new Product(ApplicationConstants.Apple, 10.0)); 
            basket.AddProduct(new Product(ApplicationConstants.Apple, 10.0)); 
            basket.AddProduct(new Product(ApplicationConstants.Apple, 10.0)); 
            basket.AddProduct(new Product(ApplicationConstants.Orange, 10.0));
            basket.AddProduct(new Product(ApplicationConstants.Orange, 10.0));

            var basketPrice = priceCalculator.CalculatePrice(basket);
            Console.WriteLine($"Calculate price for basket is {basketPrice}");
        }
    }
}