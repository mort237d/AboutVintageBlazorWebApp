using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AboutVintageAPI.Model
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CustomerCount> CustomerCounts { get; set; }
    }
}
