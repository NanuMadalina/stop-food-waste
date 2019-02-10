using System;
using System.Collections.Generic;
using System.Text;
using FoodTrackerMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodTrackerMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FoodTrackerMVC.Models.Product> Product { get; set; }
		public DbSet<FoodTrackerMVC.Models.Product_categories> Product_categories { get; set; }
        public DbSet<FoodTrackerMVC.Models.Product_List> Product_List{ get; set; }
        public DbSet<FoodTrackerMVC.Models.ShoppingListHistory> ShoppingListHistory { get; set; }
        public DbSet<FoodTrackerMVC.Models.ShoppingList> ShoppingList { get; set; }
        public DbSet<FoodTrackerMVC.Models.ListsToShow> ListsToShow { get; set; }
        //public object AspNetUsers { get; internal set; }
        public DbSet<FoodTrackerMVC.Models.User> AspNetUsers { get; set; }

    }
}
