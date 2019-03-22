using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class QueryController : ApiController
    {
        // GET: api/Query
        public IEnumerable<FixOrder> Get()
        {
            var list = MySqlUnitity.Ins.Query<FixOrder>(string.Format(@"SELECT
                                                                        	IFNULL(a.xingming, '**') NAME,
                                                                        	CONCAT(
                                                                        		IFNULL(sjpp, ''),
                                                                        		IFNULL(sjxh, ''),
                                                                        		IFNULL(sjgzlx, '')
                                                                        	) fault,
                                                                        	a.timeString AS time
                                                                        FROM
                                                                        	weixiu a
                                                                        LEFT JOIN `user` b ON b.user_name LIKE CONCAT('%', a.wxfa, '%')
                                                                        ORDER BY
                                                                        	timeString DESC
                                                                        LIMIT 3"));
            foreach (var item in list)
            {
                item.Time = DateTime.Parse(item.Time).ToString("yyyy-MM-dd");
                if (item.Fault.IndexOf("苹果") > -1)
                {
                    item.Fault = item.Fault.Substring(2);
                }
                if (item.Fault.Length > 21)
                {
                    item.Fault = item.Fault.Substring(0, 21) + "...";
                }
                if (item.Fault[item.Fault.Length - 1] == ',')
                {
                    item.Fault = item.Fault.Substring(0, item.Fault.Length - 1);
                }
                if (item.Name.Length > 1)
                {
                    string star = "";
                    for (int i = 0; i < item.Name.Length - 1; i++)
                    {
                        if (i > 2)
                        {
                            continue;
                        }
                        star += "*";
                    }
                    item.Name = item.Name.Substring(0, 1) + star;
                }
            }
            return list;
        }

        // GET: api/Query/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Query
        public IEnumerable<FixOrder> Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string phone = obj["bland"].ToString();
            string qtype = obj["qtype"] == null ? "0" : obj["qtype"].ToString();
            string sqlstr = string.Format(@"SELECT
                                                                      	a.weixiuId AS `Code`,
                                                                      	a.fwlx AS FixType,
                                                                      	a.`status` AS Status,
                                                                      	a.timeString AS Time,
                                                                      	a.wxfabj AS Price,
                                                                      	a.sjgzlx AS Fault,
                                                                      	IFNULL(b.user_phone, '4008678597') AS Phone,
	                                                                    IFNULL(b.IDCard, '人工客服') AS `Name`,
                                                                        a.timeString as Time
                                                                      FROM
                                                                      	weixiu a
                                                                      LEFT JOIN `user` b ON a.wxfa  LIKE CONCAT('%',b.user_name,'%')WHERE shoujihao = '{0}' ORDER BY timeString DESC LIMIT 10", phone);
            if (qtype == "1")
            {
                string sdate = obj["sdate"].ToString();
                string edate = obj["edate"].ToString();

                sqlstr = @"SELECT
	a.weixiuId AS `Code`,
	a.fwlx AS FixType,
	a.`status` AS STATUS,
	a.timeString AS Time,
	a.wxfabj AS Price,
	a.sjgzlx AS Fault,
	a.shoujihao AS Phone,
	IFNULL(a.xingming, '未知') AS `Name`,
  a.dizhi as Addr,
  a.sjxh as phoneVer
FROM
	weixiu a
LEFT JOIN `user` b ON a.wxfa LIKE CONCAT('%', b.user_name, '%')";
                if (string.IsNullOrWhiteSpace(sdate) || string.IsNullOrWhiteSpace(edate))
                {
                    if (string.IsNullOrWhiteSpace(phone))
                    {
                        sqlstr = sqlstr + string.Format(@" ORDER BY timeString DESC LIMIT 10;");
                    }
                    else
                    {
                        sqlstr = sqlstr + string.Format(@" WHERE  a.shoujihao like '%{0}%' ORDER BY timeString DESC LIMIT 10;", phone);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(phone))
                    {
                        sqlstr = sqlstr + string.Format(@" WHERE
                        a.timeString>'{0}' and a.timeString<'{1} 38:59:59'
                        ORDER BY timeString DESC ;", sdate, edate);
                    }
                    else
                    {
                        sqlstr = sqlstr + string.Format(@" WHERE
                        a.timeString>'{1}' and a.timeString<'{2} 23:59:59'
                        and a.shoujihao like '%{0}%' ORDER BY timeString DESC;", phone, sdate, edate);
                    }

                }

            }
            var list = MySqlUnitity.Ins.Query<FixOrder>(sqlstr);
            foreach (var item in list)
            {
                item.QType = qtype;
            }
            return list;
        }

        // PUT: api/Query/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Query/5
        public void Delete(int id)
        {
        }
    }
}
