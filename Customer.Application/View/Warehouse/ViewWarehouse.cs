using System;
using System.Collections.Generic;
using Customer.Domain.Model.Warehouse;
using Customer.Application.Helpers;
using Customer.Application.Helpers.Utils;
using Customer.DataStorage;
using Customer.Application.Services.WarehouseService;

namespace Customer.Application.View.Warehouse
{
    public class ViewWarehouse
    {
        private readonly InventoryManagementService inventory;
        private readonly Product product = new Product();
        private readonly Title title = new Title();
        private bool registrationCancelled = true;

        public ViewWarehouse()
        {
            inventory = new InventoryManagementService();
        }

        public bool Warehouse()
        {
            while (registrationCancelled)
            {
                title.ViewTitle();
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj produkt");
                Console.WriteLine("2. Dostępne Produkty");
                Console.WriteLine("3. Usuń produkt");
                Console.WriteLine("4. Zaktualizuj produkt");
                Console.WriteLine("5. Wyszukaj produkt po nazwie");
                Console.WriteLine("6. Wyszukaj produkt po kategorii");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz numer opcji: ");

                string choice = CheckForEscKey.ReadInput(ref registrationCancelled);

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AddProduct();
                        break;
                    case "2":
                        Console.Clear();
                        DisplayAllProducts();
                        break;
                    case "3":
                        Console.Clear();
                        RemoveProduct();
                        break;
                    case "4":
                        Console.Clear();
                        UpdateProduct();
                        break;
                    case "5":
                        Console.Clear();
                        SearchProductByName();
                        break;
                    case "6":
                        Console.Clear();
                        SearchProductByCategory();
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Wyjście");
                        registrationCancelled = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                        break;
                }
            }
            return true;
        }
        public bool SearchProduct()
        {
            title.ViewTitle();
            Console.WriteLine("Wybierz opcje wyszukiwania:");
            Console.WriteLine("1. Wyszukaj po Nazwie: ");
            Console.WriteLine("2. Wyszukaj po kategorii");
            string searchOperation = CheckForEscKey.ReadInput(ref registrationCancelled);
            switch (searchOperation)
            {
                case"1":
                    Console.Clear();
                    SearchProductByName();
                    break;
                case "2":
                    Console.Clear();
                    SearchProductByCategory();
                    break;
                case "0":
                    Console.Clear();
                    return false;
                default: Console.WriteLine("Nieprawidłowa operacja ! Spróbuj Ponownie!");
                    break;
            }
            return true;
        }
        private void AddProduct()
        {
            title.ViewTitle();
            try
            {
                Console.Write("Podaj nazwę produktu: ");
                string name = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!ValidationHelper.ValidateText(name))
                {
                    Console.WriteLine("Nieprawidłowa nazwa produktu. Spróbuj ponownie.");
                    return;
                }

                string category;
                do
                {
                    Console.Write("Podaj kategorię produktu (Elektronika/Owoce/Warzywa/Chemia/Motoryzacja): ");
                    category = CheckForEscKey.ReadInput(ref registrationCancelled);
                } while (!product.IsCategoryValid(category));

                Console.Write("Podaj ilość produktu: ");
                string quantityInput = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!int.TryParse(quantityInput, out int quantity))
                {
                    Console.WriteLine("Nieprawidłowy format liczby. Spróbuj ponownie.");
                    return;
                }

                Console.Write("Podaj cenę produktu: ");
                string priceInput = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!decimal.TryParse(priceInput, out decimal price))
                {
                    Console.WriteLine("Nieprawidłowy format liczby. Spróbuj ponownie.");
                    return;
                }

                Product newProduct = new Product
                {
                    Name = name,
                    Category = category,
                    Quantity = quantity,
                    Price = price
                };
                inventory.AddProduct(newProduct);
                LogToFileMessage.LogSuccess($"Add Item Success: {name} {quantity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error AddItems: {ex.Message}");
                LogToFileMessage.LogError($"Error AddItems: {ex.Message}", ex.StackTrace);
            }
        }

        private void RemoveProduct()
        {
            title.ViewTitle();
            try
            {
                Console.Write("Podaj ID produktu do usunięcia: ");
                string productIdInput = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!int.TryParse(productIdInput, out int productId))
                {
                    Console.WriteLine("Nieprawidłowy format ID. Spróbuj ponownie.");
                    return;
                }

                inventory.RemoveProduct(productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Remove Product: {ex.Message}");
                LogToFileMessage.LogError($"Error RemoveProduct: {ex.Message}", ex.StackTrace);
            }
        }

        private void UpdateProduct()
        {
            title.ViewTitle();
            try
            {
                Console.Write("Podaj ID produktu do zaktualizowania: ");
                string productIdInput = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!int.TryParse(productIdInput, out int productId))
                {
                    Console.WriteLine("Nieprawidłowy format ID. Spróbuj ponownie.");
                    return;
                }

                Console.Write("Podaj nową nazwę produktu: ");
                string name = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!ValidationHelper.ValidateText(name))
                {
                    Console.WriteLine("Nieprawidłowa nazwa produktu. Spróbuj ponownie.");
                    return;
                }

                string category;
                do
                {
                    Console.Write("Podaj nową kategorię produktu (Elektronika/Owoce/Warzywa/Chemia/Motoryzacja): ");
                    category = CheckForEscKey.ReadInput(ref registrationCancelled);
                } while (!product.IsCategoryValid(category));

                Console.Write("Podaj nową ilość produktu: ");
                string quantityInput = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!int.TryParse(quantityInput, out int quantity))
                {
                    Console.WriteLine("Nieprawidłowy format liczby. Spróbuj ponownie.");
                    return;
                }

                Console.Write("Podaj nową cenę produktu: ");
                string priceInput = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (!decimal.TryParse(priceInput, out decimal price))
                {
                    Console.WriteLine("Nieprawidłowy format liczby. Spróbuj ponownie.");
                    return;
                }

                Product updatedProduct = new Product
                {
                    Name = name,
                    Category = category,
                    Quantity = quantity,
                    Price = price
                };
                inventory.UpdateProduct(productId, updatedProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error UpdataProducts: {ex.Message}");
                LogToFileMessage.LogError($"Error UpdataProducts: {ex.Message}", ex.StackTrace);
            }
        }

        public void SearchProductByName()
        {
            try
            {
                title.ViewTitle();
                Console.Write("Wyszukaj produkt po nazwie: ");
                string name = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Przerwano!");
                    return;
                }
                var foundProducts = inventory.SearchProductsByName(name);
                DisplayProducts(foundProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SearchProductByName: {ex.Message}");
                LogToFileMessage.LogError($"Error SearchProductByName: {ex.Message}", ex.StackTrace);
            }
        }

        public void SearchProductByCategory()
        {
            try
            {
                title.ViewTitle();
                Console.Write("Wyszukaj produkt po kategorii: ");
                string category = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (string.IsNullOrEmpty(category))
                {
                    Console.WriteLine("Przerwano!");
                    return;
                }
                var foundProducts = inventory.SearchProductsByCategory(category);
                DisplayProducts(foundProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SearchProductByCategory: {ex.Message}");
                LogToFileMessage.LogError($"Error SearchProductByCategory: {ex.Message}", ex.StackTrace);
            }
        }

        private void DisplayProducts(List<Product> products)
        {
            if (products.Count > 0)
            {
                Console.WriteLine("Znalezione produkty: ");
                foreach (var product in products)
                {
                    inventory.DisplayProductDetails(product);
                }
            }
            else
            {
                Console.WriteLine("Brak pasujących produktów.");
            }
        }

        public bool DisplayAllProducts()
        {
            try
            {
                title.ViewTitle();
                Console.WriteLine("Dostępne produkty: ");
                foreach (var product in inventory.GetProducts())
                {
                    inventory.DisplayProductDetails(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DisplayAllProducts: {ex.Message}");
                LogToFileMessage.LogError($"Errpr DisplayAllProducts: {ex.Message}", ex.StackTrace);
            }
            return true;
        }
    }
}
