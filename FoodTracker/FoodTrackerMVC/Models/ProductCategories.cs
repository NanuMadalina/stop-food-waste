using System;
using System.Collections.Generic;

namespace FoodTrackerMVC.Models
{
    public partial class ProductCategories
    {
        public ProductCategories()
        {
            Product = new HashSet<Product>();
        }

        public int IdCategory { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
