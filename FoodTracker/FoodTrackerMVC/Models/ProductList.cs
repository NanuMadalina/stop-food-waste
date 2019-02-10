using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTrackerMVC.Models
{
    public partial class ProductList
    {
        public ProductList()
        {
            ProductsListMapping = new HashSet<ProductsListMapping>();
        }
        [Key]
        public int IdList { get; set; }
        public int? FkProduct { get; set; }
        public string FkUser { get; set; }

        public Product FkProductNavigation { get; set; }
        //public AspNetUsers FkUserNavigation { get; set; }
        public ICollection<ProductsListMapping> ProductsListMapping { get; set; }
    }
}
