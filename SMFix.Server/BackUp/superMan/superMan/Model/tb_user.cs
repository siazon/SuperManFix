using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superMan.Model
{
    public class tb_user
    {
        [Description("External")]
        public int id { get; set; }
        public string userName { get; set; }
        public string nickName { get; set; }
        public string passWord { get; set; }
        public int active { get; set; }
        public int userType { get; set; }
        public int pID { get; set; }
        public string usrPhone { get; set; }
        public string openID { get; set; }
        public int ruleID { get; set; }
        public DateTime createTime { get; set; }

    }
}
