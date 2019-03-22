using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class ColorController : ApiController
    {
        // GET: api/Color
        public object Get()
        {
            List<RAMFix> fixs = new List<RAMFix>();
            RAMFix fix = new RAMFix() { info = new List<FixInfo>() };
            FixInfo info = new FixInfo();
            var list = MySqlUnitity.Ins.Query<RAMFixModel>("SELECT * from tb_RAMfix ORDER BY sortIndex");
            foreach (var item in list)
            {
                fix = fixs.FirstOrDefault(a => a.phoneCode == item.phoneCode);
                if (fix != null)
                {
                    info = new FixInfo();
                    info.id = item.id;
                    info.phoneCode = item.phoneCode;
                    info.sortIndex = item.sortIndex;
                    info.fixType = item.fixType;
                    info.fixPrice = item.fixPrice.ToString();
                    fix.info.Add(info);
                }
                else
                {
                    fix = new RAMFix() { info = new List<FixInfo>(),sortIndex=item.sortIndex };
                    fix.phoneCode = item.phoneCode;
                    info = new FixInfo();
                    info.id = item.id;
                    info.phoneCode = item.phoneCode;
                    info.sortIndex = item.sortIndex;
                    info.fixType = item.fixType;
                    info.fixPrice = item.fixPrice.ToString();
                    fix.info.Add(info);
                    fixs.Add(fix);
                }
            }
            return fixs;
        }

        // GET: api/Color/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Color
        public IEnumerable<Models> Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string bland = obj["bland"].ToString();
            string ver = obj["ver"].ToString();
            var list = MySqlUnitity.Ins.Query<Models>(string.Format("SELECT DISTINCT IFNULL(yanse,'通用') as 'name' from sjxh where sjpp='{0}' AND sjxh='{1}'", bland, ver));
            return list;
        }

        // PUT: api/Color/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Color/5
        public void Delete(int id)
        {
        }
    }
}
