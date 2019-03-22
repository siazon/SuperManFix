using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMFix.Server
{
    public class HttpUtility
    {
        public HttpUtility()
        {

        }
        private static HttpUtility _instance = null;
        public static HttpUtility Ins
        {
            get { if (_instance == null) { _instance = new HttpUtility(); } return _instance; }
        }
        public Task<string> PostAsync(string postUrl, string paramData, bool hasHeaders = false)
        {
            return Task.Factory.StartNew(() =>
            {
                return Post(postUrl, paramData, hasHeaders);
            });
        }
        public void GetAsync(string phone, string code)
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
        public string Post(string postUrl, string paramData, bool hasHeaders = false)
        {
            try
            {


                string ret = string.Empty;
                Encoding dataEncode = System.Text.Encoding.GetEncoding("utf-8");
                //将URL编码后的字符串转化为字节
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                //Post请求方式
                webReq.Method = "POST";
                if (hasHeaders)
                {
                    webReq.Headers.Add(HttpRequestHeader.Authorization, "2D12CFF31185C46693C3EA4F0A8647CF");
                    SetHeaderValue(webReq.Headers, "RegisterNo", "902");
                    SetHeaderValue(webReq.Headers, "Date", DateTime.Now.ToString("yyyy-MM-dd HH:m:ss"));
                }
                // 内容类型
                webReq.ContentType = "application/json";
                webReq.Timeout = 1500;
                //设置请求的 ContentLength 
                webReq.ContentLength = byteArray.Length; //获得请 求流
                Stream newStream = webReq.GetRequestStream();
                //将请求参数写入流
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                                                                // 关闭请求流
                newStream.Close();
                // 获得响应流
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                return ret;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
    }
}
