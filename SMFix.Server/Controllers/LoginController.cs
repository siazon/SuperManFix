using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SMFix.Server.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public object Post([FromBody]object value)
        {
            JObject obj = JObject.Parse(value.ToString());
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
            if(userInfo!=null)
            DbManager.Ins.ExecuteNonquery(string.Format(@"INSERT INTO tb_user (openID,nickName,createTime) 
                                            SELECT '{0}','{1}','{2}' FROM DUAL WHERE	NOT EXISTS (	
                                            SELECT	openID	FROM tb_user WHERE openID = '{0}');",
                            userInfo.openId, userInfo.nickName, DateTime.Now));
            return userInfo;
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
