using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System.Web.Mail;

namespace SMFix.Server.Controllers
{
    public class BlandController : ApiController
    {
        // GET: api/Bland
        public InitData Get()
        {
            InitData datas= new InitData();
            var blands = MySqlUnitity.Ins.Query<Models>(@"SELECT DISTINCT sjpp AS name from wxjm order by paixu;");
            datas.Bland = blands;
            if (blands.Count == 0) return datas;
            var vers = MySqlUnitity.Ins.Query<Models>(string.Format("SELECT DISTINCT IFNULL(sjxh,'通用') as 'name' from wxjm  where sjpp ='{0}' order by ksmjg", blands[0].name));
            datas.Ver = vers;
            if (vers.Count == 0) return datas;
            var colors = MySqlUnitity.Ins.Query<Models>(string.Format("SELECT DISTINCT IFNULL(yanse,'通用') as 'name' from sjxh where sjpp='{0}' AND sjxh='{1}'", blands[0].name, vers[0].name));
            datas.Color = colors;
            var faults = MySqlUnitity.Ins.Query<Fault>(string.Format("SELECT gzlx as name,ycjg as price from wxjm where sjpp='{0}' AND sjxh='{1}' ORDER BY mklx", blands[0].name, vers[0].name));
            datas.Fault = faults;

            return datas;
        }
       
        // GET: api/Bland/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bland 
        public IEnumerable<Models> Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string bland = obj["name"].ToString();
            var list = MySqlUnitity.Ins.Query<Models>(string.Format("SELECT DISTINCT sjxh as 'name' from wxjm where sjpp ='{0}' order by ksmjg", bland));
            return list;
        }

        // PUT: api/Bland/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bland/5
        public void Delete(int id)
        {
        }
    }
}
