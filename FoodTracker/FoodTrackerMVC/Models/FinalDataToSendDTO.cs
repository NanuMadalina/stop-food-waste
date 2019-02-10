using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrackerMVC.Models
{
    public class FinalDataToSendDTO
    {
        [Key]
        public long id_data_to_send { get; set; }
        public string id_user { get; set; }
        public string age_user { get; set; }
        public string gender_user { get; set; }
        public string product_name { get; set; }
        public string category_name { get; set; }
        public string quantity { get; set; }
        public string unit_of_measurement { get; set; }
        public int id_list_for_user { get; set; }
    }
}
