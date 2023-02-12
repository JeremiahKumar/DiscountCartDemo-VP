using System.Collections.Generic;
using System.Linq;
using DiscountCart.Discounts;
using DiscountCart.Models;

namespace DiscountCart.Calculators
{
    public class PriceCalculator
    {
        private readonly IEnumerable<IDiscount> _activeDiscounts;

        public PriceCalculator(IEnumerable<IDiscount> discounts)
        {
            _activeDiscounts = discounts.Where(d => d.Active);
        }

        public double CalculatePrice(Basket basket)
        {
            foreach (var discount in _activeDiscounts)
            {
                var applicableItems = basket.Items.Where(p => discount.AppliesTo.Contains(p.Name)).ToList();
                var numberOfValidItems = applicableItems.Sum(p => p.Quantity);
                if (numberOfValidItems >= discount.MinimumQuantityRequired)
                {
                    basket.TotalPrice -= discount.CalculateDiscount(applicableItems);
                }
            }

            return basket.TotalPrice;
        }
    }
}