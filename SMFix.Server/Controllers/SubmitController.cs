using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class SubmitController : ApiController
    {
        // GET: api/Submit
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Submit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Submit
        public string Post([FromBody]object value)
        {
            int id = 0;
            List<weixiu> list = MySqlUnitity.Ins.Query<weixiu>(@"SELECT weixiuId from weixiu order by weixiuid DESC LIMIT 1;");
            if (list.Count>0)
            {
                int.TryParse(list[0].weixiuId,out id);
            }
            weixiu order = new weixiu();
            JObject datas = JObject.Parse(value.ToString());
            JObject datass = JObject.Parse(datas["datas"].ToString());
            order.weixiuId = (id+1).ToString();
            order.sjpp = datass["Bland"].ToString();
            order.sjxh = datass["Ver"].ToString();
            order.yanse = datass["Color"].ToString();
            order.sjgzlx = datass["Fault"].ToString();
            order.xingming = datass["UserName"].ToString();
            order.shoujihao = datass["Phone"].ToString();
            order.dizhi = datass["Addr"].ToString();
            order.wxfabj = datass["Amount"].ToString();
            order.fwlx = datass["ServerType"].ToString();
            order.timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            order.sjgzlxbz= datass["Time"].ToString()+ datass["Remark"].ToString();
            order.beizhu= datass["Remark"].ToString();//bakcAddr
            order.wxfa= "待处理";
            order.isdelete = "未删除";
            order.status = "您提交订单";
            order.payStatus = "线下支付";
            MySqlUnitity.Ins.Insert(order, order.GetType());
            return order.weixiuId;
        }

        // PUT: api/Submit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Submit/5
        public void Delete(int id)
        {
        }
    }
}
