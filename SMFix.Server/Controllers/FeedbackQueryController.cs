using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class FeedbackQueryController : ApiController
    {
        // GET: api/FeedbackQuery
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FeedbackQuery/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FeedbackQuery
        public object Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string qtype = obj["qtype"].ToString();
            string sdate = obj["sdate"].ToString();
            string edate = obj["edate"].ToString();
            string phone = obj["phone"].ToString();
            List<finfos> infos = new List<finfos>();
            string sqlstr = "";
            if (qtype == "1")
            {
                sqlstr = @"SELECT IFNULL(userName,'热心用户') as Name,IFNULL(phone,'热心用户') as Phone,feedback as Addr, time as time 
                  from tb_feedback";
                if (string.IsNullOrWhiteSpace(sdate) || string.IsNullOrWhiteSpace(edate))
                {
                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        sqlstr = sqlstr + string.Format(@" where phone like '%{0}%'  ORDER BY time DESC  LIMIT 10", phone);
                    }
                    else
                    {
                        sqlstr = sqlstr + string.Format(@" ORDER BY time DESC LIMIT 10", phone);
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        sqlstr = sqlstr + string.Format(@" where time>'{0}' and time <'{1} 23:59:59'  and phone
                        like '%{2}%' ORDER BY time DESC", sdate, edate, phone);
                    }
                    else
                    {
                        sqlstr = sqlstr + string.Format(@" where time>'{0}' and time <'{1} 23:59:59' 
                        ORDER BY time DESC", sdate, edate);
                    }
                   
                }
              
            }
            else
            {
                sqlstr = @" SELECT userName as Name,phone as Phone, addr as Addr, create_time as time from tb_recycle";
                if (string.IsNullOrWhiteSpace(sdate) || string.IsNullOrWhiteSpace(edate))
                {
                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        sqlstr = sqlstr + string.Format(@" where  phone like '%{0}%'  ORDER BY create_time DESC  LIMIT 10", phone);
                    }
                    else
                    {
                        sqlstr = sqlstr + string.Format(@"  ORDER BY create_time DESC  LIMIT 10", phone);
                    }
                   
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        sqlstr = sqlstr + string.Format(@" where create_time>'{0}' and create_time <'{1} 23:59:59'
                        and phone like '%{2}%'  ORDER BY create_time DESC", sdate, edate, phone);
                    }
                    else
                    {
                        sqlstr = sqlstr + string.Format(@" where create_time>'{0}' and create_time <'{1} 23:59:59'
                         ORDER BY create_time DESC", sdate, edate);
                    }
                 
                }
            }
            infos = MySqlUnitity.Ins.Query<finfos>(sqlstr);
            if (qtype == "1")
            {
                foreach (var item in infos)
                {
                    string[] phones = item.Addr.Split(':');
                    if (phones.Length > 1)
                    {
                        item.Phone = phones[0].Length == 0 ? "热心用户" : phones[0];
                        item.Addr = phones[1];
                    }
                    else if (phones.Length == 1)
                    {
                        item.Addr = phones[0];
                        item.Phone = "热心用户";
                    }
                    else
                    {
                    }
                }
            }
            return infos;

        }

        // PUT: api/FeedbackQuery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FeedbackQuery/5
        public void Delete(int id)
        {
        }
    }
    public class finfos
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Addr { get; set; }
        public DateTime time { get; set; }
    }
}
