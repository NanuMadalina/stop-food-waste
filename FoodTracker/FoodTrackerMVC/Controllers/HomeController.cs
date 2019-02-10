using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTrackerMVC.Models;
using FoodTrackerMVC.Data;

namespace FoodTrackerMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            return View();
        }


        public IActionResult ShoppingList()
        {
            var model = new List<Product>();
            model.Add(new Product { product_name = "banana" });
            model.Add(new Product { product_name = "coke" });
            model.Add(new Product { product_name = "danonino" });
            model.Add(new Product { product_name = "soup" });
            model.Add(new Product { product_name = "kiwi" });

            return PartialView("_ShoppingList");
        }


        public IActionResult Contact()
        {
            

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
