using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrackerMVC.Models
{
    public class Product_List
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id_list { get; set; }
        public int list_id_for_user { get; set; }
        public string fk_user { get; set; }
        public int fk_product { get; set; }
        public string product_name { get; set; }
    }
}
