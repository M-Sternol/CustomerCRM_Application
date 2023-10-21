using System;
using System.Collections.Generic;
using Customer.Application.Services.WarehouseService;
using Customer.Domain.Model.Warehouse;
using Xunit;

namespace Customer.Tests
{
    public class InventoryManagementServiceTests
    {
        [Fact]
        public void AddProduct_AddsProductToList()
        {
            // Arrange
            var inventoryManagementService = new InventoryManagementService();
            var initialProductCount = inventoryManagementService.GetProducts().Count;
            var product = new Product
            {
                Name = "TestProduct",
                Category = "TestCategory",
                Quantity = 10,
                Price = 100
            };

            // Act
            inventoryManagementService.AddProduct(product);
            var updatedProductCount = inventoryManagementService.GetProducts().Count;

            // Assert
            Assert.Equal(initialProductCount + 1, updatedProductCount);
        }

        [Fact]
        public void RemoveProduct_RemovesProductFromList()
        {
            // Arrange
            var inventoryManagementService = new InventoryManagementService();
            var product = new Product
            {
                Name = "TestProduct",
                Category = "TestCategory",
                Quantity = 10,
                Price = 100
            };
            inventoryManagementService.AddProduct(product);
            var initialProductCount = inventoryManagementService.GetProducts().Count;

            // Act
            inventoryManagementService.RemoveProduct(product.Id);
            var updatedProductCount = inventoryManagementService.GetProducts().Count;

            // Assert
            Assert.Equal(initialProductCount - 1, updatedProductCount);
        }

        [Fact]
        public void AddItemToCart_DecrementsProductQuantity()
        {
            // Arrange
            var inventoryManagementService = new InventoryManagementService();
            var product = new Product
            {
                Name = "TestProduct",
                Category = "TestCategory",
                Quantity = 10,
                Price = 100
            };
            inventoryManagementService.AddProduct(product);
            var initialQuantity = product.Quantity;
            var quantityToReduce = 5;

            // Act
            inventoryManagementService.AddItemToCart(product.Id, quantityToReduce);
            var updatedProduct = inventoryManagementService.GetProductById(product.Id);

            // Assert
            Assert.Equal(initialQuantity - quantityToReduce, updatedProduct.Quantity);
        }

        [Fact]
        public void UpdateProduct_UpdatesProductDetails()
        {
            // Arrange
            var inventoryManagementService = new InventoryManagementService();
            var product = new Product
            {
                Name = "TestProduct",
                Category = "TestCategory",
                Quantity = 10,
                Price = 100
            };
            inventoryManagementService.AddProduct(product);
            var updatedProduct = new Product
            {
                Id = product.Id,
                Name = "UpdatedProduct",
                Category = "UpdatedCategory",
                Quantity = 20,
                Price = 200
            };

            // Act
            inventoryManagementService.UpdateProduct(product.Id, updatedProduct);
            var retrievedProduct = inventoryManagementService.GetProductById(product.Id);

            // Assert
            Assert.Equal(updatedProduct.Name, retrievedProduct.Name);
            Assert.Equal(updatedProduct.Category, retrievedProduct.Category);
            Assert.Equal(updatedProduct.Quantity, retrievedProduct.Quantity);
            Assert.Equal(updatedProduct.Price, retrievedProduct.Price);
        }

        [Fact]
        public void SearchProductsByName_ReturnsMatchingProducts()
        {
            // Arrange
            var inventoryManagementService = new InventoryManagementService();
            var productName = "TestProduct";
            var product1 = new Product
            {
                Name = productName,
                Category = "TestCategory",
                Quantity = 10,
                Price = 100
            };
            var product2 = new Product
            {
                Name = "AnotherProduct",
                Category = "TestCategory",
                Quantity = 5,
                Price = 50
            };
            inventoryManagementService.AddProduct(product1);
            inventoryManagementService.AddProduct(product2);

            // Act
            var searchResults = inventoryManagementService.SearchProductsByName(productName);

            // Assert
            Assert.Collection(searchResults,
                item => Assert.Equal(productName, item.Name));
        }

        [Fact]
        public void SearchProductsByCategory_ReturnsMatchingProducts()
        {
            // Arrange
            var inventoryManagementService = new InventoryManagementService();
            var productCategory = "TestCategory";
            var product1 = new Product
            {
                Name = "TestProduct",
                Category = productCategory,
                Quantity = 10,
                Price = 100
            };
            var product2 = new Product
            {
                Name = "AnotherProduct",
                Category = "AnotherCategory",
                Quantity = 5,
                Price = 50
            };
            inventoryManagementService.AddProduct(product1);
            inventoryManagementService.AddProduct(product2);

            // Act
            var searchResults = inventoryManagementService.SearchProductsByCategory(productCategory);

            // Assert
            Assert.Collection(searchResults,
                item => Assert.Equal(productCategory, item.Category, StringComparer.OrdinalIgnoreCase));
        }


        [Fact]
        public void GetNextProductId_ReturnsNextAvailableId()
        {
            // Arrange
            var inventoryManagementService = new InventoryManagementService();
            var initialId = inventoryManagementService.GetNextProductId();

            // Act
            var newId = inventoryManagementService.GetNextProductId();

            // Assert
            Assert.Equal(initialId + 1, newId);
        }


    }
}
