using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superMan.Model
{
   public class tb_postAddr
    {
        [Description("External")]
        public int id { get; set; }
        public int addrType { get; set; }
        public string name { get; set; }
        public string addr { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string postName { get; set; }
    }
}
