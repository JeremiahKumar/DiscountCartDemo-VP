using System;

namespace DiscountCart.Models
{
    public class Product : IEquatable<Product> {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public double Price { get; set; }
        
        public int Quantity { get; set; } = 1;
        
        public override int GetHashCode() {
            return Name.GetHashCode();
        }
        public override bool Equals(object obj) {
            return Equals(obj as Product);
        }
        public bool Equals(Product obj) {
            return obj != null && obj.Name == Name;
        }
    }
}