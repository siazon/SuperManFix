using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class recycleController : ApiController
    {
        // GET: api/recycle
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/recycle/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/recycle
        public int Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string userName = obj["userName"].ToString();
            string phone = obj["phone"].ToString();
            string addr = obj["addr"].ToString();
            List<tb_recycle> tbrec = MySqlUnitity.Ins.Query<tb_recycle>(string.Format("SELECT * from tb_recycle where phone ='{0}' and create_time >'{1}'",
                phone, DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH-mm-ss")));
            if (tbrec != null && tbrec.Count > 0)
            {
                return 999;
            }
            int i = DbManager.Ins.ExecuteNonquery(string.Format("INSERT into tb_recycle (userName,phone, addr,create_time)VALUES('{0}','{1}','{2}','{3}')",
                  userName, phone, addr, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")));
            return i;
        }

        // PUT: api/recycle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/recycle/5
        public void Delete(int id)
        {
        }
    }
}
