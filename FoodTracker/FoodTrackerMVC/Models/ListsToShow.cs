using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrackerMVC.Models
{
    public class ListsToShow
    {
        [Key]
        public int IdList { get; set; }
        public int? FkProduct { get; set; }
        public string FkUser { get; set; }
        public string product_name { get; set; }
        public int id_list_for_user { get; set; }
    }
}
