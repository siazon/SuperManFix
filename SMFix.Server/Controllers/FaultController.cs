using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class FaultController : ApiController
    {
        // GET: api/Fault
        public object Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Fault/5
        public object Get(int id)
        {
            var list = MySqlUnitity.Ins.Query<tb_postAddr>(string.Format("SELECT * from tb_postAddr where addrType={0};",id));
            return list;
        }

        // POST: api/Fault
        public IEnumerable<Fault> Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string bland = obj["bland"].ToString();
            string ver = obj["ver"].ToString();
            var list = MySqlUnitity.Ins.Query<Fault>(string.Format("SELECT gzlx as name,ycjg as price from wxjm where sjpp='{0}' AND sjxh='{1}' ORDER BY mklx", bland, ver));
            foreach (var item in list)
            {
                if (item.name.Length>=6)
                {
                    item.name = item.name.Substring(0,6);
                }
            }
            return list;
        }

        // PUT: api/Fault/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Fault/5
        public void Delete(int id)
        {
        }
    }
}
