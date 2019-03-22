using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Super.Website.Core
{
    public static class HttpHelper
    {
        public static void GetAsync(string phone, string code)
        {
            Task.Factory.StartNew(() =>
            {
                Get(phone, code);
            });
        }
        static void Get(string phone, string code)
        {
            string url = string.Format("http://v.juhe.cn/sms/send?mobile={0}&tpl_id=39923&tpl_value=%2523code%2523%253d{1}&dtype=json&key=c46939069bc1138cbea40becef3f324c", phone, code);
            HttpClient client = new HttpClient();
            string content = client.GetStringAsync(url).Result;

        }
    }
}