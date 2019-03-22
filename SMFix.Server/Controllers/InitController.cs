[using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class InitController : ApiController
    {
        // GET: api/Init
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Init/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Init
        public void Post([FromBody]object value)
        {
        }

        // PUT: api/Init/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Init/5
        public void Delete(int id)
        {
        }
    }
}
