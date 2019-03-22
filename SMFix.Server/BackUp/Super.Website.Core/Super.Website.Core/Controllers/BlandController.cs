using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Super.Website.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/Bland")]
    public class BlandController : Controller
    {
        private DateTime now = DateTime.Now;
        public BlandController()
        {
         
            GlobalData.Ins.systemConfigs = MySqlUnitity.Ins.Query<tb_systemConfig>(@"SELECT * from tb_systemConfig where active=1;");
            Task.Run(() =>
            {
                while (true)
                {
                    if ((DateTime.Now - now).TotalSeconds > 60)
                    {
                        var msg = SendMsgQueue.Count > 0 ? SendMsgQueue.Dequeue() as Msg : null;
                        if (msg != null)
                        {
                            HttpHelper.GetAsync(msg.phone, msg.info);
                            now = DateTime.Now;
                        }
                    }
                    Thread.Sleep(1000);
                }
            });
        }
        // GET: api/Bland
        [HttpGet]
        public InitData Get()
        {
            InitData datas = new InitData();
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
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bland
        [HttpPost]
        public object Post([FromBody]object value)
        {
            string methodFun = "";

            JObject Jobj = JObject.Parse(value.ToString());
            methodFun = Jobj["FunType"].ToString();
            JObject obj = JObject.Parse(Jobj["Data"].ToString());
            if (methodFun== "GetQuery")
            {
                return GetQuery();
            }
            switch (methodFun)
            {
                case "Bland":
                    string bland = obj["name"].ToString();
                    var list = MySqlUnitity.Ins.Query<Models>(string.Format("SELECT DISTINCT sjxh as 'name' from wxjm where sjpp ='{0}' order by ksmjg", bland));
                    return list;
                case "Fixs":
                    return GetFixs();
                case "Colors":
                    return GetColors(obj);
                case "addrType":
                    string addrType = obj["addrType"].ToString();
                    var addrs = MySqlUnitity.Ins.Query<tb_postAddr>(string.Format("SELECT * from tb_postAddr where addrType={0};", addrType));
                    return addrs;
                case "Fault":
                    return GetFualt(obj);
                case "feedback":
                    tb_feedback backs = new tb_feedback();
                    backs.feedback = obj["feedback"].ToString();
                    backs.time = DateTime.Now;
                    return MySqlUnitity.Ins.Insert(backs, backs.GetType());
                case "feedbacks":
                    return GetFeedbacks(obj);
                case "recycle":
                    return recycle(obj);
                case "Submit":
                    return Submit(obj);
                case "SystemConfig":
                    List<tb_systemConfig> configs = MySqlUnitity.Ins.Query<tb_systemConfig>(@"SELECT * from tb_systemConfig where active=1;");
                    return configs;
                case "ChkLogin":
                    return ChkLogin(obj);
                case "Login":
                    return Login(obj);
                case "GetQuery":
                    return GetQuery();
                case "GetOrders":
                    return GetOrders(obj);
                case "AuthCode":
                    AuthCode(obj);
                    return "";
                default:
                    return null;
            }
        }
        private object recycle(JObject obj)
        {
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
        private object Login(JObject obj)
        {
            string authCode = obj["authCode"].ToString();
            WechatLoginInfo WLInfo = new WechatLoginInfo();
            string appID = "wx002f79fe6f42373a";
            string appSecret = "77eae58640e8f24e11780709710b1d02";
            WeChatAppDecrypt WAD = new WeChatAppDecrypt(appID, appSecret);

            WLInfo.encryptedData = obj["encryptedData"].ToString();
            WLInfo.code = obj["code"].ToString();
            WLInfo.iv = obj["iv"].ToString();
            WLInfo.rawData = obj["rawData"].ToString();
            WLInfo.signature = obj["signature"].ToString();

            WechatUserInfo userInfo = WAD.Decrypt(WLInfo);
            if (userInfo != null)
                DbManager.Ins.ExecuteNonquery(string.Format(@"INSERT INTO tb_user (openID,nickName,createTime) 
                                            SELECT '{0}','{1}','{2}' FROM DUAL WHERE	NOT EXISTS (	
                                            SELECT	openID	FROM tb_user WHERE openID = '{0}');",
                                userInfo.openId, userInfo.nickName, DateTime.Now));
            return userInfo;
        }
        private object ChkLogin(JObject obj)
        {
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
        private Queue SendMsgQueue = Queue.Synchronized(new Queue());
        private void AuthCode(JObject obj)
        {
            string Phone = obj["Phone"].ToString();
            string Code = obj["Code"].ToString();
            string push = obj["push"] == null ? "" : obj["push"].ToString();
            if (push == "custmer")
                HttpHelper.GetAsync(Phone, Code);
            else
            {
                List<tb_systemConfig> list = GlobalData.Ins.systemConfigs.Where(a => a.code.Contains("Rule")).ToList();
                foreach (var item in list)
                {
                    int rule = 0;
                    int.TryParse(item.code.Substring(4), out rule);
                    List<string> Rules = GetRules(rule);
                    foreach (var ru in Rules)
                    {
                        if (push == ru)
                        {
                            SendMsgQueue.Enqueue(new Msg() { phone = item.value, info = Code });
                        }
                    }


                }
            }
        }
        private List<string> GetRules(int RuleValue)
        {
            Byte[] Bytes = BitConverter.GetBytes(RuleValue);
            BitArray BA = new BitArray(Bytes);
            List<string> rules = new List<string>();
            for (int i = 0; i < BA.Count; i++)
            {
                if (BA[i])
                {
                    rules.Add(Math.Pow(2, i).ToString());
                }
            }
            return rules;
        }
        private object Submit(JObject obj)
        {
            int id = 0;
            List<weixiu> list = MySqlUnitity.Ins.Query<weixiu>(@"SELECT weixiuId from weixiu order by weixiuid DESC LIMIT 1;");
            if (list.Count > 0)
            {
                int.TryParse(list[0].weixiuId, out id);
            }
            weixiu order = new weixiu();
            JObject datass = JObject.Parse(obj["datas"].ToString());
            order.weixiuId = (id + 1).ToString();
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
            order.sjgzlxbz = datass["Time"].ToString() + datass["Remark"].ToString();
            order.beizhu = datass["Remark"].ToString();//bakcAddr
            order.wxfa = "待处理";
            order.isdelete = "未删除";
            order.status = "您提交订单";
            order.payStatus = "线下支付";
            MySqlUnitity.Ins.Insert(order, order.GetType());
            return order.weixiuId;
        }
        private object GetQuery()
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
        private object GetOrders(JObject obj)
        {
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
        private object GetFeedbacks(JObject obj)
        {
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
        private object GetFualt(JObject obj)
        {
            string bland = obj["bland"].ToString();
            string ver = obj["ver"].ToString();
            var list = MySqlUnitity.Ins.Query<Fault>(string.Format("SELECT gzlx as name,ycjg as price from wxjm where sjpp='{0}' AND sjxh='{1}' ORDER BY mklx", bland, ver));
            foreach (var item in list)
            {
                if (item.name.Length >= 6)
                {
                    item.name = item.name.Substring(0, 6);
                }
            }
            return list;
        }
        private object GetColors(JObject obj)
        {
            string bland = obj["bland"].ToString();
            string ver = obj["ver"].ToString();
            var list = MySqlUnitity.Ins.Query<Models>(string.Format("SELECT DISTINCT IFNULL(yanse,'通用') as 'name' from sjxh where sjpp='{0}' AND sjxh='{1}'", bland, ver));
            return list;
        }
        private List<RAMFix> GetFixs()
        {
            List<RAMFix> fixs = new List<RAMFix>();
            RAMFix fix = new RAMFix() { info = new List<FixInfo>() };
            FixInfo info = new FixInfo();
            var fixList = MySqlUnitity.Ins.Query<RAMFixModel>("SELECT * from tb_RAMfix ORDER BY sortIndex");
            foreach (var item in fixList)
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
                    fix = new RAMFix() { info = new List<FixInfo>(), sortIndex = item.sortIndex };
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
        // PUT: api/Bland/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
