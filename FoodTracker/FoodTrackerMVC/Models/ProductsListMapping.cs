using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTrackerMVC.Models
{
    public partial class ProductsListMapping
    {
        [Key]
        public int IdMapping { get; set; }
        public int ProductFk { get; set; }
        public int ListFk { get; set; }

        public ProductList ListFkNavigation { get; set; }
        public Product ProductFkNavigation { get; set; }
    }
}
