using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AboutVintageApiService.DBUtils;
using AboutVintageWebAppLibrary;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AboutVintageApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        ManageComplaint _manageComplaint = new ManageComplaint();

        // GET: api/<ComplaintsController>
        [HttpGet]
        public IEnumerable<Complaint> Get()
        {
            return _manageComplaint.GetAllComplaints();
        }

        // GET api/<ComplaintsController>/5
        [HttpGet("{id}")]
        public Complaint Get(int id)
        {
            return _manageComplaint.GetComplaintFromId(id);
        }

        // POST api/<ComplaintsController>
        [HttpPost]
        public bool Post([FromBody] Complaint complaint)
        {
            return _manageComplaint.CreateComplaint(complaint);
        }

        // PUT api/<ComplaintsController>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Complaint complaint)
        {
            return _manageComplaint.UpdateComplaint(complaint, id);
        }

        // DELETE api/<ComplaintsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _manageComplaint.DeleteComplaint(id);
        }
    }
}
