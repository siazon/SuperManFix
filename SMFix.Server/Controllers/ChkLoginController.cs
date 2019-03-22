using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class ChkLoginController : ApiController
    {
        // GET: api/ChkLogin
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ChkLogin/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChkLogin
        public int Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string OPType = obj["OPType"].ToString();
            string openId = obj["openId"].ToString();
            string nickName = obj["nickName"].ToString();
            int i = 0;
            if (OPType == "1")
            {
                i = DbManager.Ins.ExecuteNonquery(string.Format(@"INSERT INTO tb_user (openID,nickName,createTime) 
                                            SELECT '{0}','{1}','{2}' FROM DUAL WHERE	NOT EXISTS (	
                                            SELECT	openID	FROM tb_user WHERE openID = '{0}');",
                               openId, nickName, DateTime.Now));
            }
            else
            {
                DataTable dt = DbManager.Ins.ExcuteDataTable(string.Format("SELECT * from tb_user where openID='{0}'", openId));
                if (dt != null && dt.Rows.Count > 0)
                {
                    i = dt.Rows.Count;
                }
                else
                    i = 0;
            }
            return i;
        }

        // PUT: api/ChkLogin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChkLogin/5
        public void Delete(int id)
        {
        }
    }
}
