using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMFix.Server
{
    public class SessionData
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public int expires_in { get; set; }
    }
    public class WechatLoginInfo
    {
        public string code { get; set; }
        public string encryptedData { get; set; }
        public string iv { get; set; }
        public string rawData { get; set; }
        public string signature { get; set; }
    }
    public class WechatUserInfo
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string unionId { get; set; }
        public string accessToken { get; set; }
        public Watermark watermark { get; set; }

        public class Watermark
        {
            public string appid { get; set; }
            public string timestamp { get; set; }
        }
    }
    public class tb_user
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string nickName { get; set; }
        public string passWord { get; set; }
        public string userType { get; set; }
        public string userPhone { get; set; }
        public string openID { get; set; }
        public int active { get; set; }
        public int pID { get; set; }
        public int ruleID { get; set; }
        public DateTime createTime { get; set; }
    }
}