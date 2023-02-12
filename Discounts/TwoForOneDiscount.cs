using System.Collections.Generic;
using System.Linq;
using DiscountCart.Constants;
using DiscountCart.Models;

namespace DiscountCart.Discounts
{
    public class TwoForOneDiscount : IDiscount
    {
        public bool Active { get; set; } = true;

        public List<string> AppliesTo { get; set; } = new List<string>
        {
            ApplicationConstants.Apple,
            ApplicationConstants.Orange
        };

        public int MinimumQuantityRequired { get; set; } = 3;

        public double CalculateDiscount(List<Product> applicableProducts)
        {
            double discount = 0;
            var quantityToDiscount = applicableProducts.Sum(p => p.Quantity) / MinimumQuantityRequired;

            foreach (var product in applicableProducts)
            {
                if (product.Quantity >= quantityToDiscount)
                {
                    discount += quantityToDiscount * product.Price;
                    break;
                }

                discount += product.Quantity * product.Price;
                quantityToDiscount -= product.Quantity;
            }

            return discount;
        }
    }
}