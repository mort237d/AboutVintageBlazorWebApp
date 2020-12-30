using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AboutVintageApiService.DBUtils;
using AboutVintageBlazorWebApp.Data;
using AboutVintageWebAppLibrary;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AboutVintageApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        ManageStore _manageStore = new ManageStore();

        // GET: api/<StoresController>
        [HttpGet]
        public IEnumerable<Store> Get()
        {
            return _manageStore.GetAllStore();
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public Store Get(int id)
        {
            return _manageStore.GetStoreFromId(id);
        }

        // GET api/<StoresController>/storeName/date
        [HttpGet("range/{storeName}")]
        public IEnumerable<Store> Get(string storeName, [FromQuery] DateTime minDate, [FromQuery] DateTime maxDate)
        {
            return _manageStore.GetStoreFromNameAndDate(minDate, maxDate, storeName);
        }

        // GET api/<StoresController>/storeName/date
        [HttpGet("single/{storeName}")]
        public IEnumerable<Store> Get(string storeName)
        {
            return _manageStore.GetStoreFromName(storeName);
        }

        // GET api/<StoresController>/storeName/date
        [HttpGet("topten")]
        public IEnumerable<Store> GetTopTen([FromQuery] string storeName)
        {
            return _manageStore.GetTenStore(storeName);
        }

        // POST api/<StoresController>
        [HttpPost]
        public bool Post([FromBody] Store value)
        {
            return _manageStore.CreateStore(value);

            //Store selectedStore = stores.SingleOrDefault(s => s.Name.Equals("Flagship"));
            //if (selectedStore != null)
            //{
            //    selectedStore.CustomerCounts.Add(new CustomerCount { DateTime = DateTime.Now, Count = value });
            //}
        }

        [HttpPost]
        [Route("nygade")]
        public void PostNygade([FromBody] int value)
        {
            //Store selectedStore = stores.SingleOrDefault(s => s.Name.Equals("Nygade"));
            //if (selectedStore != null)
            //{
            //    selectedStore.CustomerCounts.Add(new CustomerCount { DateTime = DateTime.Now, Count = value });
            //}
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Store value)
        {
            return _manageStore.UpdateStore(value, id);
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _manageStore.DeleteStore(id);
        }
    }
}
