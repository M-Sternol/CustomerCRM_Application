using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Model.Warehouse
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            TotalPrice = product.Price * quantity;
            Name = product.Name;
            ProductId = product.Id;
        }
    }
}
