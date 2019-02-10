using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTrackerMVC.Models
{
    public partial class ShoppingList
    {
        [Key]
        public int IdShoppingList { get; set; }
        public int IdListHistory { get; set; }
        public int IdProduct { get; set; }

        public ShoppingListHistory IdListHistoryNavigation { get; set; }
        public Product IdProductNavigation { get; set; }
    }
}
