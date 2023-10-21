using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Customer.DataStorage;
using Customer.Domain.Model.Warehouse;
using Newtonsoft.Json;

namespace Customer.Application.Services.WarehouseService
{
    public class InventoryManagementService
    {
        private List<Product> products;
        private int currentId;
        private readonly string filePath = FileLocations.GetWarehouseFilePath();

        public InventoryManagementService()
        {
            products = new List<Product>();
            LoadProductsFromJson();
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public void LoadProductsFromJson()
        {
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                products = JsonConvert.DeserializeObject<List<Product>>(fileContent) ?? new List<Product>();
                currentId = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            }
        }

        public int GetNextProductId()
        {
            return currentId++;
        }
        public Product GetProductById(int productId)
        {
            return products.FirstOrDefault(p => p.Id == productId);
        }

        public void AddProduct(Product product)
        {
            product.Id = GetNextProductId();
            products.Add(product);
            SaveProductsToJson();
        }
        public void AddItemToCart(int productId, int quantity)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                if (product.Quantity >= quantity)
                {
                    product.Quantity -= quantity;
                    SaveProductsToJson();
                }
                else
                {
                    Console.WriteLine("Niewystarczająca ilość produktu w magazynie.");
                }
            }
        }
        public void RemoveProduct(int productId)
        {
            var productToRemove = products.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                SaveProductsToJson();
            }
        }

        public void UpdateProduct(int productId, Product updatedProduct)
        {
            var productToUpdate = products.FirstOrDefault(p => p.Id == productId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = updatedProduct.Name;
                productToUpdate.Category = updatedProduct.Category;
                productToUpdate.Quantity = updatedProduct.Quantity;
                productToUpdate.Price = updatedProduct.Price;
                SaveProductsToJson();
            }
        }

        public List<Product> SearchProductsByName(string name)
        {
            return products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public List<Product> SearchProductsByCategory(string category)
        {
            return products.Where(p => p.Category.ToLower().Contains(category.ToLower())).ToList();
        }

        private void SaveProductsToJson()
        {
            string productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            try
            {
                File.WriteAllText(filePath, productsJson);
                Console.WriteLine("Dane zostały zapisane do pliku Products.json.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd podczas zapisywania danych do pliku: {e.Message}");
            }
        }

        public void DisplayProductDetails(Product product)
        {
            Console.WriteLine("==========================================================================================");
            Console.WriteLine($"Identyfikator: {product.Id}, Nazwa Produktu: {product.Name}, Kategoria: {product.Category}, Ilość: {product.Quantity}, Cena: {product.Price}");
            Console.WriteLine("==========================================================================================");
        }
    }
}