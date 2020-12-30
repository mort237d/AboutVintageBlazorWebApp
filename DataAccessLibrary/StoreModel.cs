using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AboutVintageBlazorWebApp.Data
{
    public class StoreModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StoreName { get; set; }
        public float? RevenueCash { get; set; }
        public float? RevenueMobilePay { get; set; }
        public float? RevenueCreditCard { get; set; }
        public int? CustomerCount { get; set; }
        public bool IsEditing { get; set; } = false;
    }
}
