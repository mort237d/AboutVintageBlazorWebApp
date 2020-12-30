using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccessLibrary.Models
{
    public class OrderComplaint
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Variant> Variants { get; set; } = new List<Variant>();
        public Customer Customerer { get; set; }
    }
}
