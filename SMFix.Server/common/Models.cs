using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMFix.Server
{
    public class Models
    {
        public string name { get; set; }
    }
    public class InitData
    {
        public List<Models> Bland { get; set; } = new List<Models>();
        public List<Models> Ver { get; set; } = new List<Models>();
        public List<Models> Color { get; set; } = new List<Models>();
        public List<Fault> Fault { get; set; } = new List<Fault>();
    }
    public class tb_postAddr
    {
        public int id { get; set; }
        public int addrType { get; set; }
        public string name { get; set; }
        public string addr { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string postName { get; set; }
        public string phone { get; set; }
    }
    public class Fault
    {
        public string name { get; set; }
        public string price { get; set; }
        public bool isSelect { get; set; }
    }
    public class weixiu
    {
        public string weixiuId { get; set; }
        public string sjpp { get; set; }
        public string sjxh { get; set; }
        public string yanse { get; set; }
        public string sjgzlx { get; set; }
        public string xingming { get; set; }
        public string shoujihao { get; set; }
        public string timeString { get; set; }
        public string fwlx { get; set; }
        public string dizhi { get; set; }
        public string wxfabj { get; set; }
        public string isdelete { get; set; }
        public string status { get; set; }
        public string payStatus { get; set; }
        public string wxfa { get; set; }
        public string beizhu { get; set; }
        public string sjgzlxbz { get; set; }
    }
    public class tb_log
    {
        public int id { get; set; }
        public string info { get; set; }
        public DateTime time { get; set; }
    }
    public class FixOrder
    {
        public string FixType { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
        public string Fault { get; set; }
        public string Price { get; set; }
        public string Phone { get; set; }
        public string QType { get; set; }
        public string Addr { get; set; }
        public string phoneVer { get; set; }
    }
    public class tb_feedback
    {
        public string feedback { get; set; }
        public DateTime time { get; set; }
    }
    public class AuthCode
    {
        public string Phone { get; set; }
        public string Code { get; set; }
    }
    public class tb_recycle
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string remark { get; set; }
        public DateTime create_time { get; set; }
        public int reStatus { get; set; }
    }
    public class RAMFixModel
    {
        public int id { get; set; }
        public int sortIndex { get; set; }
        public string phoneCode { get; set; }
        public string fixPrice { get; set; }
        public string fixType { get; set; }
        public DateTime time { get; set; }
        public string remark { get; set; }
    }
    public class RAMFix : IComparable<RAMFix>
    {
        public int sortIndex { get; set; }
        public string phoneCode { get; set; }
        public List<FixInfo> info { get; set; }
        public int CompareTo(RAMFix other)
        {
            int result;
            if (this.sortIndex > other.sortIndex)
            {
                result = 1;
            }
            else
            {
                result = -1;
            }
            return result;
        }
    }
    public class FixInfo : IComparable<FixInfo>
    {
        public int id { get; set; }
        public string phoneCode { get; set; }
        public int sortIndex { get; set; }
        public string fixPrice { get; set; }
        public string fixType { get; set; }

        public int CompareTo(FixInfo other)
        {
            int result;
            if (this.sortIndex > other.sortIndex)
            {
                result = 1;
            }
            else
            {
                result = -1;
            }
            return result;
        }
    }
    public class tb_systemConfig {
        public int id { get; set; }
        public string cType { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public int active { get; set; }
    }
}