using System;
using System.Collections.Generic;

namespace FoodTrackerMVC.Models
{
    public partial class FinalDataToSendDtoes
    {
        public long IdDataToSend { get; set; }
        public string IdUser { get; set; }
        public string AgeUser { get; set; }
        public string GenderUser { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int ListIdForUser { get; set; }
    }
}
