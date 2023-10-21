using Customer.Domain.Interfaces;
using Customer.Domain.Model.Warehouse;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Application.Services.WarehouseService
{
    public class ShoppingCartService : IShoppingCart
    {
        private List<CartItem> cartItems;

        public ShoppingCartService()
        {
            cartItems = new List<CartItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            var existingItem = cartItems.FirstOrDefault(item => item.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cartItems.Add(new CartItem(product, quantity));
            }
        }

        public void RemoveItem(int productId)
        {
            var itemToRemove = cartItems.FirstOrDefault(item => item.Product.Id == productId);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
            }
        }

        public void ClearCart()
        {
            cartItems.Clear();
        }

        public void UpdateItemQuantity(int productId, int newQuantity)
        {
            var itemToUpdate = cartItems.FirstOrDefault(item => item.Product.Id == productId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = newQuantity;
            }
        }

        public List<Product> GetCartItems()
        {
            return cartItems.Select(item => item.Product).ToList();
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.Product.Price * item.Quantity;
            }
            return total;
        }
    }
}
