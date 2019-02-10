using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTrackerMVC.Models
{
    public partial class ShoppingListHistory
    {
        public ShoppingListHistory()
        {
            ShoppingList = new HashSet<ShoppingList>();
        }

        [Key]
        public int IdList { get; set; }
        public string IdUser { get; set; }

        public User IdUserNavigation { get; set; }
        public ICollection<ShoppingList> ShoppingList { get; set; }
    }
}
