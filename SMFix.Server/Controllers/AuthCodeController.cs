using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class AuthCodeController : ApiController
    {
        private DateTime now = DateTime.Now;
        public AuthCodeController()
        {
            WebApiConfig.systemConfigs = MySqlUnitity.Ins.Query<tb_systemConfig>(@"SELECT * from tb_systemConfig where active=1;");
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
        // GET: api/AuthCode
        public List<tb_systemConfig> Get()
        {


            List<tb_systemConfig> list = WebApiConfig.systemConfigs = MySqlUnitity.Ins.Query<tb_systemConfig>(@"SELECT * from tb_systemConfig where active=1;");
            return list;
        }

        // GET: api/AuthCode/5
        public string Get(int id)
        {
            return "value";
        }
        private Queue SendMsgQueue = Queue.Synchronized(new Queue());
        // POST: api/AuthCode
        public void Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
            string Phone = obj["Phone"].ToString();
            string Code = obj["Code"].ToString();
            string push = obj["push"] == null ? "" : obj["push"].ToString();
            if (push == "custmer")
                HttpHelper.GetAsync(Phone, Code);
            else
            {
                List<tb_systemConfig> list = WebApiConfig.systemConfigs.Where(a => a.code.Contains("Rule")).ToList();
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
        // PUT: api/AuthCode/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AuthCode/5
        public void Delete(int id)
        {
        }
    }
    public class Msg
    {
        public string phone { get; set; }
        public string info { get; set; }
    }
}
