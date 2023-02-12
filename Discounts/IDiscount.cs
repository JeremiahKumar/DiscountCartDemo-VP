using System.Collections.Generic;
using DiscountCart.Models;

namespace DiscountCart.Discounts
{
    public interface IDiscount
    {
        bool Active { get; }
        List<string> AppliesTo { get; }
        int MinimumQuantityRequired { get; }
        double CalculateDiscount(List<Product> applicableProducts);
    }
}