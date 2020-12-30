using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace AboutVintageWebAppLibrary
{
    public class ComplaintModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string OrderNumber { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string ProductVariant { get; set; }
        [Required]
        [Range(1, 5000)]
        public int WatchNumber { get; set; }
        [Required]
        public string WatchProducer { get; set; }
        [Required]
        public string Error { get; set; }
        [Required]
        public string Solution { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string ProductType { get; set; }
        public bool IsEditing { get; set; } = false;
    }
}
