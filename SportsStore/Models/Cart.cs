using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Cart
    {
        public List<CartLine> CartLines { get; set; } = new List<CartLine>();

        public void AddItems(Product product, int quantity) 
        {
            var line = CartLines.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (line == null)
            {
                CartLines.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveItems(Product product, int quantity)
        {
            var line = CartLines.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (line != null)
            {
                if (quantity >= line.Quantity)
                {
                    CartLines.Remove(line);
                }
                else
                {
                    line.Quantity -= quantity;
                }
            }
        }

        public void Clear()
        {
            CartLines.Clear();
        }

        public decimal TotalValue => CartLines.Sum(x => x.Value);
    }
}
