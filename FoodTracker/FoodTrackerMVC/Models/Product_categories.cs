using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrackerMVC.Models
{
	public class Product_categories
	{
		[System.ComponentModel.DataAnnotations.Key]
		public int id_category { get; set; }
		public string category_name { get; set; }

	}
}
