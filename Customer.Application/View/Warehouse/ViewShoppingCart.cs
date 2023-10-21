using Customer.Application.Services.WarehouseService;
using Customer.DataStorage;
using Customer.Domain.Interfaces;
using Customer.Domain.Model.Warehouse;
using System;
using System.Xml.Linq;

namespace Customer.Application.View.Warehouse
{
    public class ViewShoppingCart
    {
        private IShoppingCart cart;
        private Product product = new Product();

        public ViewShoppingCart(IShoppingCart shoppingCart)
        {
            cart = shoppingCart;
        }

        public bool ShoppingCart()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj produkt do koszyka");
                Console.WriteLine("2. Wyświetl zawartość koszyka");
                Console.WriteLine("3. Usuń produkt z koszyka");
                Console.WriteLine("4. Wyczyść koszyk");
                Console.WriteLine("5. Zaktualizuj ilość produktu w koszyku");
                Console.WriteLine("6. Oblicz całkowitą wartość koszyka");
                Console.WriteLine("0. Wyjdź z programu");
                Console.Write("Wybierz numer opcji: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Dodawanie produktu do koszyka:");
                        AddProductToCart();
                        break;
                    case "2":
                        Console.WriteLine("Zawartość koszyka:");
                        DisplayCartItems();
                        break;
                    case "3":
                        Console.WriteLine("Usuwanie produktu z koszyka:");
                        RemoveProductFromCart();
                        break;
                    case "4":
                        Console.WriteLine("Czyszczenie koszyka...");
                        cart.ClearCart();
                        Console.WriteLine("Koszyk został wyczyszczony.");
                        break;
                    case "5":
                        Console.WriteLine("Aktualizacja ilości produktu w koszyku:");
                        UpdateProductInCart();
                        break;
                    case "6":
                        Console.WriteLine($"Całkowita wartość koszyka: {cart.CalculateTotal()}");
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                        break;
                }
            }
            return true;
        }

        private void AddProductToCart()
        {
            try
            {
                Console.Write("Podaj id produktu do dodania do koszyka: ");
                int productId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Podaj ilość produktu: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                InventoryManagementService inventoryService = new InventoryManagementService();
                Product product = inventoryService.GetProductById(productId);

                if (product != null)
                {
                    cart.AddItem(product, quantity);
                    inventoryService.AddItemToCart(productId, quantity);
                    Console.WriteLine($"Produkt {product.Name} dodany do koszyka.");
                    LogToFileMessage.LogSuccess($"Add Item Success: {product.Name} {quantity}");
                }
                else
                {
                    Console.WriteLine($"Produkt o id {productId} nie został znaleziony.");
                }
            }
            catch { }
        }


        private void RemoveProductFromCart()
        {
            try
            {
                Console.Write("Podaj id produktu do usunięcia z koszyka: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                cart.RemoveItem(productId);
                Console.WriteLine($"Produkt o id {productId} usunięty z koszyka.");
                LogToFileMessage.LogSuccess($"Remove Item Success: {product.Name} {product.Quantity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Remove Product From Cart: {ex.Message}");
                LogToFileMessage.LogError($"Error Remove Product From Cart: {ex.Message}", ex.StackTrace);
            }
        }

        private void UpdateProductInCart()
        {
            try
            {
                Console.Write("Podaj id produktu do zaktualizowania w koszyku: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Podaj nową ilość produktu: ");
                int newQuantity = Convert.ToInt32(Console.ReadLine());
                cart.UpdateItemQuantity(productId, newQuantity);
                Console.WriteLine($"Produkt o id {productId} zaktualizowany. Nowa ilość: {newQuantity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Update Product In Cart: {ex.Message}");
                LogToFileMessage.LogError($"Error Update Product In Cart: {ex.Message}", ex.StackTrace);
            }
        }

        private void DisplayCartItems()
        {
            try
            {
                var cartItems = cart.GetCartItems();
                if (cartItems.Any())
                {
                    Console.WriteLine("Produkty w koszyku:");
                    foreach (var item in cartItems)
                    {
                        Console.WriteLine($"Nazwa: {item.Name}, Cena: {item.Price}, Ilość: {item.Quantity}");
                    }
                }
                else
                {
                    Console.WriteLine("Koszyk jest pusty.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Display Cart Items: {ex.Message}");
                LogToFileMessage.LogError($"Error Display Cart Items: {ex.Message}", ex.StackTrace);
            }

        }

    }
}
