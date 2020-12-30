using System;
using System.Collections.Generic;
using System.Text;

namespace AboutVintageWebAppLibrary
{
    public class Complaint
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductVariant { get; set; }
        public int WatchNumber { get; set; }
        public string WatchProducer { get; set; }
        public string Error { get; set; }
        public string Solution { get; set; }
        public string Comments { get; set; }
        public string ProductType { get; set; }
    }
}
