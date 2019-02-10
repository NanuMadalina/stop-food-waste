using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace FoodTrackerMVC.Models
{
    public class Product
    {
		[Key]
		public int id_product { get; set; }
		[DisplayName("Name")]
		public string product_name { get; set; }
		[DisplayName("Description")]
		public string product_description { get; set; }
		[DisplayName("Quantity")]
		public double quantity { get; set; }
		[DisplayName("Measurement unit")]
		public string unit_of_measurement { get; set; }
		[DisplayName("Category")]
		public int id_category { get; set; }
		[DisplayName("Valability")]
		public string valability { get; set; }
		[DisplayName("Frequency_usage")]
		public string frequency_usage { get; set; }
        [DisplayName("Kcal")]
        public int Kcal { get; set; }
	}
}

