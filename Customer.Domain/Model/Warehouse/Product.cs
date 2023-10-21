using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain.Interfaces;

namespace Customer.Domain.Model.Warehouse
{
    public class Product : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = FormatCategory(value); }
        }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


        public List<string> GetAvailableCategories()
        {
            return new List<string>
        {
            "Elektronika",
            "Owoce",
            "Warzywa",
            "Chemia",
            "Motoryzacja"
        };
        }
        public bool IsCategoryValid(string category)
        {
            var availableCategories = GetAvailableCategories();
            return availableCategories.Any(c => string.Equals(c, category, StringComparison.OrdinalIgnoreCase));
        }
        private string FormatCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return category;
            }
            return char.ToUpper(category[0]) + category.Substring(1).ToLower();
        }
    }
}
