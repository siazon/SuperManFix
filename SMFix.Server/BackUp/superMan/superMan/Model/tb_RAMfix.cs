using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superMan.Model
{
  public  class tb_RAMfix
    {
        [Description("External")]
        public int id { get; set; }
        public int sortIndex { get; set; }
        public string phoneCode { get; set; }
        public string fixType { get; set; }
        public string fixPrice { get; set; }
        public DateTime time { get; set; }
        public string remark { get; set; }
    }
}
