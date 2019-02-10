using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrackerMVC.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }

    }
}
