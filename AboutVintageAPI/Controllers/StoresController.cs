using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AboutVintageAPI.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AboutVintageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private static List<Store> _stores = new List<Store>
        {
            new Store
            {
                Id = 1,
                Name = "Flagship",
                CustomerCounts = new List<CustomerCount>
                {
                    new CustomerCount{DateTime = DateTime.Now, Count = 2}
                }
            }
        };
        
        // GET: api/<StoresController>
        [HttpGet]
        public IEnumerable<Store> Get()
        {
            return _stores;
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public Store Get(int id)
        {
            return _stores.Find(s=> s.Id == id);
        }

        // POST api/<StoresController>
        [HttpPost]
        public void Post([FromBody] Store store)
        {
            _stores.Add(store);
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Store value)
        {
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _stores.RemoveAt(_stores.IndexOf(Get(id)));
        }
    }
}
