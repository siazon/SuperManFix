using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class OrderNoticeController : ApiController
    {
        // GET: api/OrderNotice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OrderNotice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OrderNotice
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/OrderNotice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OrderNotice/5
        public void Delete(int id)
        {
        }
    }
}
