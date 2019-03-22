using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Super.Website.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/Color")]
    public class ColorController : Controller
    {
        // GET: api/Color
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Color/5
        [HttpGet("{id}", Name = "GetColor")]
        public string GetColor(int id)
        {
            return "value";
        }
        
        // POST: api/Color
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Color/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
