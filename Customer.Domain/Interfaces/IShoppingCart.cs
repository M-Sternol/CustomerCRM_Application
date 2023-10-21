using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain.Model.Warehouse;

namespace Customer.Domain.Interfaces
{
    public interface IShoppingCart
    {
        void AddItem(Product product, int quantity);
        void RemoveItem(int productId);
        void ClearCart();
        List<Product> GetCartItems();
        decimal CalculateTotal();
        void UpdateItemQuantity(int productId, int newQuantity);
    }
}
